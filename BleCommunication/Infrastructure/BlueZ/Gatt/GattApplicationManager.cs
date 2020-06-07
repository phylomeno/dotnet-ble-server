using System.Collections.Generic;
using System.Threading.Tasks;
using BleServer.Infrastructure.BlueZ.Core;

namespace BleServer.Infrastructure.BlueZ.Gatt
{
    public class GattApplicationManager
    {
        ServerContext _ServerContext;

        public GattApplicationManager(ServerContext serverContext)
        {
            _ServerContext = serverContext;
        }

        public async Task RegisterGattApplication(IEnumerable<ServiceDescription> gattServiceDescriptions)
        {

        }


    }
}