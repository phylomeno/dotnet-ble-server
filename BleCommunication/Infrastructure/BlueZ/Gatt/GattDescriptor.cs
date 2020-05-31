using System;
using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ.Utilities;
using Tmds.DBus;

namespace BleServer.Infrastructure.BlueZ.Gatt
{
    internal class GattDescriptor : IGattDescriptor1
    {
        private readonly GattDescriptor1Properties _Properties;

        public GattDescriptor(GattDescriptor1Properties gattDescriptor1Properties)
        {
            _Properties = gattDescriptor1Properties;
        }

        public ObjectPath ObjectPath { get; }

        public Task<byte[]> ReadValueAsync()
        {
            return Task.FromResult(_Properties.Value);
        }

        public Task<T> GetAsync<T>(string prop)
        {
            return _Properties.ReadProperty<T>(prop);
        }

        public Task<GattDescriptor1Properties> GetAllAsync()
        {
            return Task.FromResult(_Properties);
        }

        public Task SetAsync(string prop, object val)
        {
            return _Properties.SetProperty(prop, val);
        }

        public Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler)
        {
            return SignalWatcher.AddAsync(this, nameof(OnPropertiesChanged), handler);
        }

        public event Action<PropertyChanges> OnPropertiesChanged;
    }
}