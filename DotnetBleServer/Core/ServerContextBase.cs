using System;
using System.Threading.Tasks;
using Tmds.DBus;

namespace DotnetBleServer.Core
{
    public class ServerContextBase : IDisposable
    {
        public ServerContextBase()
        {
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