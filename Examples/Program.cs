using System;
using System.Threading.Tasks;

using DotnetBleServer.Core;

using Microsoft.Extensions.Logging;

namespace Examples
{
    internal class Program
    {
        private static void Main()
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger("BLEasy");
            logger.LogInformation("console logging");

            Console.WriteLine("Hello Bluetooth");
            Console.ReadKey();

            Task.Run(
                    async () =>
                    {
                        using (var serverContext = new ServerContext(logger))
                        {
                            await serverContext.Connect();
                            await SampleAdvertisement.RegisterSampleAdvertisement(serverContext);
                            await SampleGattApplication.RegisterGattApplication(serverContext);

                            Console.WriteLine("Press CTRL+C to quit");
                            await Task.Delay(-1);
                        }
                    })
                .Wait();
        }
    }
}