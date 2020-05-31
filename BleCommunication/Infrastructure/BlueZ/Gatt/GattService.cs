using BleServer.Infrastructure.BlueZ.Core;
using Tmds.DBus;

namespace BleServer.Infrastructure.BlueZ.Gatt
{
    internal class GattService : PropertiesBase<GattService1Properties>, IGattService1
    {
        public GattService(ObjectPath objectPath, GattService1Properties properties) : base(objectPath, properties)
        {
        }
    }
}