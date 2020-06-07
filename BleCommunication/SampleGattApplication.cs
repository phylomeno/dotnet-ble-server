using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ;
using BleServer.Infrastructure.BlueZ.Core;
using BleServer.Infrastructure.BlueZ.Gatt;

namespace BleServer
{
    internal class SampleGattApplication
    {
        public static async Task RegisterGattApplication(ServerContext serverContext)
        {
            var gattService1Properties = new GattService1Properties
            {
                UUID = "12345678-1234-5678-1234-56789abcdef0",
                Primary = true
            };

            var gattCharacteristic1Properties = new GattCharacteristic1Properties
            {
                UUID = "12345678-1234-5678-1234-56789abcdef1",
                Flags = new[] {"read", "write", "writable-auxiliaries"},
            };
            var gattDescriptor1Properties = new GattDescriptor1Properties
            {
                Value = new[] {(byte) 't'},
                UUID = "12345678-1234-5678-1234-56789abcdef2",
                Flags = new[] {"read", "write"}
            };
            var gab = new GattApplicationBuilder();
            gab
                .AddService(gattService1Properties)
                .WithCharacteristic(gattCharacteristic1Properties, new[] {gattDescriptor1Properties});

            await new GattApplicationManager(serverContext).RegisterGattApplication(gab.BuildServiceDescriptions());
        }
    }
}