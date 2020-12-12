using System;
using System.Collections.Generic;
using System.Text;

namespace CASTOR2.Core.Base
{
    internal static class Extensions
    {
        internal static TValue GetDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            dict.TryGetValue(key, out TValue value);
            return value;
        }
    }
}
