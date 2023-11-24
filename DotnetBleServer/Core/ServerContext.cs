using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tmds.DBus;

namespace DotnetBleServer.Core
{
    public class ServerContext : ServerContextBase
    {
        public IAdapter1 Adapter { get; set; }

        public async Task ConnectAndSetDefaultAdapter()
        {
            await Connect();

            var adapters = await GetAdaptersAsync();
            if (adapters.Count == 0)
            {
                throw new Exception("No Bluetooth adapters found.");
            }

            Adapter = adapters[0];
        }

        /// <summary>Get Bluetooth Adapter object based on name.</summary>
        /// <param name="adapterName">Adapter Name.</param>
        /// <param name="fullName">False allows shortened adapter name ('hci0'). True, for full name ('/org/bluez/hci0').</param>
        /// <returns>Adapter object</returns>
        /// <exception cref="Exception"></exception>
        public async Task<IAdapter1> GetAdapterAsync(string adapterName, bool fullName = false)
        {
            var adapterObjectPath = !fullName ? $"/org/bluez/{adapterName}" : adapterName;
            var adapter = Connection.CreateProxy<IAdapter1>(Constants.DbusServicePath, adapterObjectPath);

            try
            {
                await adapter.GetAliasAsync();
            }
            catch (Exception)
            {
                throw new Exception($"Bluetooth adapter {adapterName} not found.");
            }

            return adapter;
        }

        public async Task<IReadOnlyList<IAdapter1>> GetAdaptersAsync()
        {
            return await GetProxiesAsync<IAdapter1>(Constants.AdapterInterfacePath, rootObject: null);
        }

        /// <param name="interfaceName">The interface to search for</param>
        /// <param name="rootObject">The DBus object to search under. Can be null</param>
        internal async Task<IReadOnlyList<T>> GetProxiesAsync<T>(string interfaceName, IDBusObject rootObject)
        {
            // Console.WriteLine("GetProxiesAsync called.");
            var objectManager = Connection.CreateProxy<IObjectManager>(Constants.DbusServicePath, "/");
            var objects = await objectManager.GetManagedObjectsAsync();

            var matchingObjectPaths = objects
                .Where(obj => IsMatch(interfaceName, obj.Key, obj.Value, rootObject))
                .Select(obj => obj.Key);

            var proxies = matchingObjectPaths
                .Select(objectPath => Connection.CreateProxy<T>(Constants.DbusServicePath, objectPath))
                .ToList();

            //Console.WriteLine($"GetProxiesAsync returning {proxies.Count} proxies of type {typeof(T)}.");
            return proxies;
        }

        internal static bool IsMatch(string interfaceName, ObjectPath objectPath, IDictionary<string, IDictionary<string, object>> interfaces, IDBusObject rootObject)
        {
            return IsMatch(interfaceName, objectPath, interfaces.Keys, rootObject);
        }

        internal static bool IsMatch(string interfaceName, ObjectPath objectPath, ICollection<string> interfaces, IDBusObject rootObject)
        {
            if (rootObject != null && !objectPath.ToString().StartsWith($"{rootObject.ObjectPath}/"))
            {
                return false;
            }

            return interfaces.Contains(interfaceName);
        }
    }
}