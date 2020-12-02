using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace CASTOR2.Core.Base.NumberTypes.Rational
{
    public abstract class RationalBase : MathObject, Interfaces.IScalar
    {
        public virtual RationalBase Simplify()
        {
            return this;
        }
    }
    public class RationalVariable : RationalBase, Interfaces.IVariable
    {
        public bool Variable => throw new NotImplementedException();

        public string Display => throw new NotImplementedException();
    }
}
