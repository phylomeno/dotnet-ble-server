using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ.Core;
using BleServer.Infrastructure.BlueZ.Gatt;

namespace Examples
{
    internal class SampleGattApplication
    {
        public static async Task RegisterGattApplication(ServerContext serverContext)
        {
            var gattServiceDescription = new GattServiceDescription 
            {
                UUID = "12345678-1234-5678-1234-56789abcdef0",
                Primary = true
            };

            var gattCharacteristicDescription = new GattCharacteristicDescription
            {
                UUID = "12345678-1234-5678-1234-56789abcdef1",
                Flags = new[] {"read", "write", "writable-auxiliaries"},
            };
            var gattDescriptorDescription = new GattDescriptorDescription
            {
                Value = new[] {(byte) 't'},
                UUID = "12345678-1234-5678-1234-56789abcdef2",
                Flags = new[] {"read", "write"}
            };
            var gab = new GattApplicationBuilder();
            gab
                .AddService(gattServiceDescription)
                .WithCharacteristic(gattCharacteristicDescription, new[] {gattDescriptorDescription});

            await new GattApplicationManager(serverContext).RegisterGattApplication(gab.BuildServiceDescriptions());
        }
    }
}