namespace BleServer.Infrastructure.BlueZ.Gatt
{
    internal class GattObjectFactory
    {
        public GattService1Properties CreateGattService(GattServiceDescription serviceDescription)
        {
            return new GattService1Properties
            {
                UUID = serviceDescription.UUID,
                Primary = serviceDescription.Primary
            };
        }

        public GattCharacteristic CreateGattCharacteristic(GattCharacteristicDescription characteristic)
        {
            var characteristicProperties = new GattCharacteristic1Properties
            {
                UUID = characteristic.UUID,
                Flags = characteristic.Flags
            };

            return new GattCharacteristic(null,
                characteristicProperties);
        }
    }
}