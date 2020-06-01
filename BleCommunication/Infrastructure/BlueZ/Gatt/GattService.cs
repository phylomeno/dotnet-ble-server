using System.Collections.Generic;
using System.Linq;
using BleServer.Infrastructure.BlueZ.Core;
using Tmds.DBus;

namespace BleServer.Infrastructure.BlueZ.Gatt
{
    internal class GattService : PropertiesBase<GattService1Properties>, IGattService1, IObjectManagerProperties
    {
        private readonly IList<IGattCharacteristic1> _Characteristics = new List<IGattCharacteristic1>();

        public GattService(ObjectPath objectPath, GattService1Properties properties) : base(objectPath, properties)
        {
        }

        public Dictionary<string, Dictionary<string, object>> GetProperties()
        {
            return new Dictionary<string, Dictionary<string, object>>
            {
                {
                    "org.bluez.GattService1", new Dictionary<string, object>
                    {
                        {"UUID", Properties.UUID},
                        {"Primary", Properties.Primary},
                        {"Characteristics", _Characteristics.Select(c => c.ObjectPath).ToArray()}
                    }
                }
            };
        }

        private void AddCharacteristic(IGattCharacteristic1 characteristic)
        {
            _Characteristics.Add(characteristic);
        }
    }
}