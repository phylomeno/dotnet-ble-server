using System.Collections.Generic;
using BleServer.Infrastructure.BlueZ.Gatt;

namespace BleServer.Infrastructure.BlueZ.Gatt
{
    internal class ServiceDescription
    {
        public GattService1Properties Service1Properties { get; }
        public IList<CharacteristicDescription> Characteristic { get; }

        public ServiceDescription(GattService1Properties service1Properties,
            IList<CharacteristicDescription> characteristic)
        {
            Characteristic = characteristic;
            Service1Properties = service1Properties;
        }
    }
}