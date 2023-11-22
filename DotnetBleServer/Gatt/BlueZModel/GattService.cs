using System.Collections.Generic;
using System.Linq;
using DotnetBleServer.Core;

namespace DotnetBleServer.Gatt.BlueZModel
{
    internal class GattService : PropertiesBase<GattService1Properties>, IGattService1, IObjectManagerProperties
    {
        private readonly IList<GattCharacteristic> _Characteristics = new List<GattCharacteristic>();

        public IEnumerable<GattCharacteristic> Characteristics => _Characteristics;

        public GattService(string objectPath, GattService1Properties properties) : base(objectPath, properties)
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

        public GattCharacteristic AddCharacteristic(GattCharacteristic1Properties characteristic, ICharacteristic characteristicSource)
        {
            characteristic.Service = ObjectPath;
            var characteristicPath = NextCharacteristicPath();

            var gattCharacteristic = new GattCharacteristic(characteristicPath, characteristic, characteristicSource);
            _Characteristics.Add(gattCharacteristic);

            Properties.Characteristics = Properties.Characteristics.Append(characteristicPath).ToArray();
            
            return gattCharacteristic;
        }

        private string NextCharacteristicPath()
        {
            return ObjectPath + "/characteristic" + _Characteristics.Count;
        }
    }
}