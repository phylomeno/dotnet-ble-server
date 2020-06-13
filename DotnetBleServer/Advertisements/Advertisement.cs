using System;
using System.Threading.Tasks;
using DotnetBleServer.Core;

namespace DotnetBleServer.Advertisements
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