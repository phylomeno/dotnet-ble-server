namespace BleServer.Infrastructure.BlueZ.Gatt
{
    internal class GattObjectFactory
    {
        private int _NumServicesCreated;

        public GattService CreateGattService(GattServiceDescription serviceDescription)
        {
            var serviceProperties = new GattService1Properties()
            {
                UUID = serviceDescription.UUID,
                Primary = serviceDescription.Primary
            };
            var gattService = new GattService(GetNextServiceObjectPath(), serviceProperties);
            return gattService;
        }

        private string GetNextServiceObjectPath()
        {
            var applicationPrefix = ApplicationPrefix();
            return applicationPrefix + $"service{_NumServicesCreated++}";
        }

        private static string ApplicationPrefix()
        {
            return $"/org/bluez/example/";
        }
    }
}