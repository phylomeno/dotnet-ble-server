using DotnetBleServer.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Examples
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Hello Bluetooth !");
            Console.WriteLine("Press a key to start (let you attach debugger from Visual Studio using SSH)");
            Console.ReadLine();

            Task.Run(async () =>
            {
                using (ServerContext context = new ServerContext())
                {
                    // Mandatory for BLE Advertisement (connection must be manual) (ie. Tmds.DBus.Connection.RegisterObject methods)
                    await context.Connect();

                    var manager = new BlueZManager(context);

                    var adapters = await manager.GetAdaptersAsync();
                    if (adapters.Count == 0)
                    {
                        throw new Exception("No Bluetooth adapters found.");
                    }

                    var adapter = adapters.First();

                    Console.WriteLine($"Current bluetooth adapter: {adapter.ObjectPath}");

                    // Turn on adapter.
                    await adapter.SetPoweredAsync(true);

                    if (!await adapter.GetPoweredAsync())
                        throw new Exception($"Can't power on adapter {adapter.ObjectPath}");

                    await SampleAdvertisement.RegisterSampleAdvertisement(context, adapter);
                    await SampleGattApplication.RegisterGattApplication(context, adapter);

                    Console.WriteLine("Press Ctrl+C to quit");
                    await Task.Delay(-1);
                }

            }).Wait();
        }
    }
}
