using BleServer.Infrastructure.BlueZ.Core;

namespace BleServer.Infrastructure.BlueZ.Gatt
{
    internal class GattService : PropertiesBase<GattService1Properties>, IGattService1
    {
        public GattService(GattService1Properties properties) : base(properties)
        {
        }
    }
}