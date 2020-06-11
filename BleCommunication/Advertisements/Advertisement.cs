using System;
using System.Threading.Tasks;
using BleServer.Core;

namespace BleServer.Advertisements
{
    public class Advertisement : PropertiesBase<AdvertisementProperties>, ILEAdvertisement1
    {
        public Advertisement(string objectPath, AdvertisementProperties properties) : base(objectPath, properties)
        {
        }

        public Task ReleaseAsync()
        {
            throw new NotImplementedException();
        }
    }
}