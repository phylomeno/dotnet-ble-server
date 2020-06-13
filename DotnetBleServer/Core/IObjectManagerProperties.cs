using System.Collections.Generic;

namespace DotnetBleServer.Core
{
    internal interface IObjectManagerProperties
    {
        IDictionary<string, IDictionary<string, object>> GetProperties();
    }
}