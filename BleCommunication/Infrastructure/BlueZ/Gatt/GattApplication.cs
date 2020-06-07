using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ.Core;
using Tmds.DBus;

namespace BleServer.Infrastructure.BlueZ.Gatt
{
    public class GattApplicationManager
    {
        ServerContext _ServerContext;

        public GattApplicationManager(ServerContext serverContext)
        {
            _ServerContext = serverContext;
        }

        public RegisterGattApplication()

    }
    [DBusInterface("org.freedesktop.DBus.ObjectManager")]
    public class GattApplication : IObjectManager
    {
        private readonly IList<GattService> _GattServices = new List<GattService>();

        public GattApplication(ObjectPath objectPath)
        {
            ObjectPath = objectPath;
        }

        public ObjectPath ObjectPath { get; }

        public void AddService(GattService gattService)
        {
            _GattServices.Add(gattService);
        }

        public Task<IDictionary<ObjectPath, IDictionary<string, IDictionary<string, object>>>> GetManagedObjectsAsync()
        {
            IDictionary<ObjectPath, IDictionary<string, IDictionary<string, object>>> result = new Dictionary<ObjectPath, IDictionary<string, IDictionary<string, object>>>();
            foreach (var service in _GattServices)
            {
                result[service.ObjectPath] = service.GetProperties();
                foreach (var characteristic in service.Characteristics)
                {
                    result[characteristic.ObjectPath] = characteristic.GetProperties();
                    foreach (var descriptor in characteristic.Descriptors)
                    {
                        result[descriptor.ObjectPath] = descriptor.GetProperties();
                    }
                }
            }

            return Task.FromResult(result);
        }
    }
}