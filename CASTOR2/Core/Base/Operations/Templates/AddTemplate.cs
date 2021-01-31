using System;
using System.Collections.Generic;
using System.Linq;
using CASTOR2.Core.Base.Interfaces;
using CASTOR2.Core.Base.Operations.Interfaces;

namespace CASTOR2.Core.Base.Operations.Templates
{
    public class AddTemplate<TArgument, TExplicit, TField, TFieldExplicit, TMultiply> where TExplicit : TArgument, IAdd<TExplicit>, IEquatable<TExplicit> 
        where TArgument : MathObject, IMathType<TArgument>, IAdd<TArgument>, IMultiply<TArgument, TField> where TField : MathObject, IField<TField>
        where TFieldExplicit : TField, INumeric where TMultiply : TArgument, IAssociativeOperation<TArgument, TField>
    {
        private readonly TExplicit ExplicitZero;
        private readonly TFieldExplicit One;
        private readonly TFieldExplicit Zero;
        public AddTemplate(TExplicit zero, TFieldExplicit numericZero, TFieldExplicit one)
        {
            ExplicitZero = zero;
            Zero = numericZero;
            One = one;
        }
        public IList<TArgument> ApplyAdditionRules(IEnumerable<TArgument> oldArgs)
        {
            Dictionary<TArgument, TField> coefficientMapping = new Dictionary<TArgument, TField>();
            IEnumerable<TArgument> arguments = oldArgs.SelectMany(arg => arg.Simplify().AsAddition());
            TExplicit explicitCount = ExplicitZero;
            IList<TArgument> newArgs = new List<TArgument>();
            foreach (TArgument argument in arguments)
            {
                if (argument is TExplicit @explicit)
                {
                    explicitCount = explicitCount.Add(@explicit);
                    continue;
                }
                TField numeric = One;
                TArgument symbolic = argument;
                if (argument is TMultiply multiply)
                {
                    numeric = multiply.JoinArguments();
                    symbolic = multiply.Argument;
                }
                if (!coefficientMapping.ContainsKey(symbolic))
                {
                    coefficientMapping[symbolic] = numeric;
                    continue;
                }
                coefficientMapping[symbolic] = coefficientMapping[symbolic].Add(numeric);
            }
            if (!explicitCount.Equals(ExplicitZero))
            {
                newArgs.Add(explicitCount);
            }
            foreach (KeyValuePair<TArgument, TField> kvp in coefficientMapping)
            {
                if (!kvp.Value.Equals(Zero))
                {
                    if (kvp.Value.Equals(One))
                    {
                        newArgs.Add(kvp.Key);
                    }
                    else
                    {
                        newArgs.Add(kvp.Key.Multiply(kvp.Value));
                    }
                }
            }
            return newArgs;
        }
    }
    public class AddTemplate<TField, TExplicit, TMultiply> where TExplicit : TField, IMultiply<TExplicit>, IAdd<TExplicit>, IEquatable<TExplicit>
        where TField : MathObject, IMathType<TField>, IField<TField> where TMultiply : TField, IAssociativeOperation<TField, TField>
    {
        private readonly TExplicit One;
        private readonly TExplicit Zero;
        public AddTemplate(TExplicit zero, TExplicit one)
        {
            Zero = zero;
            One = one;
        }
        public IList<TField> ApplyAdditionRules(IEnumerable<TField> oldArgs)
        {
            Dictionary<TField, TExplicit> coefficientMapping = new Dictionary<TField, TExplicit>();
            IEnumerable<TField> arguments = oldArgs.SelectMany(arg => arg.Simplify().AsAddition());
            TExplicit explicitCount = Zero;
            IList<TField> newArgs = new List<TField>();
            foreach (TField argument in arguments)
            {
                if (argument is TExplicit @explicit)
                {
                    explicitCount = explicitCount.Add(@explicit);
                    continue;
                }
                TExplicit numeric = One;
                TField symbolic = numeric;
                if (argument is TMultiply multiply)
                {
                    TField coefficientCount = null;
                    bool found = false;
                    foreach (TField fieldObj in multiply.Arguments)
                    {
                        if (!found && fieldObj is TExplicit expl)
                        {
                            numeric = numeric.Multiply(expl);
                            found = true;
                        }
                        else
                        {
                            coefficientCount = coefficientCount?.Multiply(coefficientCount) ?? coefficientCount;
                        }
                    }
                    symbolic = coefficientCount;
                }
                if (!coefficientMapping.ContainsKey(symbolic))
                {
                    coefficientMapping[symbolic] = numeric;
                    continue;
                }
                coefficientMapping[symbolic] = coefficientMapping[symbolic].Add(numeric);
            }
            if (!explicitCount.Equals(Zero))
            {
                newArgs.Add(explicitCount);
            }
            foreach (KeyValuePair<TField, TExplicit> kvp in coefficientMapping)
            {
                if (!kvp.Value.Equals(Zero))
                {
                    if (kvp.Value.Equals(One))
                    {
                        newArgs.Add(kvp.Key);
                    }
                    else
                    {
                        newArgs.Add(kvp.Key.Multiply(kvp.Value));
                    }
                }
            }
            return newArgs;
        }
    }
}
