using System.Net;

namespace Sample.Common.Models
{
    public enum RemoteCommands
    {
        Command1,
        Command2
    }

    public class RemoteParameter
    {
        public string? SerializedData { get; set; }
        public RemoteCommands Command { get; set; }
    }

    public class RomeRemoteSystem(object nativeObject)
    {
        public object NativeObject { get; } = nativeObject;

        public string DisplayName { get; set; }
        public string Id { get; set; }
        public string Status { get; set; }
        public string Kind { get; set; }
        public IPEndPoint EndPoint { get; set; }

        public string DisplayMember => $"{DisplayName} - {Id}";
    }
}
