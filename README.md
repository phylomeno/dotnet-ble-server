# .NET BLE Server
![Main Pipeline](https://github.com/phylomeno/dotnet-ble-server/workflows/Main%20Pipeline/badge.svg) [![MIT License](https://img.shields.io/badge/license-MIT-blue)](LICENSE) [![NuGet](https://img.shields.io/nuget/vpre/DotnetBleServer)](https://www.nuget.org/packages/DotnetBleServer/)

.NET BLE Server is a library to support creation of BLE peripherals on Linux using .NET Core.

Under the hood the library uses BlueZ. The provided API aims to preserve the object structure of BlueZ whilst minimizing the aspects of the D-Bus communication.

## Examples

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
    {
        UUID = "12345678-1234-5678-1234-56789abcdef0",
        Primary = true
    };

    var gattCharacteristicDescription = new GattCharacteristicDescription
    {
        UUID = "12345678-1234-5678-1234-56789abcdef1",
        Flags = CharacteristicFlags.Read | CharacteristicFlags.Write | CharacteristicFlags.WritableAuxiliaries | CharacteristicFlags.Notify
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
