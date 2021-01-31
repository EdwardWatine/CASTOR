using CASTOR2.Core.Base.Interfaces;
using CASTOR2.Core.Base.Operations.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CASTOR2.Core.Base.NumberTypes.Real
{
    public partial class Rational : RealBase, IField<Rational>, IReal, IConvertible, IEquatable<Rational>, IComparable<Rational>
    {
        public TypeCode GetTypeCode()
        {
            return TypeCode.Double;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new InvalidCastException("A rational number cannot be cast to a Boolean object");
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new InvalidCastException("A rational number cannot be cast to a Byte-like object");
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException("A rational number cannot be cast to a Char");
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException("A rational number cannot be cast to a DateTime");
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return (decimal)AsDouble();
        }

        public double ToDouble(IFormatProvider provider)
        {
            return AsDouble();
        }

        public short ToInt16(IFormatProvider provider)
        {
            throw new InvalidCastException("A rational number cannot be cast to an Integer");
        }

        public int ToInt32(IFormatProvider provider)
        {
            throw new InvalidCastException("A rational number cannot be cast to an Integer");
        }

        public long ToInt64(IFormatProvider provider)
        {
            throw new InvalidCastException("A rational number cannot be cast to an Integer");
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new InvalidCastException("A rational number cannot be cast to a Byte-like object");
        }

        public float ToSingle(IFormatProvider provider)
        {
            return (float)ToDouble(provider);
        }

        public string ToString(IFormatProvider provider)
        {
            return ToString();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(this, conversionType, provider);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new InvalidCastException("A rational number cannot be cast to an Integer");
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            throw new InvalidCastException("A rational number cannot be cast to an Integer");
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new InvalidCastException("A rational number cannot be cast to an Integer");
        }

        public int CompareTo(double other)
        {
            return this > other ? 1 : this < other ? -1 : 0;
        }

        public int CompareTo(decimal other)
        {
            return (decimal)this > other ? 1 : (decimal)this < other ? -1 : 0;
        }

        public bool Equals(double other)
        {
            return this == other;
        }

        public bool Equals(decimal other)
        {
            return (decimal)this == other;
        }

        public int CompareTo(long other)
        {
            return (long)this > other ? 1 : (long)this < other ? -1 : 0;
        }

        public bool Equals(long other)
        {
            return (long)this == other;
        }

        public int CompareTo(ulong other)
        {
            return (ulong)this > other ? 1 : (ulong)this < other ? -1 : 0;
        }

        public bool Equals(ulong other)
        {
            return (ulong)this == other;
        }
        public bool Equals(Rational other)
        {
            return (double)this == other;
        }

        public int CompareTo(Rational other)
        {
            return this > other ? 1 : (ulong)this < other ? -1 : 0;
        }

        public static Rational operator +(Rational left, Rational right)
        {
            return left.Add(right);
        }
        public static bool operator ==(Rational left, Rational right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Rational left, Rational right)
        {
            return !left.Equals(right);
        }
        public static explicit operator Rational(ulong ul)
        {
            return new Rational((int)ul);
        }
        public static explicit operator Rational(long lg)
        {
            return new Rational((int)lg);
        }
        public static explicit operator Rational(decimal dc)
        {
            return FromDouble((double)dc);
        }
        public static implicit operator Rational(double db)
        {
            return FromDouble(db);
        }
        public static explicit operator ulong(Rational rational)
        {
            return (ulong)rational.AsDouble();
        }
        public static explicit operator long(Rational rational)
        {
            return (long)rational.AsDouble();
        }
        public static explicit operator decimal(Rational rational)
        {
            return (decimal)rational.AsDouble();
        }
        public static implicit operator double(Rational rational)
        {
            return rational.AsDouble();
        }
    }
}
