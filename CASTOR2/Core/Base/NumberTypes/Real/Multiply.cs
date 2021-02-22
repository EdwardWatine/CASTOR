using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Immutable;
using System.Text;
using CASTOR2.Core.Base.Interfaces;

namespace CASTOR2.Core.Base.NumberTypes.Real
{
    public class Multiply : RealBase, IAssociativeOperation<RealBase>
    {
        static Multiply()
        {
            Operations.OperatorPrecendence.SetPrecedence(typeof(Multiply), Operations.Precedence.Multiplication);
        }
        public Multiply(params RealBase[] arguments)
        {
            Arguments = arguments.ToImmutableList();
        }
        public Multiply(IEnumerable<RealBase> arguments, bool simplified = false)
        {
            Arguments = arguments.ToImmutableList();
            Simplified = simplified;
        }
        public IImmutableList<RealBase> Arguments { get; }

        public RealBase Argument => null;

        public override RealBase Simplify()
        {
            if (Simplified)
            {
                return this;
            }
            var newArguments = Arguments;
            if (newArguments.Count == 1)
            {
                return newArguments[0];
            }
            return new Multiply(newArguments, true);
        }
        public override int GetHashCode()
        {
            return Arguments.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return obj != null && obj.ToString() == ToString() &&obj is Multiply mul && 
                ContainedVariables == mul.ContainedVariables && Arguments == mul.Arguments;
        }

        public RealBase JoinArguments()
        {
            return this;
        }

        public override IEnumerable<RealBase> AsAddition()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return string.Join("*", Arguments);
        }
    }
}
