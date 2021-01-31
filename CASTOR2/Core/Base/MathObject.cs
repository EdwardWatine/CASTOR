using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace CASTOR2.Core.Base
{
    public abstract class MathObject
    {
        public readonly System System = Settings.DefaultSystem;
        public readonly ImmutableHashSet<Interfaces.IVariable> ContainedVariables = ImmutableHashSet<Interfaces.IVariable>.Empty;
        public bool Simplified { get; protected set; } = false;
    }
}
