using System;
using System.Collections.Generic;
using System.Text;

namespace CASTOR2.Core.Base
{
    public abstract class MathObject
    {
        public readonly System System = Settings.DefaultSystem;
        public readonly HashSet<Interfaces.IVariable> ContainedVariables = new HashSet<Interfaces.IVariable>();
        public bool Simplified { get; protected set; } = false;
    }
}
