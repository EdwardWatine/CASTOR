using System;
using System.Collections.Generic;
using System.Text;

namespace CASTOR2.Core.Base
{
    public class Variable : Interfaces.IVariable
    {
        public bool Constant { get; }

        public string Display { get; }
        public Variable(string display, bool constant = false)
        {
            Display = display;
            Constant = constant;
        }
    }
}
