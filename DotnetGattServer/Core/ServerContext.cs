using System;
using System.Threading.Tasks;
using Tmds.DBus;

namespace BleServer.Core
{
    public class ServerContext : IDisposable
    {
        public ServerContext()
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