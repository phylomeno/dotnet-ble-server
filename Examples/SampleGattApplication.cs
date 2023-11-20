using System;
using System.Text;
using System.Threading.Tasks;
using DotnetBleServer.Core;
using DotnetBleServer.Gatt;
using DotnetBleServer.Gatt.Description;

namespace Examples
{
    internal class SampleGattApplication
    {
        public static async Task RegisterGattApplication(ServerContext serverContext, string adapterPath)
        {
            var gattServiceDescription = new GattServiceDescription
            {
                UUID = "12345678-1234-5678-1234-56789abcdef0",
                Primary = true
            };

            var gattCharacteristicDescription = new GattCharacteristicDescription
            {
                CharacteristicSource = new ExampleCharacteristicSource(),
                UUID = "12345678-1234-5678-1234-56789abcdef1",
                Flags = CharacteristicFlags.Read | CharacteristicFlags.Write | CharacteristicFlags.WritableAuxiliaries
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

            await new GattApplicationManager(serverContext, adapterPath).RegisterGattApplication(gab.BuildServiceDescriptions());
        }

        internal class ExampleCharacteristicSource : ICharacteristicSource
        {
            public Task WriteValueAsync(byte[] value)
            {
                Console.WriteLine("Writing value");
                return Task.Run(() => Console.WriteLine(Encoding.ASCII.GetChars(value)));
            }

            public Task<byte[]> ReadValueAsync()
            {
                Console.WriteLine("Reading value");
                return Task.FromResult(Encoding.ASCII.GetBytes("Hello BLE"));
            }
        }
    }
}