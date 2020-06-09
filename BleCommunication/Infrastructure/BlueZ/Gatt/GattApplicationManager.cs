using System.Collections.Generic;
using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ.Core;
using Tmds.DBus;

namespace BleServer.Infrastructure.BlueZ.Gatt
{
    public class GattApplicationManager
    {
        private readonly ServerContext _ServerContext;

        public GattApplicationManager(ServerContext serverContext)
        {
            _ServerContext = serverContext;
        }

        public async Task RegisterGattApplication(IEnumerable<GattServiceDescription> gattServiceDescriptions)
        {
            var gattObjectFactory = new GattObjectFactory();
            var application = new GattApplication("/");
            await _ServerContext.Connection.RegisterObjectAsync(application);

            foreach (var serviceDescription in gattServiceDescriptions)
            {
                // todo dynamically create object path
                var gattService = gattObjectFactory.CreateGattService(serviceDescription);
                await _ServerContext.Connection.RegisterObjectAsync(gattService);

                var characteristicObjectPaths = new List<ObjectPath>();
                application.AddService(gattService);
                /*
                foreach (var characteristic in serviceDescription.GattCharacteristicDescriptions)
                {
                    // todo dynamically create object path
                    var gattCharacteristic = new GattCharacteristic("/org/bluez/example/service0/characteristic0",
                        characteristic.CharacteristicsProperties);
                    await _ServerContext.Connection.RegisterObjectAsync(gattCharacteristic);

                    gattService.AddCharacteristic(gattCharacteristic);

                    characteristic.CharacteristicsProperties.Service = gattService.ObjectPath;
                    characteristicObjectPaths.Add(gattCharacteristic.ObjectPath);
                    foreach (var descriptor in characteristic.Descriptors)
                    {
                        descriptor.Characteristic = gattCharacteristic.ObjectPath;
                        // todo dynamically create object path
                        var gattDescriptor = new GattDescriptor(
                            "/org/bluez/example/service0/characteristic0/descriptor0",
                            descriptor);
                        await _ServerContext.Connection.RegisterObjectAsync(gattDescriptor);

                        gattCharacteristic.AddDescriptor(gattDescriptor);
                    }

                }

                serviceDescription.Service1Properties.Characteristics = characteristicObjectPaths.ToArray();
            */
            }

            var gattManager = _ServerContext.Connection.CreateProxy<IGattManager1>("org.bluez", "/org/bluez/hci0");
            await gattManager.RegisterApplicationAsync(new ObjectPath("/"), new Dictionary<string, object>());
        }
    }
}