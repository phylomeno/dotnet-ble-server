using Tmds.DBus;

namespace BleServer.Infrastructure.BlueZ.Core
{
    public class ServerContext
    {
        public ServerContext()
        {
            Connection = new Connection(Address.System);
            Connection.ConnectAsync();
        }

        public Connection Connection { get; }
    }
}