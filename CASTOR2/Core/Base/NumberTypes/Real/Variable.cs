using System;
using System.Collections.Generic;
using System.Text;

namespace CASTOR2.Core.Base.NumberTypes.Real
{
    public class Variable : RealBase, Interfaces.IVariable
    {
        public bool Constant { get; }
        public string Display { get; }
        public Variable(string display, bool constant = false)
        {
            Display = display;
            Constant = constant;
        }
        public static implicit operator Variable(Base.Variable variable)
        {
            return new Variable(variable.Display, variable.Constant);
        }
        public override RealBase Simplify()
        {
            return this;
        }
    }
}
