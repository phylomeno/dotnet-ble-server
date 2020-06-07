using System.Collections.Generic;
using BleServer.Infrastructure.BlueZ;

namespace BleServer.Infrastructure.Bluez.Gatt
{
    public class CharacteristicDescription
    {
        public GattCharacteristic1Properties CharacteristicsProperties { get; }

        public IList<GattDescriptor1Properties> Descriptors { get; }

        public CharacteristicDescription(GattCharacteristic1Properties characteristicsProperties, IList<GattDescriptor1Properties> descriptors)
        {
            CharacteristicsProperties = characteristicsProperties;
            Descriptors = descriptors;
        }
    }
}