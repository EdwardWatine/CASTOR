using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Immutable;
using System.Text;
using CASTOR2.Core.Base.Interfaces;
using System.Linq;
using CASTOR2.Core.Base.Operations.Interfaces;
using CASTOR2.Core.Base.Operations.Templates;

namespace CASTOR2.Core.Base.NumberTypes.Real
{
    public class Add : RealBase, IAssociativeOperation<RealBase>
    {
        public Add(params RealBase[] arguments)
        {
            Arguments = arguments.ToImmutableList();
        }
        public Add(IEnumerable<RealBase> arguments, bool simplified = false)
        {
            Arguments = arguments.ToImmutableList();
            Simplified = simplified;
        }
        public IImmutableList<RealBase> Arguments { get; }

        public RealBase Argument => null;

        private static readonly AddTemplate<RealBase, Rational, Multiply> AddTemplate = 
            new AddTemplate<RealBase, Rational, Multiply>(Rational.Zero, Rational.One);
        public override RealBase Simplify()
        {
            if (Simplified)
            {
                return this;
            }
            IList<RealBase> newArguments = AddTemplate.ApplyAdditionRules(Arguments.Select(real => real.Simplify()));
            if (newArguments.Count == 1)
            {
                return newArguments[0];
            }
            return new Add(newArguments, true);
        }
        public override int GetHashCode()
        {
            return Arguments.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return obj is Add add && ContainedVariables == add.ContainedVariables && Arguments == add.Arguments;
        }
        public override IEnumerable<RealBase> AsAddition()
        {
            return Arguments;
        }

        public RealBase JoinArguments()
        {
            return this;
        }
        public override string ToString()
        {
            return string.Join("+", Arguments);
        }
    }
}
