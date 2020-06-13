using System.Threading.Tasks;

namespace DotnetBleServer.Gatt
{
    public interface ICharacteristicSource
    {
        Task WriteValueAsync(byte[] value);
        Task<byte[]> ReadValueAsync();
    }
}