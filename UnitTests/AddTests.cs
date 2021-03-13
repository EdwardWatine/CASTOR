using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CASTOR2.Core.Base.NumberTypes.Real;
using static CASTOR2.Core.Exports.RealVariables;

namespace UnitTests
{
    [TestClass]
    public class AddTests
    {
        [TestMethod]
        public void AddRational()
        {
            Assert.AreEqual(new Add(Rational.One, -1).Simplify(), 0);
        }
        [TestMethod]
        public void AddCoeff()
        {
            Assert.AreEqual(new Add(x, -x).Simplify(), 0);
            Assert.AreEqual(new Add(x, new Multiply(-3, x)).Simplify(), new Multiply(-2, x));
        }
        [TestMethod]
        public void AddSame()
        {
            Assert.AreEqual(new Add(x, x).Simplify(), new Multiply(2, x));
        }
        [TestMethod]
        public void AddMultipleTerms()
        {
            Assert.AreEqual(new Add(x + y + z + x + y + x).Simplify(), 3 * x + 2 * y + z);
        }
    }
}
