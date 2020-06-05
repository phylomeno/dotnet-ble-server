using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ.Core;
using Tmds.DBus;

namespace BleServer.Infrastructure.BlueZ.Advertisements
{
    public class AdvertisingManager
    {
        private readonly ServerContext _Context;

        public AdvertisingManager(ServerContext context)
        {
            _Context = context;
        }

        public async Task RegisterAdvertisement(Advertisement advertisement)
        {
            await _Context.Connection.RegisterObjectAsync(advertisement);
            Console.WriteLine($"advertisement object {advertisement.ObjectPath} created");

            await GetAdvertisingManager().RegisterAdvertisementAsync(((IDBusObject) advertisement).ObjectPath,
                new Dictionary<string, object>());

            Console.WriteLine($"advertisement {advertisement.ObjectPath} registered in BlueZ advertising manager");
        }

        private ILEAdvertisingManager1 GetAdvertisingManager()
        {
            return _Context.Connection.CreateProxy<ILEAdvertisingManager1>("org.bluez", "/org/bluez/hci0");
        }
    }
}