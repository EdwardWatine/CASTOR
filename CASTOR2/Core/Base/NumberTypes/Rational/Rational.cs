using System;
using System.Collections.Generic;
using System.Text;

namespace CASTOR2.Core.Base.NumberTypes.Rational
{
    public class Rational : RationalBase, IConvertible, Interfaces.IReal
    {
        public static int LCM(int num1, int num2)
        {
            return num1 * num2 / GCD(num1, num2);
        }
        private static int GCD(int x, int y)
        {
            x = Math.Abs(x);
            y = Math.Abs(y);
            while (x * y != 0)
            {
                if (x >= y)
                {
                    x %= y;
                }
                else
                {
                    y %= x;
                }
            }
            return x + y;
        }
        static Rational()
        {
            Add.SimplificationLookup[typeof(Rational)] = new Func<RationalBase, RationalBase, RationalBase>((l, r) => AddRational((Rational)l, (Rational)r));
        }
        public static Rational FromDouble(double value, double accuracy = 1e-6)
        {
            if (accuracy <= 0.0 || accuracy >= 1.0)
            {
                throw new ArgumentOutOfRangeException("The accuracy must be between one and zero");
            }

            int sign = Math.Sign(value);

            if (sign == -1)
            {
                value = Math.Abs(value);
            }

            // Accuracy is the maximum relative error; convert to absolute maxError
            double maxError = sign == 0 ? accuracy : value * accuracy;

            int n = (int)Math.Floor(value);
            value -= n;

            if (value < maxError)
            {
                return new Rational(sign * n, 1, 0, true);
            }

            if (1 - maxError < value)
            {
                return new Rational(sign * (n + 1), 1, 0, true);
            }

            double z = value;
            int previousDenominator = 0;
            int denominator = 1;
            int numerator;

            do
            {
                z = 1.0 / (z - (int)z);
                int temp = denominator;
                denominator = denominator * (int)z + previousDenominator;
                previousDenominator = temp;
                numerator = Convert.ToInt32(value * denominator);
            }
            while (Math.Abs(value - (double)numerator / denominator) > maxError && z != (int)z);

            return new Rational((n * denominator + numerator) * sign, denominator, 0, true);
        }
        public Rational(int integer) : this(integer, 1, 0, true)
        { }
        public Rational(int numerator, int denominator) : this(numerator, denominator, 0, false)
        { }
        public Rational(int numerator, int denominator, int exponent = 0, bool simplified = false)
        {
            Simplified = simplified;
            Exponent = exponent;
            Numerator = numerator;
            Denominator = denominator;
        }
        public Rational(Rational numerator, Rational denominator) : this(LCM(numerator.Numerator, denominator.Denominator),
                                                                         LCM(numerator.Denominator, denominator.Numerator),
                                                                         numerator.Exponent - denominator.Exponent, numerator.Simplified && denominator.Simplified)
        { }
        public override RationalBase Simplify()
        {
            if (Simplified)
            {
                return this;
            }
            int numerator = Numerator;
            int denominator = Denominator;
            int exponent = Exponent;
            while (numerator % 10 == 0)
            {
                numerator %= 10;
                exponent += 1;
            }
            while (denominator % 10 == 0)
            {
                denominator %= 10;
                exponent -= 1;
            }
            int gcd = GCD(numerator, denominator);
            numerator = numerator * Math.Sign(denominator) / gcd;
            denominator = denominator * Math.Sign(denominator) / gcd;
            return new Rational(numerator, denominator, exponent, true);
        }
        public readonly int Numerator;
        public readonly int Denominator;
        public readonly int Exponent;
        public override bool Equals(object obj)
        {
            if (obj is Rational rational)
            {
                return Equals(rational);
            }
            return Helpers.Equals(this, obj);
        }
        public override string ToString()
        {
            return Numerator.ToString() + (Denominator == 1 ? "" : $"/{Denominator}") + (Exponent == 0 ? "" : $" e{Exponent}");
        }
        public override int GetHashCode() => 13 * Numerator + 17 * Denominator - 23 * Exponent;

        public double AsDouble()
        {
            return (double)Numerator / Denominator * Math.Pow(10, Exponent);
        }
        private static Rational AddRational(Rational left, Rational right)
        {
            int lcm = LCM(left.Denominator, right.Denominator);
            int rm = lcm / right.Denominator;
            int lm = lcm / left.Denominator;
            int max;
            if (left.Exponent > right.Exponent)
            {
                rm *= (int)Math.Pow(10, left.Exponent - right.Exponent);
                max = left.Exponent;
            }
            else
            {
                lm *= (int)Math.Pow(10, right.Exponent - left.Exponent);
                max = right.Exponent;
            }
            return new Rational(left.Numerator * lm + right.Numerator * rm, lcm, max, false);
        }
        public static Rational operator +(Rational left, Rational right)
        {
            return AddRational(left, right);
        }
        public static bool operator ==(Rational left, Rational right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Rational left, Rational right)
        {
            return !left.Equals(right);
        }
        public TypeCode GetTypeCode()
        {
            return TypeCode.Double;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return Numerator != 0;
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

        public int CompareTo(Rational other)
        {
            return Numerator * other.Denominator * (int)Math.Pow(10, Exponent) - Denominator * other.Numerator * (int)Math.Pow(10, other.Exponent);
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

        public bool Equals(Rational other)
        {
            return Numerator * other.Denominator == Denominator * other.Numerator && Exponent == other.Exponent;
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
    }
}
