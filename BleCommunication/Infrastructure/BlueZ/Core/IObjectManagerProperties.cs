using System.Collections.Generic;

namespace BleServer.Infrastructure.BlueZ.Core
{
    internal interface IObjectManagerProperties
    {
        Dictionary<string, Dictionary<string, object>> GetProperties();
    }
}