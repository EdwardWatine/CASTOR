using System;
using System.Collections.Generic;
using System.Text;

namespace CASTOR2.Core.Base
{
    public static class OperationHelpers
    {
        public static void SimplifyCommutuativeArgument<TArgument>(
            CommutativeSimplificationLookup<TArgument> simplificationLookup,
            IList<TArgument> newArguments, TArgument argument) 
        {
            Type arg_type = argument.GetType();
            for (int i = 0; i < newArguments.Count; i++)
            {
                TArgument result = simplificationLookup.Simplify(newArguments[i], argument);
                if (result != null)
                {
                    newArguments[i] = result;
                    return;
                }
            }
            newArguments.Add(argument);
        }

        public static List<TArgument> SimplifyCommutativeOperation<TOperation, TArgument>(
            CommutativeSimplificationLookup<TArgument> simplificationLookup, TOperation operation) 
            where TOperation : TArgument, Interfaces.ICommutativeOperation<TArgument> where TArgument : Interfaces.IMathType<TArgument>
        {
            List<TArgument> NewArguments = new List<TArgument>();
            foreach (TArgument argument in operation.Arguments)
            {
                TArgument simplified = argument.Simplify();
                if (simplified is TOperation inner_operation)
                {
                    foreach (TArgument inner_arg in inner_operation.Arguments)
                    {
                        SimplifyCommutuativeArgument(simplificationLookup, NewArguments, inner_arg);
                    }
                }
                else
                {
                    SimplifyCommutuativeArgument(simplificationLookup, NewArguments, argument);
                }
            }
            return NewArguments;
        }
    }
}
