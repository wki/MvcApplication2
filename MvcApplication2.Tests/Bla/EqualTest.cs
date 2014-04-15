using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MvcApplication2.Tests.Bla
{
    [TestClass]
    public class EqualTest
    {
        [TestMethod]
        public void CheckEqual()
        {
            Assert.AreEqual(17, 17);
        }
    }
}
