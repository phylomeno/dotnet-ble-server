using System;

namespace DotnetBleServer.Gatt.Description
{
    [Flags]
    public enum CharacteristicFlags
    {
        Read = 1,
        Write = 2,
        Notify = 3,
        WritableAuxiliaries = 4
    }
}