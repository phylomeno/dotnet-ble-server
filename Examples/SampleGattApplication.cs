using DotnetBleServer.Core;
using DotnetBleServer.Gatt;
using DotnetBleServer.Gatt.Description;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    internal static class SampleGattApplication
    {
        public static async Task RegisterGattApplication(ServerContext serverContext, string adapterPath)
        {
            var gattServiceDescription = new GattServiceDescription
            {
                UUID = "12345678-1234-5678-1234-56789abcdef0",
                Primary = true
            };

            var gattCharacteristicDescription = new MyCustomGattCharacteristicDescription
            {
                UUID = "12345678-1234-5678-1234-56789abcdef1",
                Flags = CharacteristicFlags.Read | CharacteristicFlags.Write | CharacteristicFlags.WritableAuxiliaries | CharacteristicFlags.Notify
            };

            var gattDescriptorDescription = new GattDescriptorDescription
            {
                Value = new[] { (byte)'t' },
                UUID = "12345678-1234-5678-1234-56789abcdef2",
                Flags = new[] { "read", "write" }
            };

            var gab = new GattApplicationBuilder();
            gab
                .AddService(gattServiceDescription)
                .WithCharacteristic(gattCharacteristicDescription, new[] { gattDescriptorDescription });

            await new GattApplicationManager(serverContext, adapterPath).RegisterGattApplication(gab.BuildServiceDescriptions());
        }
    }

    class MyCustomGattCharacteristicDescription : GattCharacteristicDescription
    {
        public override Task WriteValueAsync(byte[] value)
        {
            Console.WriteLine("Writing value");

            string message = new string(Encoding.ASCII.GetChars(value));

            Console.WriteLine($"Value received: {message}");

            return base.WriteValueAsync(value);
        }

        public override Task<byte[]> ReadValueAsync()
        {
            Console.WriteLine("Reading value");
            return Task.FromResult(Encoding.ASCII.GetBytes("Some data"));
        }
    }
}