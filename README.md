## Useful docs
BlueZ GATT API documentation: https://git.kernel.org/pub/scm/bluetooth/bluez.git/tree/doc/gatt-api.txt
Presentation BLE on Linux: https://elinux.org/images/3/32/Doing_Bluetooth_Low_Energy_on_Linux.pdf

# Install Tmds.DBus tool

``` dotnet install tool -g Tmds.DBus.Tool ```

# Generate DBus Code

## Generate GATT Objects from XML specs

``` dotnet dbus codegen Spec/org.bluez.gatt.xml --namespace BleServer.Infrastructure.BlueZ --output BlueZGatt.cs ```

## Generate GATT Objects from dbus services

``` dotnet dbus codegen --bus system --service org.bluez --namespace BleServer.Infrastructure.BlueZ --output BlueZ.cs ```
