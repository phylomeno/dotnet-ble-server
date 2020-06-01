using System.Collections.Generic;

namespace BleServer.Infrastructure.BlueZ.Core
{
    internal interface IObjectManagerProperties
    {
        IDictionary<string, IDictionary<string, object>> GetProperties();
    }
}