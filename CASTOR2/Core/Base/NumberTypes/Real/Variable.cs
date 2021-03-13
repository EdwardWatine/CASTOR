using System;
using System.Collections.Immutable;
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
            ContainedVariables = ImmutableHashSet.Create<Interfaces.IVariable>(this);
        }
        public override RealBase Simplify()
        {
            return this;
        }
        public override string ToString()
        {
            return Display;
        }
    }
}
