using System;
using System.Collections.Generic;
using System.Text;

namespace CASTOR2.Core.Base.TypeAliasSubclasses
{
    public class SimplificationLookup<TLeft, TRight, TOut> :
        Tuple<Dictionary<Type, Func<TLeft, TRight, TOut>>, Dictionary<Type, Func<TLeft, TRight, TOut>>>
    {
        public SimplificationLookup() : base(new Dictionary<Type, Func<TLeft, TRight, TOut>>(), new Dictionary<Type, Func<TLeft, TRight, TOut>>())
        {
        }
    }
    public class CommutativeSimplificationLookup<TLeft, TRight, TOut> :
        Dictionary<Type, Func<TLeft, TRight, TOut>>
    {

    }
}
