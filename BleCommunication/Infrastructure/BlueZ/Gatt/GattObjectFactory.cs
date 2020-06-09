namespace BleServer.Infrastructure.BlueZ.Gatt
{
    internal class GattObjectFactory
    {
        public GattService CreateGattService(GattServiceDescription serviceDescription)
        {
            var serviceProperties = new GattService1Properties()
            {
                UUID = serviceDescription.UUID,
                Primary = serviceDescription.Primary
            };
            var gattService = new GattService("/org/bluez/example/service0", serviceProperties);
            return gattService;
        }
    }
}