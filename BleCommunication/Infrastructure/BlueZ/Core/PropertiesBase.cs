using System;
using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ.Utilities;
using Tmds.DBus;

namespace BleServer.Infrastructure.BlueZ.Core
{
    public abstract class PropertiesBase<TV>
    {
        protected readonly TV Properties;

        protected PropertiesBase(TV properties)
        {
            Properties = properties;
        }

        public ObjectPath ObjectPath { get; }

        public Task<T> GetAsync<T>(string prop)
        {
            return Properties.ReadProperty<T>(prop);
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