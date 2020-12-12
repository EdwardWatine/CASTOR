using System;
using System.Collections.Generic;
using System.Text;

namespace CASTOR2.Core.Base.Interfaces
{
    public interface IVariable
    {
        bool Constant { get; }
        string Display { get; }
    }
    public interface IBinaryOperation<TLeft, TRight, TOut>
    {
        IList<TLeft> LeftArguments { get; }
        IList<TRight> RightArguments { get; }
    }
    public interface ICommutativeOperation<TArgument>
    {
        IReadOnlyCollection<TArgument> Arguments { get; }
    }
    public interface IMathType<T>
    {
        T Simplify();
    }
}
