using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotnetBleServer.Core;
using DotnetBleServer.Gatt.BlueZModel;
using Tmds.DBus;

namespace DotnetBleServer.Advertisements
{
    public class AdvertisingManager
    {
        private readonly ServerContext _Context;
        private readonly string _AdapterPath;

        public AdvertisingManager(ServerContext context, string adapterPath)
        {
            _Context = context;
            _AdapterPath = adapterPath;
        }

        public async Task RegisterAdvertisement(Advertisement advertisement)
        {
            await _Context.Connection.RegisterObjectAsync(advertisement);
            Console.WriteLine($"advertisement object {advertisement.ObjectPath} created");

            await GetAdvertisingManager().RegisterAdvertisementAsync(((IDBusObject)advertisement).ObjectPath,
                new Dictionary<string, object>());

            Console.WriteLine($"advertisement {advertisement.ObjectPath} registered in BlueZ advertising manager");
        }

        private ILEAdvertisingManager1 GetAdvertisingManager()
        {
            return _Context.Connection.CreateProxy<ILEAdvertisingManager1>("org.bluez", new ObjectPath(_AdapterPath));
        }

        public async Task CreateAdvertisement(LEAdvertisement1Properties advertisementProperties)
        {
            var advertisement = new Advertisement($"{_AdapterPath}/advertisement0", advertisementProperties);
            await new AdvertisingManager(_Context, _AdapterPath).RegisterAdvertisement(advertisement);
        }
    }
}