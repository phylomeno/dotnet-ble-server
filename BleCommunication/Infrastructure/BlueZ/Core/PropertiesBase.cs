using System;
using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ.Utilities;
using Tmds.DBus;

namespace BleServer.Infrastructure.BlueZ.Core
{
    public abstract class PropertiesBase<TV>
    {
        protected readonly TV Properties;
        private readonly ObjectPath _ObjectPath;

        protected PropertiesBase(ObjectPath objectPath, TV properties)
        {
            _ObjectPath = objectPath;
            Properties = properties;
        }

        public ObjectPath ObjectPath => _ObjectPath;

        public Task<object> GetAsync(string prop)
        {
            return Task.FromResult(Properties.ReadProperty(prop));
        }
        public Task<T> GetAsync<T>(string prop)
        {
            return Task.FromResult(Properties.ReadProperty<T>(prop));
        }

        public Task<TV> GetAllAsync()
        {
            return Task.FromResult(Properties);
        }

        public Task SetAsync(string prop, object val)
        {
            return Properties.SetProperty(prop, val);
        }

        public Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler)
        {
            return SignalWatcher.AddAsync(this, nameof(OnPropertiesChanged), handler);
        }

        public event Action<PropertyChanges> OnPropertiesChanged;
    }
}