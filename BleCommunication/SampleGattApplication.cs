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
            var descriptor = new GattDescriptor("/descriptors/descriptor0", new GattDescriptor1Properties
            {
                Characteristic = "/characteristics/characteristic0",
                Value = new[] {(byte) 't'},
                UUID = "12345678-1234-5678-1234-56789abcdef2",
                Flags = new[] {"read", "write"}
            });

            var characteristic = new GattCharacteristic("/characteristics/characteristic0", new GattCharacteristic1Properties
            {
                UUID = "12345678-1234-5678-1234-56789abcdef1",
                Flags = new[] {"read", "write"},
                Service = "/services/service0"
            });

            characteristic.AddDescriptor(descriptor);

            var service = new GattService("/services/service0", new GattService1Properties
            {
                UUID = "12345678-1234-5678-1234-56789abcdef0",
                Characteristics = new[] {new ObjectPath("/characteristics/characteristic0")}
            });

            service.AddCharacteristic(characteristic);

            var application = new GattApplication("/");

            await connection.RegisterObjectsAsync(new IDBusObject[] {characteristic, descriptor, service, application});
        }
    }
}