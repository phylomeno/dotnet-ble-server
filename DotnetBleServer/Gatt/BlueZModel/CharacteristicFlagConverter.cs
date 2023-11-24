using System.Collections.Generic;
using System.Linq;

using DotnetBleServer.Gatt.Description;

namespace DotnetBleServer.Gatt.BlueZModel
{
    internal class CharacteristicFlagConverter
    {
        private static readonly Dictionary<CharacteristicFlags, string> FlagMappings =
            new Dictionary<CharacteristicFlags, string>
            {
                {CharacteristicFlags.Read, "read"},
                {CharacteristicFlags.Write, "write"},
                {CharacteristicFlags.Notify, "notify"},
                {CharacteristicFlags.WritableAuxiliaries, "writable-auxiliaries"}
            };

        public static string[] ConvertFlags(CharacteristicFlags characteristicFlags)
        {
            return (from mapping in FlagMappings where (characteristicFlags & mapping.Key) > 0 select mapping.Value).ToArray();
        }
    }
}