using System;
using System.Threading.Tasks;
using DotnetBleServer.Core;
using DotnetBleServer.Utilities;

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

                    // Trying to turn on the bluetooth device within the code.
                    Console.WriteLine($"Trying to turn on the bluetooth adapter: {adapterPath}");

                    IAdapter1 BLEAdapter = serverContext.Connection.CreateProxy<IAdapter1>("org.bluez", adapterPath);

                    // Uncomment if needed.
                    //await Adapter1Extensions.SetAliasAsync(BLEAdapter, "Debian (raspberry Pi)");
                    //await Adapter1Extensions.SetDiscoverableAsync(BLEAdapter, true);

                    await Adapter1Extensions.SetPoweredAsync(BLEAdapter, true);

                    if (!await Adapter1Extensions.GetPoweredAsync(BLEAdapter))
                        throw new Exception($"Can't power on adapter {adapterPath}");

                    await SampleAdvertisement.RegisterSampleAdvertisement(serverContext, adapterPath);
                    await SampleGattApplication.RegisterGattApplication(serverContext, adapterPath);

                    Console.WriteLine("Press ENTER to quit");
                    Console.ReadLine();

                    // Power off adapter (Bluetooth is no more Discoverable)
                    await Adapter1Extensions.SetPoweredAsync(BLEAdapter, false);

                }
            }).Wait();
        }
    }
}
