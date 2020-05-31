using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tmds.DBus;

namespace BleServer.Infrastructure.BlueZ.Gatt
{
    public class GattApplication : IObjectManager
    {
        public ObjectPath ObjectPath { get; }
        public Task<IDictionary<ObjectPath, IDictionary<string, IDictionary<string, object>>>> GetManagedObjectsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDisposable> WatchInterfacesAddedAsync(Action<(ObjectPath @object, IDictionary<string, IDictionary<string, object>> interfaces)> handler, Action<Exception> onError = null)
        {
            throw new NotImplementedException();
        }

        public Task<IDisposable> WatchInterfacesRemovedAsync(Action<(ObjectPath @object, string[] interfaces)> handler, Action<Exception> onError = null)
        {
            throw new NotImplementedException();
        }
    }
}