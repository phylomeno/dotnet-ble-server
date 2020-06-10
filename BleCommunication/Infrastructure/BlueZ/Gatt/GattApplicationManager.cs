using System.Collections.Generic;
using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ.Core;
using BleServer.Infrastructure.BlueZ.Gatt.BlueZModel;
using BleServer.Infrastructure.BlueZ.Gatt.Description;
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
            await BuildApplicationTree(gattServiceDescriptions);
            await RegisterApplicationInBluez();
        }

        private async Task BuildApplicationTree(IEnumerable<GattServiceDescription> gattServiceDescriptions)
        {
            var application = await BuildGattApplication();

            foreach (var serviceDescription in gattServiceDescriptions)
            {
                var service = await AddNewService(application, serviceDescription);

                foreach (var characteristicDescription in serviceDescription.GattCharacteristicDescriptions)
                {
                    var characteristic = await AddNewCharacteristic(service, characteristicDescription);

                    foreach (var descriptorDescription in characteristicDescription.Descriptors)
                    {
                        await AddNewDescriptor(characteristic, descriptorDescription);
                    }
                }
            }
        }

        private async Task RegisterApplicationInBluez()
        {
            var gattManager = _ServerContext.Connection.CreateProxy<IGattManager1>("org.bluez", "/org/bluez/hci0");
            await gattManager.RegisterApplicationAsync(new ObjectPath("/"), new Dictionary<string, object>());
        }

        private async Task<GattApplication> BuildGattApplication()
        {
            var application = new GattApplication("/");
            await _ServerContext.Connection.RegisterObjectAsync(application);
            return application;
        }

        private async Task<GattService> AddNewService(GattApplication application,
            GattServiceDescription serviceDescription)
        {
            var gattService1Properties = GattPropertiesFactory.CreateGattService(serviceDescription);
            var gattService = application.AddService(gattService1Properties);
            await _ServerContext.Connection.RegisterObjectAsync(gattService);
            return gattService;
        }

        private async Task<GattCharacteristic> AddNewCharacteristic(GattService gattService, GattCharacteristicDescription characteristic)
        {
            var gattCharacteristic1Properties = GattPropertiesFactory.CreateGattCharacteristic(characteristic);
            var gattCharacteristic = gattService.AddCharacteristic(gattCharacteristic1Properties);
            await _ServerContext.Connection.RegisterObjectAsync(gattCharacteristic);
            return gattCharacteristic;
        }

        private async Task AddNewDescriptor(GattCharacteristic gattCharacteristic,
            GattDescriptorDescription descriptor)
        {
            var gattDescriptor1Properties = GattPropertiesFactory.CreateGattDescriptor(descriptor);
            var gattDescriptor = gattCharacteristic.AddDescriptor(gattDescriptor1Properties);
            await _ServerContext.Connection.RegisterObjectAsync(gattDescriptor);
        }
    }
}