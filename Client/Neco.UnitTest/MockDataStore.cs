using System;
using System.Collections.Generic;
using System.Text;

namespace Neco.UnitTest
{
    class MockDataStore : Client.IDataStore
    {
        Dictionary<String, String> data = new Dictionary<string, string>();

        public string GetString(object context, string key)
        {
            return data.ContainsKey(key) ? data[key] : null;
        }

        public void SetString(object context, string key, string value)
        {
            data[key] = value;
        }
    }
}
