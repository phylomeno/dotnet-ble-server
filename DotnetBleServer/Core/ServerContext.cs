using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using Tmds.DBus;

namespace DotnetBleServer.Core
{
    public class ServerContext : IDisposable
    {
        public ServerContext(ILogger logger = null)
        {
            logger = logger ?? NullLogger.Instance;

            logger.LogWarning("system address = {SystemAddress}", Address.System);
            Connection = new Connection(Address.System);
        }

        public async Task Connect()
        {
            await Connection.ConnectAsync();
        }

        public Connection Connection { get; }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}