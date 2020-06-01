using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ;
using BleServer.Infrastructure.BlueZ.Advertisements;
using BleServer.Infrastructure.BlueZ.Gatt;
using Tmds.DBus;

namespace BleServer
{
    internal class SampleGattApplication
    {
        public static async Task RegisterGattApplication(Connection connection)
        {
            var descriptor = new GattDescriptor(new GattDescriptor1Properties
            {
                Characteristic = "/characteristics/characteristic0",
                Value = new[] {(byte) 't'},
                UUID = "12345678-1234-5678-1234-56789abcdef2",
                Flags = new[] {"read", "write"}
            });

            var characteristic = new GattCharacteristic(new GattCharacteristic1Properties
            {
                UUID = "12345678-1234-5678-1234-56789abcdef1",
                Flags = new[] {"read", "write"},
                Service = "/services/service0"
            }, TODO);

            var service = new GattService(new GattService1Properties
            {
                UUID = "12345678-1234-5678-1234-56789abcdef0",
                Characteristics = new[] {new ObjectPath("/characteristics/characteristic0")}
            });

            var application = new GattApplication();

            await connection.RegisterObjectsAsync(new IDBusObject[] {characteristic, descriptor, service, application});
        }
    }

    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Hello Bluetooth");

            Task.Run(async () =>
            {
                using (var connection = new Connection(Address.System))
                {
                    await ConnectToDBus(connection);
                    var advertisement = await CreateAdvertisementObject(connection);
                    await RegisterAdvertisement(connection, advertisement);
                    await SampleGattApplication.RegisterGattApplication(connection);
                }
            }).Wait();

            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
        }

        private static async Task RegisterAdvertisement(Connection connection, IDBusObject advertisement)
        {
            var advertisingManager = AdvertisingManager.GetAdvertisingManager(connection);

            await advertisingManager.RegisterAdvertisementAsync(advertisement.ObjectPath,
                new Dictionary<string, object>());
            Console.WriteLine("Advertisement registered");
        }

        private static async Task<IDBusObject> CreateAdvertisementObject(Connection connection)
        {
            var advertisement = await Advertisement.CreateAdvertisement(connection);
            Console.WriteLine("Advertisement created");
            return advertisement;
        }

        private static async Task ConnectToDBus(IConnection connection)
        {
            await connection.ConnectAsync();
        }
    }
}