using System.Collections.Generic;
using System.Linq;

namespace DotnetBleServer.Gatt.Description
{
    public class GattApplicationBuilder
    {
        private readonly IList<GattServiceBuilder> _ServiceBuilders = new List<GattServiceBuilder>();

        public GattServiceBuilder AddService(GattServiceDescription gattServiceDescription)
        {
            var gattServiceBuilder = new GattServiceBuilder(gattServiceDescription);
            _ServiceBuilders.Add(gattServiceBuilder);
            return gattServiceBuilder;
        }

        public IEnumerable<GattServiceDescription> BuildServiceDescriptions()
        {
            return _ServiceBuilders.Select(s => s.ServiceDescription);
        }
    }
}