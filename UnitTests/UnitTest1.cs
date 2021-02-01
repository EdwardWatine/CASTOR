using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CASTOR2.Core.Base.NumberTypes.Real;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RationalTests()
        {
            Assert.AreEqual(0.5 + new Rational(1), new Rational(6, 4));
            Assert.AreEqual(new Add(Rational.Zero, new Rational(1)).Simplify().ToString(), "1");
            Assert.AreEqual(new Add(new Rational(1), new Rational(2)).Simplify().ToString(), "3");
            Assert.AreEqual(Rational.Zero + 0.5 - 0.6 + 0.1, 0);
            Assert.AreEqual(Rational.One * 0.1 * -20, new Rational(-2));
            Assert.AreEqual(new Rational(3, 2) / new Rational(6, 12), 3);
        }
    }
}
