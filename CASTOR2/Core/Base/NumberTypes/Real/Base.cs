using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using CASTOR2.Core.Base.Interfaces;
using CASTOR2.Core.Base.Operations.Interfaces;

namespace CASTOR2.Core.Base.NumberTypes.Real
{
    public abstract class RealBase : MathObject, IMathType<RealBase>, IField<RealBase>
    {
        public virtual RealBase Add(RealBase right)
        {
            return new Add(this, right);
        }

        public virtual IEnumerable<RealBase> AsAddition()
        {
            return this.Yield();
        }

        public virtual RealBase Multiply(RealBase right)
        {
            return new Multiply(this, right);
        }
        public abstract RealBase Simplify();

        public static RealBase operator +(RealBase left, RealBase right)
        {
            return left.Add(right);
        }
        public static RealBase operator -(RealBase left, RealBase right)
        {
            return left.Add(-right);
        }
        public static RealBase operator -(RealBase real)
        {
            return new Rational(-1).Multiply(real);
        }
        public static RealBase operator *(RealBase left, RealBase right)
        {
            return left.Multiply(right);
        }
        public static implicit operator RealBase(int nt)
        {
            return new Rational(nt);
        }
        public static implicit operator RealBase(double db)
        {
            return Rational.FromDouble(db);
        }
    }
}
