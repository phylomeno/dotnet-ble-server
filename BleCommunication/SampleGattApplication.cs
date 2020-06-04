using System.Collections.Generic;
using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ;
using BleServer.Infrastructure.BlueZ.Gatt;
using Tmds.DBus;

namespace BleServer
{
    internal class SampleGattApplication
    {
        public static async Task RegisterGattApplication(Connection connection)
        {
            var application = new GattApplication("/");
            await connection.RegisterObjectAsync(application);

            var service = new GattService("/org/bluez/example/service0", new GattService1Properties
            {
                UUID = "12345678-1234-5678-1234-56789abcdef0",
                Characteristics = new[] {new ObjectPath("/org/bluez/example/service0/characteristic0")},
                Primary = true
            });
            await connection.RegisterObjectAsync(service);

            var characteristic = new GattCharacteristic("/org/bluez/example/service0/characteristic0", new GattCharacteristic1Properties
            {
                UUID = "12345678-1234-5678-1234-56789abcdef1",
                Flags = new[] {"read", "write", "writable-auxiliaries"},
                Service = "/org/bluez/example/service0"
            });
            await connection.RegisterObjectAsync(characteristic);

            var descriptor1 = new GattDescriptor("/org/bluez/example/service0/characteristic0/descriptor0", new GattDescriptor1Properties
            {
                Characteristic = "/org/bluez/example/service0/characteristic0",
                Value = new[] {(byte) 't'},
                UUID = "12345678-1234-5678-1234-56789abcdef2",
                Flags = new[] {"read", "write"}
            });
            await connection.RegisterObjectAsync(descriptor1);
            
            characteristic.AddDescriptor(descriptor1);

            service.AddCharacteristic(characteristic);

            application.AddService(service);

            var gattManager = connection.CreateProxy<IGattManager1>("org.bluez", "/org/bluez/hci0");
            await gattManager.RegisterApplicationAsync(new ObjectPath("/"), Options: new Dictionary<string, object>());

        }
    }
}