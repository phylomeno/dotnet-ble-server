namespace DotnetBleServer.Gatt.Description
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
            foreach (var description in gattDescriptorDescriptions)
            {
                gattCharacteristicDescription.AddDescriptor(description);
            }

            ServiceDescription.AddCharacteristic(gattCharacteristicDescription);
        }
    }
}