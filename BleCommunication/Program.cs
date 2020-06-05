using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ.Advertisements;
using BleServer.Infrastructure.BlueZ.Core;
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
                    await SampleAdvertisement.RegisterSampleAdvertisement(new ServerContext());
                    await SampleGattApplication.RegisterGattApplication(connection);

                    Console.WriteLine("Press CTRL+C to quit");
                    await Task.Delay(-1);
                }
            }).Wait();
        }

        private static async Task ConnectToDBus(IConnection connection)
        {
            await connection.ConnectAsync();
        }
    }
}