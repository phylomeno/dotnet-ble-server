using System;
using BleServer.Infrastructure.BlueZ;
using Tmds.DBus;

namespace BleCommunication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Bluetooth");

            GetAdapterAsync();
            var advertisingManager = GetAdvertisingManager();
        }

        private static IAdapter1 GetAdapterAsync()
        {
            return Connection.System.CreateProxy<IAdapter1>("org.bluez", "/org/bluez/hci0");
        }

        private static ILEAdvertisingManager1 GetAdvertisingManager()
        {
            return Connection.System.CreateProxy<ILEAdvertisingManager1>("org.bluez", "/org/bluez/hci0");
        }
    }
}
