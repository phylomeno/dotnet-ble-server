using System;
using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ.Core;
using Tmds.DBus;

namespace BleServer.Infrastructure.BlueZ.Advertisements
{
    public class Advertisement : PropertiesBase<LEAdvertisement1Properties>, ILEAdvertisement1
    {
        public Advertisement(ObjectPath objectPath, LEAdvertisement1Properties properties) : base(objectPath,
            properties)
        {
        }

        public Task ReleaseAsync()
        {
            throw new NotImplementedException();
        }
    }
}