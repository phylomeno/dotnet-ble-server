using System;
using System.Threading.Tasks;

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
                    var serverContext = new ServerContext();
                    await SampleAdvertisement.RegisterSampleAdvertisement(serverContext);
                    await SampleGattApplication.RegisterGattApplication(serverContext);

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
