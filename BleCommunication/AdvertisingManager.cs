using BleServer.Infrastructure.BlueZ;
using Tmds.DBus;

namespace BleServer
{
    internal class AdvertisingManager
    {
        public static ILEAdvertisingManager1 GetAdvertisingManager(Connection connection)
        {
            return connection.CreateProxy<ILEAdvertisingManager1>("org.bluez", "/org/bluez/hci0");
        }
    }
}