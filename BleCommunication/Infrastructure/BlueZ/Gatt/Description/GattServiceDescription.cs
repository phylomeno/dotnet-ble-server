using System.Collections.Generic;

namespace BleServer.Infrastructure.BlueZ.Gatt.Description
{
    public class GattServiceDescription
    {
        public IList<GattCharacteristicDescription> GattCharacteristicDescriptions { get; } =
            new List<GattCharacteristicDescription>();

        public string UUID { get; set; }
        public bool Primary { get; set; }

        public void AddCharacteristic(GattCharacteristicDescription characteristic)
        {
            GattCharacteristicDescriptions.Add(characteristic);
        }
    }
}