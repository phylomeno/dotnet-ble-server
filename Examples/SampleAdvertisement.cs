using DotnetBleServer.Advertisements;
using DotnetBleServer.Core;
using DotnetBleServer.Gatt.BlueZModel;
using System;
using System.Threading.Tasks;

namespace Examples
{
    public class SampleAdvertisement
    {
        public static async Task RegisterSampleAdvertisement(ServerContext serverContext, string adapterPath)
        {
            var advertisementProperties = new LEAdvertisement1Properties
            {
                Type = "peripheral",
                ServiceUUIDs = new[] { "12345678-1234-5678-1234-56789abcdef0" },
                LocalName = "DISPLAYED NAME",
                // Check bluetooth spec here: https://www.bluetooth.com/specifications/an/ (Computer appearance is 64 => convert hex value to uint: 0x0080 = 64)
                // I don't know why, for some reasons, changing appearance is not always working.
                Appearance = (ushort)Convert.ToUInt32("0x0080", 16),
                // Set to false if Type = "broadcast"
                Discoverable = true,
                IncludeTxPower = true,
            };

            await new AdvertisingManager(serverContext, adapterPath).CreateAdvertisement(advertisementProperties);
        }
    }
}