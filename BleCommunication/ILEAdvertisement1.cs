using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tmds.DBus;

namespace BleCommunication
{
    [DBusInterface("org.bluez.LEAdvertisement1")]
    internal interface ILEAdvertisement1 : IDBusObject
    {
        Task ReleaseAsync();
        Task<object> GetAsync(string prop);
        Task<IDictionary<string, object>> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }
}