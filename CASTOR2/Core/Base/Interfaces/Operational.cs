using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace CASTOR2.Core.Base.Interfaces
{
    public interface IVariable
    {
        bool Constant { get; }
        string Display { get; }
    }
    public interface IOperation<TLeft, TRight>
    {
        TLeft Left { get; }
        TRight Right { get; }
    }
    public interface ICommutativeOperation<TInOut, TOther>
    {
        TInOut InOut { get; }
        TOther Other { get; }
    }
    public interface IAssociativeOperation<TArgument> : IAssociativeOperation<TArgument, TArgument>
    {
        new IImmutableList<TArgument> Arguments { get; }
    }
    public interface IAssociativeOperation<TInOut, TOther>
    {
        IImmutableList<TOther> Arguments { get; }
        TInOut Argument { get; }
        TOther JoinArguments();
    }
    public interface IMathType<T>
    {
        T Simplify();
    }
}
