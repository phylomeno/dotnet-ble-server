using System.Threading.Tasks;

namespace BleServer.Infrastructure.BlueZ.Utilities
{
    public static class PropertyAccessExtensions
    {
        public static Task<T> ReadProperty<T>(this object o, string prop)
        {
            var propertyValue = o.GetType().GetProperty(prop)?.GetValue(o);
            return Task.FromResult((T) propertyValue);
        }

        public static Task SetProperty(this object o, string prop, object val)
        {
            o.GetType().GetProperty(prop)?.SetValue(o, val);
            return Task.CompletedTask;
        }
    }
}