using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CASTOR2.Core.Base.Interfaces;
namespace CASTOR2.Core.Base
{
    public class System
    {
        private static readonly Dictionary<int, System> idMapping = new Dictionary<int, System>();
        private static int idCounter = -1;
        public static System GetSystemById(int id)
        {
            return idMapping[id];
        }
        public static void DeleteSystem(int id)
        {
            idMapping.Remove(id);
        }
        public static void DeleteSystem(System system)
        {
            DeleteSystem(system.Id);
        }
        public static Dictionary<int, System> GetIdMapping()
        {
            return idMapping;
        }


        public readonly HashSet<IVariable> ContainedVariables = new HashSet<IVariable>();
        public readonly int Id;
        public System()
        {
            Id = ++idCounter;
        }
    }
}
