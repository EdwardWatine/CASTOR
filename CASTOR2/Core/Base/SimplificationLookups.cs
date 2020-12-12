using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CASTOR2.Core.Base
{
    public abstract class SimplificationLookupBase<TLeft, TRight, TOut>
    {
        public TOut Simplify(TLeft left, TRight right)
        {
            Type tleft = left.GetType();
            Type tright = right.GetType();
            Func<TLeft, TRight, TOut> func = GetLeftShortcut(tleft);
            if (func != null)
            {
                TOut result = func(left, right);
                if (result != null)
                {
                    return result;
                }
            }
            func = GetRightShortcut(tright);
            if (func != null)
            {
                TOut result = func(left, right);
                if (result != null)
                {
                    return result;
                }
            }
            func = GetSimplification(tleft, tright);
            return func == null ? default : func(left, right);
        }
        public abstract Func<TLeft, TRight, TOut> GetLeftShortcut(Type left);
        public abstract void SetLeftShortcut(Type left, Func<TLeft, TRight, TOut> shortcut);
        public abstract Func<TLeft, TRight, TOut> GetRightShortcut(Type right);
        public abstract void SetRightShortcut(Type right, Func<TLeft, TRight, TOut> shortcut);
        public abstract Func<TLeft, TRight, TOut> GetSimplification(Type left, Type right);
        public abstract void SetSimplification(Type left, Type right, Func<TLeft, TRight, TOut> simplification);
    }
    public class SimplificationLookup<TLeft, TRight, TOut> : SimplificationLookupBase<TLeft, TRight, TOut>
    {
        private readonly Dictionary<Type, Func<TLeft, TRight, TOut>> leftShortcupLookup = new Dictionary<Type, Func<TLeft, TRight, TOut>>();
        private readonly Dictionary<Type, Func<TLeft, TRight, TOut>> rightShortcutLookup = new Dictionary<Type, Func<TLeft, TRight, TOut>>();
        private readonly Dictionary<Tuple<Type, Type>, Func<TLeft, TRight, TOut>> simplificationLookup = new Dictionary<Tuple<Type, Type>, Func<TLeft, TRight, TOut>>();
        public override void SetLeftShortcut(Type left, Func<TLeft, TRight, TOut> shortcut)
        {
            if (leftShortcupLookup.ContainsKey(left))
            {
                throw new InvalidOperationException($"There is already a left shortcut defined for type {left.Name}");
            }
            leftShortcupLookup[left] = shortcut;
        }
        public override void SetRightShortcut(Type right, Func<TLeft, TRight, TOut> shortcut)
        {
            if (rightShortcutLookup.ContainsKey(right))
            {
                throw new InvalidOperationException($"There is already a left shortcut defined for type {right.Name}");
            }
            leftShortcupLookup[right] = shortcut;
        }
        public override void SetSimplification(Type left, Type right, Func<TLeft, TRight, TOut> simplification)
        {
            Tuple<Type, Type> typePair = new Tuple<Type, Type>(left, right);
            if (simplificationLookup.ContainsKey(typePair))
            {
                throw new InvalidOperationException($"There is already a simplification defined for types {left.Name} and {right.Name}");
            }
            simplificationLookup[typePair] = simplification;
        }
        public override Func<TLeft, TRight, TOut> GetLeftShortcut(Type left)
        {
            return leftShortcupLookup.GetDefault(left);
        }

        public override Func<TLeft, TRight, TOut> GetRightShortcut(Type right)
        {
            return rightShortcutLookup.GetDefault(right);
        }

        public override Func<TLeft, TRight, TOut> GetSimplification(Type left, Type right)
        {
            return simplificationLookup[new Tuple<Type, Type>(left, right)];
        }
    }
    public sealed class CommutativeSimplificationLookup<TArgument> : SimplificationLookup<TArgument, TArgument, TArgument>
    {
        public void SetShortcut(Type argument, Func<TArgument, TArgument, TArgument> shortcut)
        {
            SetLeftShortcut(argument, shortcut);
            SetRightShortcut(argument, (l, r) => shortcut(r, l));
        }
        public override void SetSimplification(Type left, Type right, Func<TArgument, TArgument, TArgument> simplification)
        {
            base.SetSimplification(left, right, simplification);
            if (left != right)
            {
                base.SetSimplification(right, left, (l, r) => simplification(r, l));
            }
        }
    }
}
