using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ;
using Tmds.DBus;

namespace BleCommunication
{
    [DBusInterface("org.bluez.LEAdvertisement1")]
    interface ILEAdvertisement1 : IDBusObject
    {
        Task ReleaseAsync();
        Task<object> GetAsync(string prop);
        Task<IDictionary<string, object>> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    public class Advertisement : ILEAdvertisement1
    {
        public static readonly ObjectPath Path = new ObjectPath("/org/bluez/example/advertisement");
        private IDictionary<string, object> _properties;
        public Advertisement(IDictionary<string, object> properties)
        {
            _properties = properties;
        }
        public ObjectPath ObjectPath
        {
            get
            {
                return Path;
            }
        }

        public event Action<PropertyChanges> OnPropertiesChanged;

        public Task<IDictionary<string, object>> GetAllAsync()
        {
            return Task.FromResult(_properties);
        }

        public Task<object> GetAsync(string prop)
        {
            return Task.FromResult(_properties[prop]);
        }

        public Task SetAsync(string prop, object val)
        {
            _properties[prop] = val;
            OnPropertiesChanged?.Invoke(PropertyChanges.ForProperty(prop, val));
            return Task.CompletedTask;
        }

        public Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler)
        {
            return SignalWatcher.AddAsync(this, nameof(OnPropertiesChanged), handler);
        }
        public Task ReleaseAsync()
        {
            throw new NotImplementedException();
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello Bluetooth");

            Task.Run(async () =>
            {
                using (var connection = new Connection(Address.System))
                {
                    await connection.ConnectAsync();
                    var advertisement = await CreateAdvertisement(connection);
                    Console.WriteLine("Advertisement created");
                    var advertisingManager = GetAdvertisingManager(connection);

                    await advertisingManager.RegisterAdvertisementAsync(advertisement.ObjectPath, new Dictionary<string, object>());
                    Console.WriteLine("Advertisement registered");

                }
            }).Wait();


            Console.WriteLine("Waiting");
            while (true)
            {
                Thread.Sleep(5000);
            }
        }

        private static async Task<IDBusObject> CreateAdvertisement(Connection connection)
        {
            var advertisement = new Advertisement(new Dictionary<string, object>()
            {
                {"Type", "peripheral"},
                {"ServiceUUIDs", new[] {"180D", "180F"}},
                {
                    "ManufacturerData",
                    new Dictionary<string, object> {{"0xffff", new[] {"0x00", "0x01", "0x02", "0x03"}}}
                },
                {"ServiceData", new Dictionary<string, object> {{"9999", new[] {"0x00", "0x01", "0x02", "0x03"}}}},
                {"IncludeTxPower", true},
                {"LocalName", "TestAdvertisement"}
            });

            var advertisementProxy = connection.CreateProxy<ILEAdvertisement1>("org.bluez", advertisement.ObjectPath);
            await connection.RegisterObjectAsync(advertisement);

            //await advertisementProxy.SetAsync("LocalName", "My Adv");

            return advertisementProxy;
        }

        private static ILEAdvertisingManager1 GetAdvertisingManager(Connection connection)
        {
            return connection.CreateProxy<ILEAdvertisingManager1>("org.bluez", "/org/bluez/hci0");
        }
    }
}