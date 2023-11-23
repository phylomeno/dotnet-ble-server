using DotnetBleServer.Core;
using DotnetBleServer.Gatt.BlueZModel;
using DotnetBleServer.Gatt.Description;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tmds.DBus;

namespace DotnetBleServer.Gatt
{
    public class GattApplicationManager
    {
        private readonly ServerContext _context;

        public GattApplicationManager(ServerContext context)
        {
            _context = context;
        }

        public async Task RegisterGattApplication(IEnumerable<GattServiceDescription> gattServiceDescriptions)
        {
            var applicationObjectPath = GenerateApplicationObjectPath();
            await BuildApplicationTree(applicationObjectPath, gattServiceDescriptions);
            await RegisterApplicationInBluez(applicationObjectPath);
        }

        private static string GenerateApplicationObjectPath()
        {
            var appId = Guid.NewGuid().ToString().Substring(0, 8);
            var applicationObjectPath = $"/{appId}";
            return applicationObjectPath;
        }

        private async Task BuildApplicationTree(string applicationObjectPath, IEnumerable<GattServiceDescription> gattServiceDescriptions)
        {
            var application = await BuildGattApplication(applicationObjectPath);

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

        private async Task RegisterApplicationInBluez(string applicationObjectPath)
        {
            var gattManager = _context.Connection.CreateProxy<IGattManager1>(Constants.DbusServicePath, _context.Adapter.ObjectPath);
            await gattManager.RegisterApplicationAsync(new ObjectPath(applicationObjectPath), new Dictionary<string, object>());
        }

        private async Task<GattApplication> BuildGattApplication(string applicationObjectPath)
        {
            var application = new GattApplication(applicationObjectPath);
            await _context.Connection.RegisterObjectAsync(application);
            return application;
        }

        private async Task<GattService> AddNewService(GattApplication application, GattServiceDescription serviceDescription)
        {
            var gattService1Properties = GattPropertiesFactory.CreateGattService(serviceDescription);
            var gattService = application.AddService(gattService1Properties);
            await _context.Connection.RegisterObjectAsync(gattService);
            return gattService;
        }

        private async Task<GattCharacteristic> AddNewCharacteristic(GattService gattService, GattCharacteristicDescription characteristic)
        {
            var gattCharacteristic1Properties = GattPropertiesFactory.CreateGattCharacteristic(characteristic);
            var gattCharacteristic = gattService.AddCharacteristic(gattCharacteristic1Properties, characteristic);
            await _context.Connection.RegisterObjectAsync(gattCharacteristic);
            return gattCharacteristic;
        }

        private async Task AddNewDescriptor(GattCharacteristic gattCharacteristic, GattDescriptorDescription descriptor)
        {
            var gattDescriptor1Properties = GattPropertiesFactory.CreateGattDescriptor(descriptor);
            var gattDescriptor = gattCharacteristic.AddDescriptor(gattDescriptor1Properties);
            await _context.Connection.RegisterObjectAsync(gattDescriptor);
        }
    }
}