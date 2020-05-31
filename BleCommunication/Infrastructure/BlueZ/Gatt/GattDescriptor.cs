using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ.Core;
using Tmds.DBus;

namespace BleServer.Infrastructure.BlueZ.Gatt
{
    internal class GattDescriptor : PropertiesBase<GattDescriptor1Properties>, IGattDescriptor1
    {
        public GattDescriptor(ObjectPath objectPath, GattDescriptor1Properties gattDescriptor1Properties)
            : base(objectPath, gattDescriptor1Properties)
        {

        }

        public Task<byte[]> ReadValueAsync()
        {
            return Task.FromResult(Properties.Value);
        }
    }
}