using System.Collections.Generic;
using System.Threading.Tasks;
using DotnetBleServer.Core;
using Tmds.DBus;

namespace DotnetBleServer.Gatt.BlueZModel
{
    internal class GattDescriptor : PropertiesBase<GattDescriptor1Properties>, IGattDescriptor1, IObjectManagerProperties
    {
        public GattDescriptor(ObjectPath objectPath, GattDescriptor1Properties gattDescriptor1Properties)
            : base(objectPath, gattDescriptor1Properties)
        {

        }

        public Task<byte[]> ReadValueAsync()
        {
            return Task.FromResult(Properties.Value);
        }

        public IDictionary<string, IDictionary<string, object>> GetProperties()
        {
            return new Dictionary<string, IDictionary<string, object>>()
            {
                {
                    "org.bluez.GattDescriptor1", new Dictionary<string, object>
                    {
                        { "Characteristic", Properties.Characteristic },
                        { "UUID", Properties.UUID },
                        { "Flags", Properties.Flags }
                    }
                }
            };
        }
    }
}