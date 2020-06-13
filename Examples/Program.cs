using System;
using System.Threading.Tasks;
using DotnetBleServer.Core;

namespace Examples
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Hello Bluetooth");
            Console.ReadKey();

            Task.Run(async () =>
            {
                using (var serverContext = new ServerContext())
                {
                    await serverContext.Connect();
                    await SampleAdvertisement.RegisterSampleAdvertisement(serverContext);
                    await SampleGattApplication.RegisterGattApplication(serverContext);

                    Console.WriteLine("Press CTRL+C to quit");
                    await Task.Delay(-1);
                }
            }).Wait();
        }
    }
}
