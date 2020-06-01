using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tmds.DBus;

namespace BleServer.Infrastructure.BlueZ.Advertisements
{
    public class Advertisement : ILEAdvertisement1
    {
        public static readonly ObjectPath Path = new ObjectPath("/org/bluez/example/advertisement0");
        private readonly IDictionary<string, object> _Properties;

        public Advertisement(IDictionary<string, object> properties)
        {
            _Properties = properties;
        }

        public ObjectPath ObjectPath => Path;

        public Task<IDictionary<string, object>> GetAllAsync()
        {
            return Task.FromResult(_Properties);
        }

        public Task<object> GetAsync(string prop)
        {
            return Task.FromResult(_Properties[prop]);
        }

        public Task SetAsync(string prop, object val)
        {
            _Properties[prop] = val;
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

        public event Action<PropertyChanges> OnPropertiesChanged;

        public static async Task<IDBusObject> CreateAdvertisement(Connection connection)
        {
            var advertisement = new Advertisement(new Dictionary<string, object>
            {
                {"Type", "peripheral"},
                {"ServiceUUIDs", new[] {"180D", "180F"}},
                // {
                //     "ManufacturerData",
                //     new Dictionary<string, object> {{"0xffff", new[] {"0x00", "0x01", "0x02", "0x03"}}}
                // },
                {"SolicitUUIDs", new[] {"180D", "180F"}},
                // {"ServiceData", new Dictionary<string, object> {{"9999", new[] {"0x00", "0x01", "0x02", "0x03"}}}},
                {"Includes", new string[0]},
                {"LocalName", "Edelweiss ASD"},
                // {"Appearance", 0},
                {"Duration", (ushort) 10}
                // {"Timeout", 1000}
            });

            await connection.RegisterObjectAsync(advertisement);

            return advertisement;
        }
    }
}