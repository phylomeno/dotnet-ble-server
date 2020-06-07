using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ;
using BleServer.Infrastructure.BlueZ.Gatt;
using Tmds.DBus;

namespace BleServer
{
    internal class SampleGattApplication
    {
        public static async Task RegisterGattApplication(Connection connection)
        {
            var gattService1Properties = new GattService1Properties
            {
                UUID = "12345678-1234-5678-1234-56789abcdef0",
                Characteristics = new[] {new ObjectPath("/org/bluez/example/service0/characteristic0")},
                Primary = true
            };

            var gattCharacteristic1Properties = new GattCharacteristic1Properties
            {
                UUID = "12345678-1234-5678-1234-56789abcdef1",
                Flags = new[] {"read", "write", "writable-auxiliaries"},
                Service = "/org/bluez/example/service0"
            };
            var gattDescriptor1Properties = new GattDescriptor1Properties
            {
                Characteristic = "/org/bluez/example/service0/characteristic0",
                Value = new[] {(byte) 't'},
                UUID = "12345678-1234-5678-1234-56789abcdef2",
                Flags = new[] {"read", "write"}
            };
            var gab = new GattApplicationBuilder();
            gab
                .AddService(gattService1Properties)
                .WithCharacteristic(gattCharacteristic1Properties, new[] {gattDescriptor1Properties})


            var application = new GattApplication("/");
            await connection.RegisterObjectAsync(application);

            var service = new GattService("/org/bluez/example/service0", gattService1Properties);
            await connection.RegisterObjectAsync(service);

            var characteristic = new GattCharacteristic("/org/bluez/example/service0/characteristic0",
                gattCharacteristic1Properties);
            await connection.RegisterObjectAsync(characteristic);

            var descriptor1 = new GattDescriptor("/org/bluez/example/service0/characteristic0/descriptor0",
                gattDescriptor1Properties);
            await connection.RegisterObjectAsync(descriptor1);

            characteristic.AddDescriptor(descriptor1);

            service.AddCharacteristic(characteristic);

            application.AddService(service);

            var gattManager = connection.CreateProxy<IGattManager1>("org.bluez", "/org/bluez/hci0");
            await gattManager.RegisterApplicationAsync(new ObjectPath("/"), new Dictionary<string, object>());
        }
    }

    internal class GattApplicationBuilder
    {
        IList<GattServiceBuilder> _ServiceBuilders = new List<GattServiceBuilder>();

        public GattServiceBuilder AddService(GattService1Properties gattService1Properties)
        {
            var gattServiceBuilder = new GattServiceBuilder();
            _ServiceBuilders.Add(gattServiceBuilder);
            return gattServiceBuilder;
        }
    }

    internal class GattServiceBuilder
    {
        private readonly IList<Tuple<GattCharacteristic1Properties, GattDescriptor1Properties[]>> _Characteristics =
            new List<Tuple<GattCharacteristic1Properties, GattDescriptor1Properties[]>>();

        public void WithCharacteristic(GattCharacteristic1Properties gattCharacteristic1Properties,
            GattDescriptor1Properties[] gattDescriptor1Properties)
        {
            _Characteristics.Add(Tuple.Create(gattCharacteristic1Properties, gattDescriptor1Properties));
        }
    }
}