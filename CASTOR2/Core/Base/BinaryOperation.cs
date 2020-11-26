using System;
using System.Collections.Generic;
using System.Text;

namespace CASTOR2.Core.Base
{
    public abstract class Operation<TChain, TOut> where TOut : MathObject where TChain : Interfaces.IChain<TOut>
    {
    }
    public class UnaryOperation<TChain, TIn, TOut> : Operation<TChain, TOut> where TOut : MathObject where TChain : Interfaces.IChain<TOut>
    {
        private readonly Func<TIn, TChain> ChainConstructor;

        public UnaryOperation(Func<TIn, TChain> chainConstructor)
        {
            ChainConstructor = chainConstructor;
        }
        public virtual TOut Do(TIn parameter)
        {
            TChain chain = ChainConstructor(parameter);
            if (!Settings.AutoSimplify)
            {
                return chain.Cast();
            }
            return chain.Simplify();
        }
    }
    public class BinaryOperation<TChain, TLeft, TRight, TOut> : Operation<TChain, TOut> where TOut : MathObject where TChain : Interfaces.IChain<TOut>
    {
        private readonly Func<TLeft, TRight, TChain> ChainConstructor;
        public readonly bool Associative;
        public readonly bool Commututative;

        public BinaryOperation(Func<TLeft, TRight, TChain> chainConstructor, bool associative, bool commututative)
        {
            ChainConstructor = chainConstructor;
            Associative = associative;
            Commututative = commututative;
        }
        public virtual TOut Do(TLeft left, TRight right)
        {
            TChain chain = ChainConstructor(left, right);
            if (!Settings.AutoSimplify)
            {
                return chain.Cast();
            }
            return chain.Simplify();
        }
    }
}
