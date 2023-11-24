using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetBleServer.Gatt.Description
{
    public abstract class GattCharacteristicDescription : ICharacteristic
    {
        private bool _notify;
        private readonly IList<GattDescriptorDescription> _Descriptors = new List<GattDescriptorDescription>();

        public IEnumerable<GattDescriptorDescription> Descriptors => _Descriptors;

        public string UUID { get; set; }

        public CharacteristicFlags Flags { get; set; }

        public void AddDescriptor(GattDescriptorDescription gattDescriptorDescription)
        {
            _Descriptors.Add(gattDescriptorDescription);
        }

        public bool CanUpdate => Flags.HasFlag(CharacteristicFlags.Notify);

        public event EventHandler<CharacteristicUpdatedEventArgs> ValueUpdated;

        public virtual Task WriteValueAsync(byte[] value)
        {
            if (_notify)
                ValueUpdated?.Invoke(this, new CharacteristicUpdatedEventArgs(this));

            return Task.CompletedTask;
        }

        public abstract Task<byte[]> ReadValueAsync();

        public Task StartUpdatesAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(() => _notify = true);
        }

        public Task StopUpdatesAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(() => _notify = false);
        }
    }
}