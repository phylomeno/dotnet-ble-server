using System.Collections.Generic;
using BleServer.Infrastructure.Bluez.Gatt;

namespace BleServer.Infrastructure.BlueZ.Gatt
{
    internal class GattServiceBuilder
    {
        private readonly GattService1Properties _Service1Properties;
        private readonly IList<CharacteristicDescription> _Characteristics =
            new List<CharacteristicDescription>();

        public GattServiceBuilder(GattService1Properties service1Properties)
        {
            _Service1Properties = service1Properties;
        }

        public void WithCharacteristic(GattCharacteristic1Properties gattCharacteristic1Properties,
            GattDescriptor1Properties[] gattDescriptor1Properties)
        {
            _Characteristics.Add(new CharacteristicDescription(gattCharacteristic1Properties, gattDescriptor1Properties));
        }

        public ServiceDescription BuildServiceDescription()
        {
            return new ServiceDescription(_Service1Properties, _Characteristics);
        }
    }
}