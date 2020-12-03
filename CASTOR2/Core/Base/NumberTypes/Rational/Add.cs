using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CASTOR2.Core.Base.Interfaces;
using CASTOR2.Core.Base.TypeAliasSubclasses;

namespace CASTOR2.Core.Base.NumberTypes.Rational
{
    using SimpFunc = Func<RationalBase, RationalBase, RationalBase>;
    public class Add : RationalBase, IBinaryOperation<RationalBase, RationalBase, RationalBase>
    {
        public Add(params RationalBase[] arguments)
        {
            RightArguments = new ReadOnlyCollection<RationalBase>(new List<RationalBase>(arguments));
        }
        private Add(List<RationalBase> arguments, bool simplified = false)
        {
            RightArguments = arguments.AsReadOnly();
            Simplified = simplified;
        }
        public IReadOnlyList<RationalBase> Arguments;
        public static CommutativeSimplificationLookup<RationalBase, RationalBase, RationalBase> SimplificationLookup = 
            new CommutativeSimplificationLookup<RationalBase, RationalBase, RationalBase>();

        public IList<RationalBase> LeftArguments { get; }

        public IList<RationalBase> RightArguments { get; }

        public override RationalBase Simplify()
        {
            if (Simplified)
            {
                return this;
            }
            List<RationalBase> NewArguments = new List<RationalBase>();
            IList<Type> Types = new List<Type>();
            foreach (RationalBase argument in RightArguments)
            {
                RationalBase simplified = argument.Simplify();
                if (simplified is Add add)
                {
                    foreach (RationalBase inner_arg in add.Arguments)
                    {
                        SimplifyArgument(NewArguments, Types, inner_arg);
                    }
                }
                else
                {
                    SimplifyArgument(NewArguments, Types, argument);
                }
            }
            if (NewArguments.Count == 1)
            {
                return NewArguments[0];
            }
            return new Add(NewArguments, true);
        }
        private void SimplifyArgument(IList<RationalBase> newArguments, IList<Type> types, RationalBase argument)
        {
            Type arg_type = argument.GetType();
            for (int i = 0; i < newArguments.Count; i++)
            {
                RationalBase newarg = newArguments[i];
                Type type = types[i];
                if (SimplificationLookup.TryGetValue(type, out SimpFunc func))
                {
                    RationalBase res = func(newarg, argument);
                    newArguments[i] = res;
                    types[i] = res.GetType();
                    return;
                }
                if (SimplificationLookup.TryGetValue(arg_type, out func))
                {
                    RationalBase res = func(argument, newarg);
                    newArguments[i] = res;
                    types[i] = res.GetType();
                    return;
                }
            }
            newArguments.Add(argument);
            types.Add(arg_type);
        }
    }
}
