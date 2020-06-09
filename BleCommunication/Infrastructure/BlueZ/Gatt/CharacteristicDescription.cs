using System.Collections.Generic;

namespace BleServer.Infrastructure.BlueZ.Gatt
{
    public class CharacteristicDescription
    {
        public GattCharacteristicDescription CharacteristicsProperties { get; }

        public IList<GattDescriptorDescription> Descriptors { get; }

        public CharacteristicDescription(GattCharacteristicDescription characteristicsProperties, IList<GattDescriptorDescription> descriptors)
        {
            CharacteristicsProperties = characteristicsProperties;
            Descriptors = descriptors;
        }
    }
}