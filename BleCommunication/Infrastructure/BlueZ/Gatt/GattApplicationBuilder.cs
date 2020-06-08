using System.Collections.Generic;
using System.Linq;

namespace BleServer.Infrastructure.BlueZ.Gatt
{
    public class GattApplicationBuilder
    {
        private readonly IList<GattServiceBuilder> _ServiceBuilders = new List<GattServiceBuilder>();

        public GattServiceBuilder AddService(GattService1Properties gattService1Properties)
        {
            var gattServiceBuilder = new GattServiceBuilder(gattService1Properties);
            _ServiceBuilders.Add(gattServiceBuilder);
            return gattServiceBuilder;
        }

        public IEnumerable<ServiceDescription> BuildServiceDescriptions()
        {
            return _ServiceBuilders.Select(s => s.BuildServiceDescription());
        }
    }
}