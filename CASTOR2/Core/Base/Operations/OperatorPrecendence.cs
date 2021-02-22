using System;
using System.Collections.Generic;
using System.Linq;
using CASTOR2.Core.Base.Exceptions;

namespace CASTOR2.Core.Base.Operations
{
    public enum Precedence
    {
        UnderAddition,
        Addition,
        UnderMultiplication,
        Multiplication,
        UnderExponentiation,
        Exponentiation,
        OverExponentation
    }
    public static class OperatorPrecendence
    {
        private static Dictionary<Type, Precedence> pMapping = new Dictionary<Type, Precedence>();
        public static void SetPrecedence(Type type, Precedence precedence)
        {
            pMapping[type] = precedence;
        }
        public static Precedence? GetPrecedence(Type type)
        {
            return pMapping.GetDefault(type);
        }
        public static bool IsPrecedenceGreater(Type type, Precedence precedence)
        {
            Precedence? p = pMapping.GetDefault(type);
            return p != null && p - precedence > 0;
        }
        public static bool IsPrecedenceLess(Type type, Precedence precedence)
        {
            Precedence? p = pMapping.GetDefault(type);
            return p != null && p - precedence < 0;
        }
        public static bool IsPrecedenceEqual(Type type, Precedence precedence)
        {
            Precedence? p = pMapping.GetDefault(type);
            return p != null && p - precedence == 0;
        }
    }
}
