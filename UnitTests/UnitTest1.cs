using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CASTOR2.Core.Base.NumberTypes.Rational;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RationalTests()
        {
            Assert.AreEqual(0.5 + new Rational(1), new Rational(6, 4));
            Assert.AreEqual(new Add(new Rational(1), new Rational(2)).Simplify().ToString(), "3");
        }
    }
}
