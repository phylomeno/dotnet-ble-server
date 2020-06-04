using System.Collections.Generic;
using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ.Advertisements;
using Tmds.DBus;

namespace BleServer
{
    public class SampleAdvertisement
    {
        public static async Task<IDBusObject> CreateAdvertisement(Connection connection)
        {
            var advertisement = new Advertisement(new Dictionary<string, object>
            {
                {"Type", "peripheral"},
                {"ServiceUUIDs", new[] {"12345678-1234-5678-1234-56789abcdef0"}},
                // {
                //     "ManufacturerData",
                //     new Dictionary<string, object> {{"0xffff", new[] {"0x00", "0x01", "0x02", "0x03"}}}
                // },
                //{"SolicitUUIDs", new[] {"180D", "180F"}},
                // {"ServiceData", new Dictionary<string, object> {{"9999", new[] {"0x00", "0x01", "0x02", "0x03"}}}},
                {"Includes", new string[0]},
                {"LocalName", "A"},
                // {"Appearance", 0},
                //{"Duration", (ushort) 10}
                // {"Timeout", 1000}
            });

            await connection.RegisterObjectAsync(advertisement);

            return advertisement;
        }
    }
}