using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CASTOR2.Core.Base.Interfaces;

namespace CASTOR2.Core.Base.NumberTypes.Rational
{
    public class Add : RationalBase, ICommutativeOperation<RationalBase>
    {
        public Add(params RationalBase[] arguments)
        {
            Arguments = new ReadOnlyCollection<RationalBase>(new List<RationalBase>(arguments));
        }
        private Add(List<RationalBase> arguments, bool simplified = false)
        {
            Arguments = arguments.AsReadOnly();
            Simplified = simplified;
        }
        public IReadOnlyCollection<RationalBase> Arguments { get; }
        public static CommutativeSimplificationLookup<RationalBase> SimplificationLookup = new CommutativeSimplificationLookup<RationalBase>();

        public override RationalBase Simplify()
        {
            if (Simplified)
            {
                return this;
            }
            List<RationalBase> newArguments = OperationHelpers.SimplifyCommutativeOperation(SimplificationLookup, this);
            if (newArguments.Count == 1)
            {
                return newArguments[0];
            }
            return new Add(newArguments, true);
        }
    }
}
