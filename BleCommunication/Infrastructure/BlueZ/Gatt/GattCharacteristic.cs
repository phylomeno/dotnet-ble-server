using System.Collections.Generic;
using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ.Core;

namespace BleServer.Infrastructure.BlueZ.Gatt
{
    internal class GattCharacteristic : PropertiesBase<GattCharacteristic1Properties>, IGattCharacteristic1
    {
        public GattCharacteristic(GattCharacteristic1Properties properties) : base(properties)
        {
        }

        public Task<byte[]> ReadValueAsync(IDictionary<string, object> Options)
        {
            throw new System.NotImplementedException();
        }

        public Task WriteValueAsync(byte[] Value, IDictionary<string, object> Options)
        {
            throw new System.NotImplementedException();
        }

        public Task StartNotifyAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task StopNotifyAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}