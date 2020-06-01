using System.Collections.Generic;
using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ.Core;
using Tmds.DBus;

namespace BleServer.Infrastructure.BlueZ.Gatt
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

        public Dictionary<string, Dictionary<string, object>> GetProperties()
        {
            return new Dictionary<string, Dictionary<string, object>>()
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