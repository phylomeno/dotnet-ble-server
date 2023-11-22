using System;

namespace DotnetBleServer.Gatt
{
    /// <summary>
    /// Event arguments for <c>ICharacteristic.ValueUpdated</c>
    /// </summary>
    public class CharacteristicUpdatedEventArgs : EventArgs
    {
        /// <summary>
        /// The characteristic.
        /// </summary>
        public ICharacteristic Characteristic { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public CharacteristicUpdatedEventArgs(ICharacteristic characteristic)
        {
            Characteristic = characteristic;
        }
    }
}