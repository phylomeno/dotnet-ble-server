using DotnetBleServer.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tmds.DBus;

namespace DotnetBleServer.Utilities
{
    public class AdapterExtensions
    {
        private const string BluezPrependDevicePath = "/org/bluez/";
        public async static Task<List<string>> GetBluetoothAdapters(Connection dbusConnection)
        {
            List<string> bluezDeviceList = new List<string>();
            foreach (var itemDict in await dbusConnection.CreateProxy<IObjectManager>("org.bluez", "/").GetManagedObjectsAsync())
            {
                string keyString = itemDict.Key.ToString();
                if (keyString.Length < BluezPrependDevicePath.Length)
                    continue;

                bluezDeviceList.Add(keyString);
            }

            return bluezDeviceList;
        }
    }
}