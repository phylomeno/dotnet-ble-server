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
        private string _UUID = default (string);
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

        private bool _Primary = default (bool);
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

        private ObjectPath[] _Characteristics = default (ObjectPath[]);
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
        private string _UUID = default (string);
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

        private ObjectPath _Service = default (ObjectPath);
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

        private byte[] _Value = default (byte[]);
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

        private bool _Notifying = default (bool);
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

        private string[] _Flags = default (string[]);
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
        private string _UUID = default (string);
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

        private ObjectPath _Characteristic = default (ObjectPath);
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

        private byte[] _Value = default (byte[]);
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

        private string[] _Flags = default (string[]);
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
    interface ILEAdvertisement1 : IDBusObject
    {
        Task ReleaseAsync();
        Task<T> GetAsync<T>(string prop);
        Task<LEAdvertisement1Properties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class LEAdvertisement1Properties
    {
        private string _Type = default (string);
        public string Type
        {
            get
            {
                return _Type;
            }

            set
            {
                _Type = (value);
            }
        }

        private string[] _ServiceUUIDs = default (string[]);
        public string[] ServiceUUIDs
        {
            get
            {
                return _ServiceUUIDs;
            }

            set
            {
                _ServiceUUIDs = (value);
            }
        }

        private IDictionary<string, object> _ManufacturerData = default (IDictionary<string, object>);
        public IDictionary<string, object> ManufacturerData
        {
            get
            {
                return _ManufacturerData;
            }

            set
            {
                _ManufacturerData = (value);
            }
        }

        private string[] _SolicitUUIDs = default (string[]);
        public string[] SolicitUUIDs
        {
            get
            {
                return _SolicitUUIDs;
            }

            set
            {
                _SolicitUUIDs = (value);
            }
        }

        private IDictionary<string, object> _ServiceData = default (IDictionary<string, object>);
        public IDictionary<string, object> ServiceData
        {
            get
            {
                return _ServiceData;
            }

            set
            {
                _ServiceData = (value);
            }
        }

        private bool _IncludeTxPower = default (bool);
        public bool IncludeTxPower
        {
            get
            {
                return _IncludeTxPower;
            }

            set
            {
                _IncludeTxPower = (value);
            }
        }
    }

    static class LEAdvertisement1Extensions
    {
        public static Task<string> GetTypeAsync(this ILEAdvertisement1 o) => o.GetAsync<string>("Type");
        public static Task<string[]> GetServiceUUIDsAsync(this ILEAdvertisement1 o) => o.GetAsync<string[]>("ServiceUUIDs");
        public static Task<IDictionary<string, object>> GetManufacturerDataAsync(this ILEAdvertisement1 o) => o.GetAsync<IDictionary<string, object>>("ManufacturerData");
        public static Task<string[]> GetSolicitUUIDsAsync(this ILEAdvertisement1 o) => o.GetAsync<string[]>("SolicitUUIDs");
        public static Task<IDictionary<string, object>> GetServiceDataAsync(this ILEAdvertisement1 o) => o.GetAsync<IDictionary<string, object>>("ServiceData");
        public static Task<bool> GetIncludeTxPowerAsync(this ILEAdvertisement1 o) => o.GetAsync<bool>("IncludeTxPower");
    }
}