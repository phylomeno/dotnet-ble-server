using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Extensions;
using Sample.Common.Models;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Client.Win
{
    internal class BluetoothService
    {
        public Action<string> StatusChanged { get; set; }
        public Action<RomeRemoteSystem> Discovered { get; set; }

        private static readonly Guid SERVICE_CHARACTERISTIC_UUID = new Guid("12345678-1234-5678-1234-56789abcdef1");
        private const int TIMEOUT = 5000;
        private IDevice _connectedDevice;
        private Guid _serviceUuid;
        private IAdapter _adapter;
        private CancellationTokenSource _cts = new CancellationTokenSource();

        private async void Scan(Guid serviceUuid)
        {
            if (_adapter.IsScanning)
                await StopAsync();

            _serviceUuid = serviceUuid;
            _adapter.ScanTimeout = TIMEOUT;
            _adapter.DeviceDiscovered += DeviceDiscovered;
            _adapter.ScanTimeoutElapsed += ScanTimeoutElapsed;

            var scanFilterOptions = new ScanFilterOptions();
            scanFilterOptions.ServiceUuids = [serviceUuid]; // cross platform filter

            Debug.WriteLine("Scanning started");
            StatusChanged.Invoke("Scanning...");
            await _adapter.StartScanningForDevicesAsync(scanFilterOptions, _cts.Token);
        }

        private void ScanTimeoutElapsed(object sender, EventArgs e)
        {
            StatusChanged.Invoke("Stopped. Try again.");
        }

        private void DeviceDiscovered(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceEventArgs scanResult)
        {
            var device = $"{scanResult.Device.Name} - {scanResult.Device.Id}";
            Debug.WriteLine($"Discovered {device}");

            Discovered?.Invoke(new RomeRemoteSystem(scanResult.Device)
            {
                DisplayName = scanResult.Device.Name,
                Status = scanResult.Device.State.ToString(),
                Id = scanResult.Device.Id.ToString(),
            });
        }

        private async Task ResetAsync()
        {
            if (_adapter == null) return;

            _cts?.Cancel();

            if (_adapter.ConnectedDevices.Count > 0)
            {
                await _adapter.DisconnectDeviceAsync(_adapter.ConnectedDevices[0]);
            }

            _adapter.DeviceDiscovered -= DeviceDiscovered;
            _adapter.ScanTimeoutElapsed -= ScanTimeoutElapsed;

            _cts.Dispose();
            _cts = new CancellationTokenSource();
        }

        private async Task StopAsync()
        {
            if (_adapter.IsScanning)
                await _adapter.StopScanningForDevicesAsync();
        }

        public async Task<bool> ConnectAsync(IDevice device)
        {
            try
            {
                await StopAsync();

                Debug.WriteLine($"Connecting...");

                _connectedDevice = device;

                await _adapter.ConnectToDeviceAsync(_connectedDevice, cancellationToken: _cts.Token);

                Debug.WriteLine($"Connected: {_connectedDevice != null}");

                return _connectedDevice != null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to connect : " + ex.Message);
                return false;
            }
        }

        public async Task InitializeAsync(Guid serviceUuid, string? hostName)
        {
            StatusChanged.Invoke("Initializing...");

            await ResetAsync();

            _adapter = CrossBluetoothLE.Current.Adapter;

            if (CrossBluetoothLE.Current.State == BluetoothState.On)
                Scan(serviceUuid);
            else
            {
                StatusChanged.Invoke("waiting for initialization...");
                CrossBluetoothLE.Current.StateChanged += (s, e) =>
                {
                    if (CrossBluetoothLE.Current.State == BluetoothState.On)
                        Scan(serviceUuid);
                };
            }
        }

        public async Task<string> GetAsync(string value)
        {
            try
            {
                Debug.WriteLine("Get service");
                var service = await _connectedDevice.GetServiceAsync(_serviceUuid, _cts.Token);

                Debug.WriteLine("Get write characteristic");
                var characteristic = await service?.GetCharacteristicAsync(SERVICE_CHARACTERISTIC_UUID);

                Debug.WriteLine("Write value");
                await characteristic?.WriteAsync(Encoding.ASCII.GetBytes(value), _cts.Token);

                Debug.WriteLine("Read value");
                var result = await characteristic?.ReadAsync(_cts.Token);

                // You can also use ValueUpdated
                //characteristic.ValueUpdated += (a, b) =>
                //{
                //    Console.WriteLine("Value updated: " + Encoding.ASCII.GetString(b.Characteristic.Value));
                //};
                //await characteristic.StartUpdatesAsync();

                return Encoding.ASCII.GetString(result.data);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetAsync failed: {ex.Message}");
                return string.Empty;
            }
        }
    }
}