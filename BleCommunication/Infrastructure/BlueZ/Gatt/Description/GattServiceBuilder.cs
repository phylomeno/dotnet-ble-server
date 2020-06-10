namespace BleServer.Infrastructure.BlueZ.Gatt.Description
{
    public class GattServiceBuilder
    {
        public GattServiceDescription ServiceDescription { get; }

        public GattServiceBuilder(GattServiceDescription gattServiceServiceDescription)
        {
            ServiceDescription = gattServiceServiceDescription;
        }

        public void WithCharacteristic(GattCharacteristicDescription gattCharacteristicDescription,
            GattDescriptorDescription[] gattDescriptorDescriptions)
        {
            var characteristicDescription = new GattCharacteristicDescription();
            foreach (var description in gattDescriptorDescriptions)
            {
                characteristicDescription.AddDescriptor(description);
            }

            ServiceDescription.AddCharacteristic(characteristicDescription);
        }
    }
}