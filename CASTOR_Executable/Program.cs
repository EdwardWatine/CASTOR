using System;
using CASTOR2.Core.Base.NumberTypes;
namespace CASTOR_Executable
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new Rational(3, 2) + 0.5 + 1);
        }
    }
}
