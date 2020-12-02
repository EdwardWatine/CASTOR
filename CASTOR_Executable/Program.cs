using System;
using CASTOR2.Core.Base.NumberTypes.Rational;
namespace CASTOR_Executable
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine((0.5 + new Rational(1, 6)).Simplify().ToString());
        }
    }
}
