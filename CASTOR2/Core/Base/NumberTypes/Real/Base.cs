﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using CASTOR2.Core.Base.Interfaces;
using CASTOR2.Core.Base.Operations.Interfaces;

namespace CASTOR2.Core.Base.NumberTypes.Real
{
    public abstract class RealBase : MathObject, IMathType<RealBase>, IField<RealBase>
    {
        public RealBase Add(RealBase right)
        {
            return new Add(this, right);
        }

        public abstract IEnumerable<RealBase> AsAddition();

        public RealBase Multiply(RealBase right)
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
    }
}
