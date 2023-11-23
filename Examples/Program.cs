using DotnetBleServer.Core;
using System;
using System.Threading.Tasks;

namespace Examples
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Hello Bluetooth !");
            Console.WriteLine("Press a key to start (it's time to attach debugger from Visual Studio using SSH)");
            Console.ReadLine();

            Task.Run(async () =>
            {
                using (var context = new ServerContext())
                {
                    await context.ConnectAndSetDefaultAdapter();

                    Console.WriteLine($"Using bluetooth adapter: {context.Adapter.ObjectPath}");
                    Console.WriteLine("(switch adapter using context.GetAdapters() and then set context.Adapter)");

                    // Turn on adapter.
                    await context.Adapter.SetPoweredAsync(true);

                    if (!await context.Adapter.GetPoweredAsync())
                        throw new Exception($"Can't power on adapter {context.Adapter.ObjectPath}");

                    await SampleAdvertisement.RegisterSampleAdvertisement(context);
                    await SampleGattApplication.RegisterGattApplication(context);

                    Console.WriteLine("Press Ctrl+C to quit");
                    await Task.Delay(-1);
                }

            }).Wait();
        }
    }
}