using System.Collections.Generic;
using BleServer.Infrastructure.Bluez.Gatt;

namespace BleServer.Infrastructure.BlueZ
{
    public class ServiceDescription
    {
        private IList<CharacteristicDescription> _Characteristic;

        public ServiceDescription(IList<CharacteristicDescription> characteristic)
        {
            _Characteristic = characteristic;
        }
    }
}