using System;
using System.Collections.Generic;
using System.Text;

namespace CASTOR2.Core.Base.Operations.Interfaces
{
    public interface IAdd<TThis>
    {
        TThis Add(TThis right);
        IEnumerable<TThis> AsAddition();
    }
    public interface IMultiply<out TInOut, in TOther>
    {
        TInOut Multiply(TOther right);
    }
    public interface IMultiply<TThis> : IMultiply<TThis, TThis>
    {
        new TThis Multiply(TThis other);
    }
}
