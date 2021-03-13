using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace CASTOR2.Core.Base
{
    public abstract class MathObject
    {
        public static MathObjectComparer MathObjectComparer = new MathObjectComparer();
        public readonly System System = Settings.DefaultSystem;
        public ImmutableHashSet<Interfaces.IVariable> ContainedVariables { get; protected set; } = ImmutableHashSet<Interfaces.IVariable>.Empty;
        public bool Simplified { get; protected set; } = false;
    }
}
