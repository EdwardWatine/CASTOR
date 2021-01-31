using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Immutable;
using System.Text;
using CASTOR2.Core.Base.Interfaces;

namespace CASTOR2.Core.Base.NumberTypes.Real
{
    public class Multiply : RealBase, IAssociativeOperation<RealBase>, INumericSplit<Numeric.Numeric, RealBase>
    {
        public Multiply(params RealBase[] arguments)
        {
            Arguments = arguments.ToImmutableList();
        }
        public Multiply(IList<RealBase> arguments, bool simplified = false)
        {
            Arguments = arguments.ToImmutableList();
            Simplified = simplified;
        }
        public IImmutableList<RealBase> Arguments { get; }
        public IImmutableDictionary<Type, List<int>> TypeMappings { get; }
        public override RealBase Simplify()
        {
            if (Simplified)
            {
                return this;
            }
            List<RealBase> newArguments = OperationHelpers.SimplifyCommutativeOperation(Simplifications, this);
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
            return obj is Multiply mul && ContainedVariables == mul.ContainedVariables && Arguments == mul.Arguments;
        }
    }
}
