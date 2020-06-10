using System.Threading.Tasks;
using BleServer.Advertisements;
using Tmds.DBus;

namespace BleServer.Core
{
    public class ServerContext
    {
        public ServerContext()
        {
            Connection = new Connection(Address.System);
            Connection.ConnectAsync();
        }

        public Connection Connection { get; }

        public async Task CreateAdvertisement(AdvertisementProperties advertisementProperties, string objectPath)
        {
            var advertisement = new Advertisement(objectPath, advertisementProperties);

            await new AdvertisingManager(this).RegisterAdvertisement(advertisement);
        }
    }
}