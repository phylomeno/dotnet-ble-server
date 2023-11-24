# .NET BLE Server
![Main Pipeline](https://github.com/phylomeno/dotnet-ble-server/workflows/Main%20Pipeline/badge.svg) [![MIT License](https://img.shields.io/badge/license-MIT-blue)](LICENSE) [![NuGet](https://img.shields.io/nuget/vpre/BluetoothLE.Server.Linux)](https://www.nuget.org/packages/BluetoothLE.Server.Linux/)

.NET BLE Server is a library to support creation of BLE peripherals on Linux using .NET Core.

Under the hood the library uses BlueZ. The provided API aims to preserve the object structure of BlueZ whilst minimizing the aspects of the D-Bus communication.

## Examples

<table>
  <tr>
    <td colspan="2" align="center">Client / Server demo (Windows - Linux)</td>
  </tr>
  <tr>
    <td colspan="2"><img src='https://github.com/CarteKiwi/BluetoothLE.Server.Linux/assets/12309245/e733aeff-2fb9-4808-89e0-f404739231fb'/></td>
  </tr>
    <tr>
        <td>Client running WinUI app using <a href="https://github.com/dotnet-bluetooth-le/dotnet-bluetooth-le">PLUGIN.BLE</a> nuget package</td>
        <td>Server running Linux (raspberry PI 3) (connected via SSH on WSL)</td>
  </tr>
</table>


### Advertisement
```csharp
using (var serverContext = new ServerContext())
{
    var advertisementProperties = new AdvertisementProperties
    {
        Type = "peripheral",
        ServiceUUIDs = new[] { "12345678-1234-5678-1234-56789abcdef0"},
        LocalName = "A",
        Appearance = (ushort)Convert.ToUInt32("0x0080", 16),
        Discoverable = true,
        IncludeTxPower = true,
    };

    await new AdvertisingManager(serverContext).CreateAdvertisement(advertisementProperties);
}
```

### Simple GATT Application
```csharp
using (var serverContext = new ServerContext())
{
    var gattServiceDescription = new GattServiceDescription
    {![demo](https://github.com/CarteKiwi/BluetoothLE.Server.Linux/assets/12309245/f8dd1bb2-f6af-4bbe-bcbe-50257fd29622)

        UUID = "12345678-1234-5678-1234-56789abcdef0",
        Primary = true
    };

    var gattCharacteristicDescription = new GattCharacteristicDescription
    {
        UUID = "12345678-1234-5678-1234-56789abcdef1",
        Flags = CharacteristicFla

https://github.com/CarteKiwi/BluetoothLE.Server.Linux/assets/12309245/bb4ebdf7-d298-494a-b33c-72248d4db187

gs.Read | CharacteristicFlags.Write | CharacteristicFlags.WritableAuxiliaries | CharacteristicFlags.Notify
    };
    var gattDescriptorDescription = new GattDescriptorDescription
    {
        Value = new[] {(byte) 't'},
        UUID = "12345678-1234-5678-1234-56789abcdef2",
        Flags = new[] {"read", "write"}
    };
    var gab = new GattApplicationBuilder();
    gab
        .AddService(gattServiceDescription)
        .WithCharacteristic(gattCharacteristicDescription, new[] {gattDescriptorDescription});

    await new GattApplicationManager(serverContext).RegisterGattApplication(gab.BuildServiceDescriptions());
}
```

## Useful references 
| Resource | Link |
| --- | --- |
| BlueZ GATT API documentation | https://git.kernel.org/pub/scm/bluetooth/bluez.git/tree/doc/gatt-api.txt |
| Presentation BLE on Linux | https://elinux.org/images/3/32/Doing_Bluetooth_Low_Energy_on_Linux.pdf |
