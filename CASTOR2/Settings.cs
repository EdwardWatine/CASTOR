using System;
using System.Collections.Generic;
using System.Text;
using CASTOR2.Core.Base;


namespace CASTOR2
{
    using System = Core.Base.System;
    public class Settings
    {
        public static System DefaultSystem = new System();
        public static void NewSystem()
        {
            DefaultSystem = new System();
        }
    }
}
