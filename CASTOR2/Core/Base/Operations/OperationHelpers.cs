using System;
using System.Collections.Generic;
using System.Text;

namespace CASTOR2.Core.Base
{
    public struct Split<TOperation, TSpecific, TOther>
    {
        public TOperation Specific;
        public TOperation Other;
        public Split(TOperation specific, TOperation other)
        {
            Specific = specific;
            Other = other;
        }
    }
}
