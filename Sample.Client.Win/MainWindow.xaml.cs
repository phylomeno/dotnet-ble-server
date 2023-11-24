using Microsoft.UI.Xaml;
using Plugin.BLE.Abstractions.Contracts;
using Sample.Common.Models;
using System;
using System.Text.Json;

namespace Sample.Client.Win
{
    public sealed partial class MainWindow : Window
    {
        private BluetoothService _service;

        public MainWindow()
        {
            this.InitializeComponent();
            _service = new BluetoothService();
        }

        private async void myButton_Click(object sender, RoutedEventArgs e)
        {
            devices.Items.Clear();
            logs.Items.Clear();

            _service.Discovered = (device) =>
            {
                DispatcherQueue.TryEnqueue(() =>
                {
                    devices.Items.Add(device);
                });
            };

            _service.StatusChanged = (state) =>
            {
                myButton.Content = state;
            };

            await _service.InitializeAsync(new Guid("12345678-1234-5678-1234-56789abcdef0"), null);
        }

        private async void devices_SelectionChanged(object sender, Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;

            var device = (RomeRemoteSystem)e.AddedItems[0];
            myButton.Content = "Start";

            logs.Items.Add($"Connecting to {device.DisplayName}...");
            var connected = await _service.ConnectAsync((IDevice)device.NativeObject);
            logs.Items.Add(connected ? "Connected" : "Unable to connect");

            var data = JsonSerializer.Serialize(new RemoteParameter() { Command = RemoteCommands.Command1 });
            logs.Items.Add($"Sending data... {data}");
            var response = await _service.GetAsync(data);
            logs.Items.Add($"Data received: {response}");
        }
    }
}