using System;
using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ.Core;

namespace Examples
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Hello Bluetooth");

            Task.Run(async () =>
            {
                    var serverContext = new ServerContext();
                    await SampleAdvertisement.RegisterSampleAdvertisement(serverContext);
                    await SampleGattApplication.RegisterGattApplication(serverContext);

                    Console.WriteLine("Press CTRL+C to quit");
                    await Task.Delay(-1);
            }).Wait();
        }
    }
}
