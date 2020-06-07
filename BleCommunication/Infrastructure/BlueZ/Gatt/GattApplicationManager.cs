using System.Collections.Generic;
using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ.Core;

namespace BleServer.Infrastructure.BlueZ.Gatt
{
    public class GattApplicationManager
    {
        ServerContext _ServerContext;

        public GattApplicationManager(ServerContext serverContext)
        {
            _ServerContext = serverContext;
        }

        public async Task RegisterGattApplication(IEnumerable<ServiceDescription> gattServiceDescriptions)
        {

            var application = new GattApplication("/");
            await connection.RegisterObjectAsync(application);

            var service = new GattService("/org/bluez/example/service0", gattService1Properties);
            await connection.RegisterObjectAsync(service);

            var characteristic = new GattCharacteristic("/org/bluez/example/service0/characteristic0",
                gattCharacteristic1Properties);
            await connection.RegisterObjectAsync(characteristic);

            var descriptor1 = new GattDescriptor("/org/bluez/example/service0/characteristic0/descriptor0",
                gattDescriptor1Properties);
            await connection.RegisterObjectAsync(descriptor1);

            characteristic.AddDescriptor(descriptor1);

            service.AddCharacteristic(characteristic);

            application.AddService(service);

            var gattManager = connection.CreateProxy<IGattManager1>("org.bluez", "/org/bluez/hci0");
            await gattManager.RegisterApplicationAsync(new ObjectPath("/"), new Dictionary<string, object>());
        }


    }
}