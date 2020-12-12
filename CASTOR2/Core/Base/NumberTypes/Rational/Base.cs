using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace CASTOR2.Core.Base.NumberTypes.Rational
{
    public abstract class RationalBase : MathObject, Interfaces.IMathType<RationalBase>
    {
        public virtual RationalBase Simplify()
        {
            return this;
        }
    }
    public class RationalVariable : RationalBase, Interfaces.IVariable
    {
        public RationalVariable(string display, bool constant = false)
        {
            Display = display;
            Constant = constant;
        }
        public bool Constant { get; }

        public string Display { get; }
    }
}
