using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Tmds.DBus;

namespace DotnetBleServer.Advertisements
{
    [Dictionary]
    [SuppressMessage("ReSharper", "ConvertToAutoProperty")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class AdvertisementProperties
    {
        private string _Type;
        public string Type
        {
            get => _Type;
            set => _Type = value;
        }

        private string[] _ServiceUUIDs;
        public string[] ServiceUUIDs
        {
            get => _ServiceUUIDs;
            set => _ServiceUUIDs = value;
        }

        private IDictionary<string, object> _ManufacturerData;
        public IDictionary<string, object> ManufacturerData
        {
            get => _ManufacturerData;
            set => _ManufacturerData = value;
        }

        private string[] _SolicitUUIDs;
        public string[] SolicitUUIDs
        {
            get => _SolicitUUIDs;
            set => _SolicitUUIDs = value;
        }

        private IDictionary<string, object> _ServiceData;
        public IDictionary<string, object> ServiceData
        {
            get => _ServiceData;
            set => _ServiceData = value;
        }

        private bool _IncludeTxPower;
        public bool IncludeTxPower
        {
            get => _IncludeTxPower;
            set => _IncludeTxPower = value;
        }

        private string _LocalName;
        public string LocalName
        {
            get => _LocalName;
            set => _LocalName = value;
        }

    }
}