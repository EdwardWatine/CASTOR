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
        static Add()
        {
            Operations.OperatorPrecendence.SetPrecedence(typeof(Add), Operations.Precedence.Addition);
        }
        public Add(params RealBase[] arguments) : this(arguments, false) { }
        public Add(IEnumerable<RealBase> arguments, bool simplified = false)
        {
            Arguments = arguments.SelectMany(arg => arg.AsAddition()).SortMathObjects().ToImmutableList();
            Simplified = simplified;
            ContainedVariables = Arguments.Select(arg => arg.ContainedVariables).Aggregate(ContainedVariables, (result, current) => result.Union(current));
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
            if (newArguments.Count == 0)
            {
                return Rational.Zero;
            }
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
            return obj != null && obj.ToString() == ToString() && obj is Add add &&
                ContainedVariables.SetEquals(add.ContainedVariables) && Arguments.SequenceEqual(add.Arguments);
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
