using System.Threading.Tasks;

namespace BleServer.Gatt
{
    public interface ICharacteristicSource
    {
        Task WriteValueAsync(byte[] value);
        Task<byte[]> ReadValueAsync();
    }
}