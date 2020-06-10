using System.Collections.Generic;

namespace BleServer.Core
{
    internal interface IObjectManagerProperties
    {
        IDictionary<string, IDictionary<string, object>> GetProperties();
    }
}