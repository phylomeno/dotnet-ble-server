using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ.Core;

namespace BleServer.Infrastructure.BlueZ.Gatt
{
    internal class GattDescriptor : PropertiesBase<GattDescriptor1Properties>, IGattDescriptor1
    {
        public GattDescriptor(GattDescriptor1Properties gattDescriptor1Properties)
            : base(gattDescriptor1Properties)
        {
        }

        public Task<byte[]> ReadValueAsync()
        {
            return Task.FromResult(Properties.Value);
        }
    }
}