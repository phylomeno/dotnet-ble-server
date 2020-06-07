using System.Collections.Generic;
using BleServer.Infrastructure.BlueZ;

namespace BleServer.Infrastructure.Bluez.Gatt
{
    public class CharacteristicDescription
    {
        private GattCharacteristic1Properties _CharacteristicsProperties;

        private IList<GattDescriptor1Properties> _Descriptors;

        public CharacteristicDescription(GattCharacteristic1Properties characteristicsProperties, IList<GattDescriptor1Properties> descriptors)
        {
            _CharacteristicsProperties = characteristicsProperties;
            _Descriptors = descriptors;
        }
    }
}