using System.Collections.Generic;
using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ.Core;
using Tmds.DBus;

namespace BleServer.Infrastructure.BlueZ.Gatt
{
    internal class GattCharacteristic : PropertiesBase<GattCharacteristic1Properties>, IGattCharacteristic1
    {
        public GattCharacteristic(ObjectPath objectPath, GattCharacteristic1Properties properties) : base(objectPath, properties)
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