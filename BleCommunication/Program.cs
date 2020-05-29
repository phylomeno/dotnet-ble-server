using System;
using System.Collections.Generic;
using BleServer.Infrastructure.BlueZ;
using Tmds.DBus;

namespace BleCommunication
{
    public class ExampleAdvertisement : IDBusObject
    {
        public string Type { get; set; }
        public string[] ServiceUUIDs { get; set; }
        public IDictionary<string, object> ManufacturerData { get; set; }
        public string[] SolicitUUIDs { get; set; }

        public IDictionary<string, object> ServiceData { get; set; }

        public bool IncludeTxPower { get; set; }

        public string LocalName { get; set; }
        public ObjectPath ObjectPath { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello Bluetooth");

            var advertisement = CreateAdvertisement();
            var advertisingManager = GetAdvertisingManager();

            advertisingManager.RegisterAdvertisementAsync(advertisement.ObjectPath, new Dictionary<string, object>());
        }

        private static IDBusObject CreateAdvertisement()
        {
            var advertisement = new ExampleAdvertisement
            {
                ObjectPath = new ObjectPath("/org/bluez/example/advertisement"),
                Type = "peripheral",
                ServiceUUIDs = new[] {"180D", "180F"},
                ManufacturerData = {{"0xffff", new[] {"0x00", "0x01", "0x02", "0x03"}}},
                ServiceData = {{"9999", new[] {"0x00", "0x01", "0x02", "0x03"}}},
                IncludeTxPower = true,
                LocalName = "TestAdvertisement"
            };

            Connection.System.RegisterObjectAsync(advertisement);

            return advertisement;
        }

        private static ILEAdvertisingManager1 GetAdvertisingManager()
        {
            return Connection.System.CreateProxy<ILEAdvertisingManager1>("org.bluez", "/org/bluez/hci0");
        }
    }
}