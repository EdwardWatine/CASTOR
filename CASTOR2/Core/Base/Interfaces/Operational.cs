using System;
using System.Collections.Generic;
using System.Text;

namespace CASTOR2.Core.Base.Interfaces
{
    public interface IVariable
    {
        bool Variable { get; }
        string Display { get; }
    }
    public interface IBinaryOperation<TLeft, TRight, out TOut> where TLeft : MathObject where TRight : MathObject where TOut : MathObject
    {
        TOut Simplify();
        IList<TLeft> LeftArguments { get; }
        IList<TRight> RightArguments { get; }

    }
}
