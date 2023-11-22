using DotnetBleServer.Core;
using DotnetBleServer.Gatt;
using DotnetBleServer.Utilities;
using System;
using System.Text;
using System.Text.Json;
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

                    // Uncomment if needed.
                    //await Adapter1Extensions.SetAliasAsync(BLEAdapter, "Debian (raspberry Pi)");
                    //await Adapter1Extensions.SetDiscoverableAsync(BLEAdapter, true);
                    //await Adapter1Extensions.SetPairableAsync(BLEAdapter, true);

                    // Turn on adapter.
                    await Adapter1Extensions.SetPoweredAsync(BLEAdapter, true);

                    if (!await Adapter1Extensions.GetPoweredAsync(BLEAdapter))
                        throw new Exception($"Can't power on adapter {adapterPath}");

                    await SampleAdvertisement.RegisterSampleAdvertisement(serverContext, adapterPath);

                    var source = new ExampleCharacteristicSource() { ExecuteMeDuringWrite = CommandReceived };
                    await SampleGattApplication.RegisterGattApplication(serverContext, adapterPath, source);

                    Console.WriteLine("Press Ctrl+C to quit");
                    await Task.Delay(-1);
                }
            }).Wait();
        }

        private static Task<string> CommandReceived(YourDTOClass parameter)
        {
            Console.WriteLine("Executing some code...");
            Console.WriteLine(parameter.SomeProperty);

            return Task.FromResult(JsonSerializer.Serialize(new YourDTOClass() { SomeProperty = "Whatever you want to execute during Write, before returning new value to Read" }));
        }

        class YourDTOClass
        {
            public string SomeProperty { get; set; }
        }

        class ExampleCharacteristicSource : ICharacteristicSource
        {
            public Func<YourDTOClass, Task<string>> ExecuteMeDuringWrite { get; set; }

            private string _data;

            public async Task WriteValueAsync(byte[] value)
            {
                Console.WriteLine("Writing value");

                string message = new string(Encoding.ASCII.GetChars(value));

                Console.WriteLine($"Value received: {message}");

                await DoSomeStuff(message);
            }

            public Task<byte[]> ReadValueAsync()
            {
                Console.WriteLine("Reading value");
                return Task.FromResult(Encoding.ASCII.GetBytes(_data));
            }

            /// <summary>
            /// Could be used to notify or execute code according to received value from client
            /// </summary>
            /// <param name="receivedValue"></param>
            /// <returns></returns>
            private async Task DoSomeStuff(string receivedValue)
            {
                // Considering the receivedValue is serialized data as { SomeProperty = \"data\" }
                var deserializedObject = JsonSerializer.Deserialize<YourDTOClass>(receivedValue);

                string serializedData = await ExecuteMeDuringWrite(deserializedObject);
                _data = serializedData;
            }
        }
    }
}
