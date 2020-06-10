using System.Collections.Generic;
using System.Linq;
using BleServer.Infrastructure.BlueZ.Core;

namespace BleServer.Infrastructure.BlueZ.Gatt
{
    internal class GattService : PropertiesBase<GattService1Properties>, IGattService1, IObjectManagerProperties
    {
        private readonly IList<GattCharacteristic> _Characteristics = new List<GattCharacteristic>();

        public IEnumerable<GattCharacteristic> Characteristics => _Characteristics;

        public GattService(GattService1Properties properties) : base(properties)
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

        public GattCharacteristic AddCharacteristic(GattCharacteristic1Properties characteristic)
        {
            var gattCharacteristic = new GattCharacteristic(null, characteristic);
            var characteristicPath= ObjectPath + "/characteristic" + _Characteristics.Count;
            gattCharacteristic.SetObjectPath(characteristicPath);
            _Characteristics.Add(gattCharacteristic);
            return gattCharacteristic;
        }
    }
}