using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tmds.DBus;

[assembly: InternalsVisibleTo(Tmds.DBus.Connection.DynamicAssemblyName)]
namespace DotnetBleServer.Gatt.BlueZModel
{
    [DBusInterface("org.bluez.GattService1")]
    internal interface IGattService1 : IDBusObject
    {
        Task<object> GetAsync(string prop);
        Task<GattService1Properties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    internal class GattService1Properties
    {
        private string _UUID = default(string);
        public string UUID
        {
            get
            {
                return _UUID;
            }

            set
            {
                _UUID = (value);
            }
        }

        private bool _Primary = default(bool);
        public bool Primary
        {
            get
            {
                return _Primary;
            }

            set
            {
                _Primary = (value);
            }
        }

        private ObjectPath[] _Characteristics = default(ObjectPath[]);
        public ObjectPath[] Characteristics
        {
            get
            {
                return _Characteristics;
            }

            set
            {
                _Characteristics = (value);
            }
        }
    }

    [DBusInterface("org.bluez.GattCharacteristic1")]
    interface IGattCharacteristic1 : IDBusObject
    {
        Task<byte[]> ReadValueAsync(IDictionary<string, object> Options);
        Task WriteValueAsync(byte[] Value, IDictionary<string, object> Options);
        Task StartNotifyAsync();
        Task StopNotifyAsync();
        Task<Object> GetAsync(string prop);
        Task<GattCharacteristic1Properties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    public class GattCharacteristic1Properties
    {
        private string _UUID = default(string);
        public string UUID
        {
            get
            {
                return _UUID;
            }

            set
            {
                _UUID = (value);
            }
        }

        private ObjectPath _Service = default(ObjectPath);
        public ObjectPath Service
        {
            get
            {
                return _Service;
            }

            set
            {
                _Service = (value);
            }
        }

        private byte[] _Value = default(byte[]);
        public byte[] Value
        {
            get
            {
                return _Value;
            }

            set
            {
                _Value = (value);
            }
        }

        private bool _Notifying = default(bool);
        public bool Notifying
        {
            get
            {
                return _Notifying;
            }

            set
            {
                _Notifying = (value);
            }
        }

        private string[] _Flags = default(string[]);
        public string[] Flags
        {
            get
            {
                return _Flags;
            }

            set
            {
                _Flags = (value);
            }
        }
    }

    [DBusInterface("org.bluez.GattDescriptor1")]
    interface IGattDescriptor1 : IDBusObject
    {
        Task<byte[]> ReadValueAsync();
        Task<object> GetAsync(string prop);
        Task<GattDescriptor1Properties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    public class GattDescriptor1Properties
    {
        private string _UUID = default(string);
        public string UUID
        {
            get
            {
                return _UUID;
            }

            set
            {
                _UUID = (value);
            }
        }

        private ObjectPath _Characteristic = default(ObjectPath);
        public ObjectPath Characteristic
        {
            get
            {
                return _Characteristic;
            }

            set
            {
                _Characteristic = (value);
            }
        }

        private byte[] _Value = default(byte[]);
        public byte[] Value
        {
            get
            {
                return _Value;
            }

            set
            {
                _Value = (value);
            }
        }

        private string[] _Flags = default(string[]);
        public string[] Flags
        {
            get
            {
                return _Flags;
            }

            set
            {
                _Flags = (value);
            }
        }
    }

    [DBusInterface("org.bluez.LEAdvertisement1")]
    public interface ILEAdvertisement1 : IDBusObject
    {
        Task ReleaseAsync();
        Task<object> GetAsync(string prop);
        Task<LEAdvertisement1Properties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    public class LEAdvertisement1Properties
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

        private UInt16 _Appearance;
        public UInt16 Appearance
        {
            get => _Appearance;
            set => _Appearance = value;
        }

        private bool _Discoverable;
        public bool Discoverable
        {
            get => _Discoverable;
            set => _Discoverable = value;
        }
    }

    static class LEAdvertisement1Extensions
    {
        //public static Task<object> GetTypeAsync(this ILEAdvertisement1 o) => o.GetAsync("Type");
        //public static Task<string[]> GetServiceUUIDsAsync(this ILEAdvertisement1 o) => o.GetAsync<string[]>("ServiceUUIDs");
        //public static Task<IDictionary<string, object>> GetManufacturerDataAsync(this ILEAdvertisement1 o) => o.GetAsync<IDictionary<string, object>>("ManufacturerData");
        //public static Task<string[]> GetSolicitUUIDsAsync(this ILEAdvertisement1 o) => o.GetAsync<string[]>("SolicitUUIDs");
        //public static Task<IDictionary<string, object>> GetServiceDataAsync(this ILEAdvertisement1 o) => o.GetAsync<IDictionary<string, object>>("ServiceData");
        //public static Task<bool> GetIncludeTxPowerAsync(this ILEAdvertisement1 o) => o.GetAsync<bool>("IncludeTxPower");
        public static Task SetLocalNameAsync(this ILEAdvertisement1 o, string val) => o.SetAsync("LocalName", val);
    }
}