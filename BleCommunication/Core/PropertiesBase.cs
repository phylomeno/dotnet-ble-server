using System;
using System.Threading.Tasks;
using BleServer.Utilities;
using Tmds.DBus;

namespace BleServer.Core
{
    public abstract class PropertiesBase<TV>
    {
        protected readonly TV Properties;
        private ObjectPath _ObjectPath;

        protected PropertiesBase(TV properties)
        {
            Properties = properties;
        }

        public ObjectPath ObjectPath => _ObjectPath;

        public void SetObjectPath(ObjectPath objectPath)
        {
            _ObjectPath = objectPath;
        }

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