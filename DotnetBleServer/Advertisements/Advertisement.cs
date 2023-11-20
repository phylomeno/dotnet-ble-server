using DotnetBleServer.Core;
using DotnetBleServer.Gatt.BlueZModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetBleServer.Advertisements
{
    public class Advertisement : PropertiesBase<LEAdvertisement1Properties>, ILEAdvertisement1, IObjectManagerProperties
    {
        public Advertisement(string objectPath, LEAdvertisement1Properties properties) : base(objectPath, properties)
        {
        }

        /// <summary>
        /// TODO: Keep or remove ?
        /// Not sure it is mandatory for LEAdvertisement1
        /// Should try setting properties without implementing IObjectManagerProperties
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, IDictionary<string, object>> GetProperties()
        {
            return new Dictionary<string, IDictionary<string, object>>
            {
                {
                    "org.bluez.LEAdvertisement1", new Dictionary<string, object>
                    {
                        {"LocalName", Properties.LocalName},
                        {"Type", Properties.Type},
                        {"ServiceUUIDs", Properties.ServiceUUIDs},
                        {"Appearance", Properties.Appearance}
                    }
                }
            };
        }

        public Task ReleaseAsync()
        {
            throw new NotImplementedException();
        }
    }
}