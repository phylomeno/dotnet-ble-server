using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ.Advertisements;
using Tmds.DBus;

namespace BleServer
{
    public class SampleAdvertisement
    {
        public static async Task<IDBusObject> CreateAdvertisement(Connection connection)
        {
            var advertisement = new Advertisement(new ObjectPath("/org/bluez/example/advertisement0"), new LEAdvertisement1Properties
            {
                Type = "peripheral",
                ServiceUUIDs = new[] { "12345678-1234-5678-1234-56789abcdef0"},
                LocalName = "A",
            });

            await connection.RegisterObjectAsync(advertisement);

            return advertisement;
        }
    }
}