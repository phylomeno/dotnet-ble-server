using System;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DotnetBleServer.Core;
using DotnetBleServer.Gatt;
using DotnetBleServer.Gatt.Description;

namespace Examples
{
    internal static class SampleGattApplication
    {
        public static async Task RegisterGattApplication(ServerContext serverContext, string adapterPath, ICharacteristicSource source)
        {
            var gattServiceDescription = new GattServiceDescription
            {
                UUID = "E11EFB5D-E8BD-46B5-814A-1C4322F80067",
                Primary = true
            };

            var gattCharacteristicDescription = new GattCharacteristicDescription
            {
                CharacteristicSource = source,
                UUID = "3929F0D8-D461-43B0-BCF3-DF228CDD4A35",
                Flags = CharacteristicFlags.Read | CharacteristicFlags.Write | CharacteristicFlags.WritableAuxiliaries | CharacteristicFlags.Notify
            };

            var gattDescriptorDescription = new GattDescriptorDescription
            {
                Value = new[] {(byte) 't'},
                UUID = "3929F0D8-D461-43B0-BCF3-DF228CDD4A35",
                Flags = new[] {"read", "write"}
            };

            var gab = new GattApplicationBuilder(); 
            gab
                .AddService(gattServiceDescription)
                .WithCharacteristic(gattCharacteristicDescription, new[] {gattDescriptorDescription});
            
            await new GattApplicationManager(serverContext, adapterPath).RegisterGattApplication(gab.BuildServiceDescriptions());
        }
    }
}