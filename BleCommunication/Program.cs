using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ.Advertisements;
using Tmds.DBus;

namespace BleServer
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Hello Bluetooth");

            Task.Run(async () =>
            {
                using (var connection = new Connection(Address.System))
                {
                    await ConnectToDBus(connection);
                    var advertisement = await CreateAdvertisementObject(connection);
                    await RegisterAdvertisement(connection, advertisement);
                    await SampleGattApplication.RegisterGattApplication(connection);

                    Console.WriteLine("Press CTRL+C to quit");
                    await Task.Delay(-1);
                }
            }).Wait();
        }

        private static async Task RegisterAdvertisement(Connection connection, IDBusObject advertisement)
        {
            var advertisingManager = AdvertisingManager.GetAdvertisingManager(connection);

            await advertisingManager.RegisterAdvertisementAsync(advertisement.ObjectPath,
                new Dictionary<string, object>());
            Console.WriteLine("Advertisement registered");
        }

        private static async Task<IDBusObject> CreateAdvertisementObject(Connection connection)
        {
            var advertisement = await SampleAdvertisement.CreateAdvertisement(connection);
            Console.WriteLine("Advertisement created");
            return advertisement;
        }

        private static async Task ConnectToDBus(IConnection connection)
        {
            await connection.ConnectAsync();
        }
    }
}