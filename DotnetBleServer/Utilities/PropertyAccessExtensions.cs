using System.Threading.Tasks;

namespace DotnetBleServer.Utilities
{
    public static class PropertyAccessExtensions
    {
        public static T ReadProperty<T>(this object o, string prop)
        {
            var propertyValue = o.GetType().GetProperty(prop)?.GetValue(o);
            return (T) propertyValue;
        }

        public static object ReadProperty(this object o, string prop)
        {
            return o.GetType().GetProperty(prop)?.GetValue(o);
        }


        public static Task SetProperty(this object o, string prop, object val)
        {
            o.GetType().GetProperty(prop)?.SetValue(o, val);
            return Task.CompletedTask;
        }
    }
}