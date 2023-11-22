using DotnetBleServer.Core;
using DotnetBleServer.Utilities;
using System;
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
                using (var serverContext = new ServerContext())
                {
                    await serverContext.Connect();

                    string adapterPath = (await AdapterExtensions.GetBluetoothAdapters(serverContext.Connection))[0];

                    Console.WriteLine($"Current bluetooth adapter: {adapterPath}");

                    // Retrieve adapter
                    IAdapter1 BLEAdapter = serverContext.Connection.CreateProxy<IAdapter1>("org.bluez", adapterPath);

                    // Turn on adapter.
                    await Adapter1Extensions.SetPoweredAsync(BLEAdapter, true);

                    if (!await Adapter1Extensions.GetPoweredAsync(BLEAdapter))
                        throw new Exception($"Can't power on adapter {adapterPath}");

                    await SampleAdvertisement.RegisterSampleAdvertisement(serverContext, adapterPath);
                    await SampleGattApplication.RegisterGattApplication(serverContext, adapterPath);

                    Console.WriteLine("Press Ctrl+C to quit");
                    await Task.Delay(-1);
                }
            }).Wait();
        }
    }
}
