using CASTOR2.Core.Base.Interfaces;
using CASTOR2.Core.Base.Operations.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CASTOR2.Core.Base.NumberTypes.Real
{
    public partial class Rational : RealBase, IField<Rational>, IReal, IConvertible, IEquatable<Rational>, IComparable<Rational>
    {
        public static Rational Zero = new Rational(0, 1, 0, true);
        public static Rational One = new Rational(1, 1, 0, true);
        private static int LCM(int num1, int num2)
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
        public static Rational FromDouble(double value, double accuracy = 1e-6)
        {
            if (accuracy <= 0.0 || accuracy >= 1.0)
            {
                throw new ArgumentOutOfRangeException("The accuracy must be between one and zero");
            }

            int sign = Math.Sign(value);

            value *= sign;

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
        public override RealBase Simplify()
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
        public Rational Add(Rational other)
        {
            int lcm = LCM(Denominator, other.Denominator);
            int rm = lcm / other.Denominator;
            int lm = lcm / Denominator;
            int max;
            if (Exponent > other.Exponent)
            {
                rm *= (int)Math.Pow(10, Exponent - other.Exponent);
                max = Exponent;
            }
            else
            {
                lm *= (int)Math.Pow(10, other.Exponent - Exponent);
                max = other.Exponent;
            }
            return new Rational(Numerator * lm + other.Numerator * rm, lcm, max, false);
        }
        IEnumerable<Rational> IAdd<Rational>.AsAddition()
        {
            return this.Yield();
        }
        public Rational Multiply(Rational other)
        {
            int hcf1 = GCD(Numerator, other.Denominator);
            int hcf2 = GCD(Denominator, other.Numerator);
            int hcff = hcf1 * hcf2;
            return new Rational(Numerator * other.Numerator / hcff, Denominator * other.Denominator / hcff, Exponent + other.Exponent, Simplified && other.Simplified);
        }
        public static Rational operator +(Rational left, Rational right)
        {
            return left.Add(right);
        }
        public static Rational operator -(Rational left, Rational right)
        {
            return left + -right;
        }
        public static Rational operator -(Rational rational)
        {
            return new Rational(-rational.Numerator, rational.Denominator, rational.Exponent, true);
        }
        public static Rational operator *(Rational left, Rational right)
        {
            return left.Multiply(right);
        }
        private Rational Flip(int exp)
        {
            return new Rational(Denominator, Numerator, exp, Simplified);
        }
        public static Rational operator /(Rational left, Rational right)
        {
            return left.Multiply(right.Flip(-right.Exponent));
        }
        public static bool operator ==(Rational left, Rational right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Rational left, Rational right)
        {
            return !left.Equals(right);
        }
    }
}
