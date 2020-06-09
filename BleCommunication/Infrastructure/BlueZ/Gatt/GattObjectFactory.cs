namespace BleServer.Infrastructure.BlueZ.Gatt
{
    internal class GattObjectFactory
    {
        public GattService CreateGattService(GattServiceDescription serviceDescription)
        {
            var serviceProperties = new GattService1Properties
            {
                UUID = serviceDescription.UUID,
                Primary = serviceDescription.Primary
            };
            return new GattService(serviceProperties);
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