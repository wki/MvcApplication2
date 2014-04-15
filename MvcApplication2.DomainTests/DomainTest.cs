using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcApplication2.Domain;
using Ninject;

namespace MvcApplication2.DomainTests
{
    [TestClass]
    public class DomainTest
    {
        [TestMethod]
        public void DomainUninitialized()
        {
            try
            {
                var x = DomainLayer.Instance;
            }
            catch (UninitializedException)
            {
                Assert.IsTrue(true, "Uninitialized Exception caught");
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void DomainSetup()
        {
            DomainLayer.Setup();

            Assert.IsNotNull(DomainLayer.Instance);
        }

        [TestMethod]
        public void NinjectKernel()
        {
            DomainLayer.Setup();

            Assert.IsNotNull(DomainLayer._kernel);
        }

        [TestMethod]
        public void SubDomain_Measurement()
        {
            DomainLayer.Setup();

            Assert.IsNotNull(DomainLayer.Instance.measurement);
            Assert.IsInstanceOfType(DomainLayer.Instance.measurement, typeof(Measurement));

            var m = DomainLayer.Instance.measurement;
            Assert.AreSame(m, DomainLayer.Instance.measurement);
        }
    }
}
