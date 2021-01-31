using System;
using System.Collections.Generic;
using System.Text;

namespace CASTOR2.Core.Base
{
    public static class Helpers
    {
        public static bool Equals<T>(T t_this, object numeric) where T : Interfaces.INumeric
        {
            switch (Type.GetTypeCode(numeric.GetType()))
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return t_this.Equals((long)numeric);
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return t_this.Equals((ulong)numeric);
                case TypeCode.Decimal:
                    return t_this.Equals((decimal)numeric);
                case TypeCode.Single:
                case TypeCode.Double:
                    return t_this.Equals((double)numeric);
                default:
                    throw new InvalidCastException($"The type {typeof(T).Name} cannot be compared to the type {numeric.GetType().Name}");
            }
        }
        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }
    }
}
