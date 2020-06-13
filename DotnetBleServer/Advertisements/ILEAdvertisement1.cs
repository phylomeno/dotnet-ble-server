using System;
using System.Threading.Tasks;
using Tmds.DBus;

namespace DotnetBleServer.Advertisements
{
    [DBusInterface("org.bluez.LEAdvertisement1")]
    internal interface ILEAdvertisement1 : IDBusObject
    {
        Task ReleaseAsync();
        Task<object> GetAsync(string prop);
        Task<AdvertisementProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }
}