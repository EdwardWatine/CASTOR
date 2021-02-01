using System;
using System.Collections.Generic;
using System.Linq;
using CASTOR2.Core.Base.Interfaces;
using CASTOR2.Core.Base.NumberTypes.Real;

namespace CASTOR2.Core.Base.Operations.Templates
{/***
    public class ChainedMultiplication<TArgument, TExplicit, TNumeric, TMultiply> where TExplicit : TArgument, IAdd<TExplicit>, IEquatable<TExplicit> 
        where TArgument : IAdd<TArgument> where TNumeric : INumeric, IAdd<TNumeric>, IEquatable<TNumeric>
        where TMultiply : TArgument, IAssociativeOperation<TArgument>, INumericSplit<TNumeric, TArgument>
    {
        private readonly TExplicit ExplicitZero;
        private readonly TNumeric One;
        private readonly TNumeric Zero;
        private readonly Func<TNumeric, TArgument, TMultiply> MultiplyConstructor;
        public AddTemplate(TExplicit zero, TNumeric numericZero, TNumeric one,
            Func<TNumeric, TArgument, TMultiply> multiplyConstructor)
        {
            ExplicitZero = zero;
            Zero = numericZero;
            One = one;
            MultiplyConstructor = multiplyConstructor;
        }
        public IList<TArgument> ApplyAdditionRules(IAssociativeOperation<TArgument> operation)
        {
            Dictionary<TArgument, TNumeric> coefficientCount = new Dictionary<TArgument, TNumeric>();
            IEnumerable<TArgument> arguments = operation.Arguments.SelectMany(arg => arg.AsAddition().Arguments);
            TExplicit explicitCount = ExplicitZero;
            IList<TArgument> newArgs = new List<TArgument>();
            foreach (TArgument argument in arguments)
            {
                if (argument is TExplicit @explicit)
                {
                    explicitCount = explicitCount.Add(@explicit);
                    continue;
                }
                TNumeric numeric = One;
                TArgument symbolic = argument;
                if (argument is TMultiply multiply)
                {
                    NumericSplit<TNumeric, TArgument> split = multiply.Split();
                    numeric = split.Numeric;
                    symbolic = split.Symbolic;
                }
                if (!coefficientCount.ContainsKey(symbolic))
                {
                    coefficientCount[symbolic] = numeric;
                    continue;
                }
                coefficientCount[symbolic] = coefficientCount[symbolic].Add(numeric);
            }
            if (!explicitCount.Equals(ExplicitZero))
            {
                newArgs.Add(explicitCount);
            }
            foreach (KeyValuePair<TArgument, TNumeric> kvp in coefficientCount)
            {
                if (!kvp.Value.Equals(Zero))
                {
                    if (kvp.Value.Equals(One))
                    {
                        newArgs.Add(kvp.Key);
                    }
                    else
                    {
                        newArgs.Add(MultiplyConstructor(kvp.Value, kvp.Key));
                    }
                }
            }
            return newArgs;
        }
    }***/
}
