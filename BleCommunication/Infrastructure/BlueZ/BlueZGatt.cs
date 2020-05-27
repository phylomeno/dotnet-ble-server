using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tmds.DBus;

[assembly: InternalsVisibleTo(Tmds.DBus.Connection.DynamicAssemblyName)]
namespace BleServer.Infrastructure.BlueZ
{
    [DBusInterface("org.bluez.GattService1")]
    interface IGattService1 : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<GattService1Properties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class GattService1Properties
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

    static class GattService1Extensions
    {
        public static Task<string> GetUUIDAsync(this IGattService1 o) => o.GetAsync<string>("UUID");
        public static Task<bool> GetPrimaryAsync(this IGattService1 o) => o.GetAsync<bool>("Primary");
        public static Task<ObjectPath[]> GetCharacteristicsAsync(this IGattService1 o) => o.GetAsync<ObjectPath[]>("Characteristics");
    }

    [DBusInterface("org.bluez.GattCharacteristic1")]
    interface IGattCharacteristic1 : IDBusObject
    {
        Task<byte[]> ReadValueAsync(IDictionary<string, object> Options);
        Task WriteValueAsync(byte[] Value, IDictionary<string, object> Options);
        Task StartNotifyAsync();
        Task StopNotifyAsync();
        Task<T> GetAsync<T>(string prop);
        Task<GattCharacteristic1Properties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class GattCharacteristic1Properties
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

    static class GattCharacteristic1Extensions
    {
        public static Task<string> GetUUIDAsync(this IGattCharacteristic1 o) => o.GetAsync<string>("UUID");
        public static Task<ObjectPath> GetServiceAsync(this IGattCharacteristic1 o) => o.GetAsync<ObjectPath>("Service");
        public static Task<byte[]> GetValueAsync(this IGattCharacteristic1 o) => o.GetAsync<byte[]>("Value");
        public static Task<bool> GetNotifyingAsync(this IGattCharacteristic1 o) => o.GetAsync<bool>("Notifying");
        public static Task<string[]> GetFlagsAsync(this IGattCharacteristic1 o) => o.GetAsync<string[]>("Flags");
    }

    [DBusInterface("org.bluez.GattDescriptor1")]
    interface IGattDescriptor1 : IDBusObject
    {
        Task<byte[]> ReadValueAsync();
        Task<T> GetAsync<T>(string prop);
        Task<GattDescriptor1Properties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class GattDescriptor1Properties
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

    static class GattDescriptor1Extensions
    {
        public static Task<string> GetUUIDAsync(this IGattDescriptor1 o) => o.GetAsync<string>("UUID");
        public static Task<ObjectPath> GetCharacteristicAsync(this IGattDescriptor1 o) => o.GetAsync<ObjectPath>("Characteristic");
        public static Task<byte[]> GetValueAsync(this IGattDescriptor1 o) => o.GetAsync<byte[]>("Value");
        public static Task<string[]> GetFlagsAsync(this IGattDescriptor1 o) => o.GetAsync<string[]>("Flags");
    }
}