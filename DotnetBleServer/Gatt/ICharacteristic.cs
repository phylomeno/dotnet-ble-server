using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetBleServer.Gatt
{
    public interface ICharacteristic
    {
        /// <summary>
        /// Indicates wheter the characteristic supports notify or not.
        /// </summary>
        bool CanUpdate { get; }

        /// <summary>
        /// Event gets raised, when the davice notifies a value change on this characteristic.
        /// To start listening, call <see cref="StartUpdatesAsync"/>.
        /// </summary>
        event EventHandler<CharacteristicUpdatedEventArgs> ValueUpdated;

        Task WriteValueAsync(byte[] value);

        Task<byte[]> ReadValueAsync();

        /// <summary>
        /// Starts listening for notify events on this characteristic.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="InvalidOperationException">Thrown if characteristic doesn't support notify. See: <see cref="CanUpdate"/></exception>
        /// <exception cref="Exception">Thrown if an error occurs while starting notifications </exception>
        Task StartUpdatesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Stops listening for notify events on this characteristic.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="Exception">Thrown if an error occurs while starting notifications </exception>
        Task StopUpdatesAsync(CancellationToken cancellationToken = default);
    }
}