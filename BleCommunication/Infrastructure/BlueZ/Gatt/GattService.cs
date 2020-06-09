using System.Collections.Generic;
using System.Linq;
using BleServer.Infrastructure.BlueZ.Core;
using Tmds.DBus;

namespace BleServer.Infrastructure.BlueZ.Gatt
{
    internal class GattService : PropertiesBase<GattService1Properties>, IGattService1, IObjectManagerProperties
    {
        public IList<GattCharacteristic> Characteristics { get; } = new List<GattCharacteristic>();

        public GattService(ObjectPath objectPath, GattService1Properties properties) : base(objectPath, properties)
        {
        }

        public IDictionary<string, IDictionary<string, object>> GetProperties()
        {
            return new Dictionary<string, IDictionary<string, object>>
            {
                {
                    "org.bluez.GattService1", new Dictionary<string, object>
                    {
                        {"UUID", Properties.UUID},
                        {"Primary", Properties.Primary},
                        {"Characteristics", Characteristics.Select(c => c.ObjectPath).ToArray()}
                    }
                }
            };
        }

        public void AddCharacteristic(GattCharacteristic characteristic)
        {
            Characteristics.Add(characteristic);
        }
    }
}