using System;
using System.Reflection;
using IoCTest.ClassForTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyIoC;
using MyIoC.Exceptions;

namespace IoCTest
{
    [TestClass]
    public class ContainerTest
    {
        private Container container;

        [TestInitialize]
        public void TestMethod1()
        {
            container = new Container();            
        }

        [TestMethod]
        public void CreateInstance_ExecutingAssembly_CustomerBLL()
        {
            container.AddAssembly(Assembly.GetExecutingAssembly());

            var actial = (CustomerBLL)container.CreateInstance(typeof(CustomerBLL));
            
            Assert.IsTrue(actial.GetType() == typeof(CustomerBLL));
        }

        [TestMethod]
        public void CreateInstance_ExecutingAssembly_CustomerBLLProperty()
        {
            container.AddAssembly(Assembly.GetExecutingAssembly());

            var actial = (CustomerBLLProperty)container.CreateInstance(typeof(CustomerBLLProperty));

            Assert.IsTrue(actial.GetType() == typeof(CustomerBLLProperty));
        }

        [TestMethod]
        public void CreateInstance_MainSetInjection_CustomerBll()
        {
            container.AddType(typeof(CustomerBLL));
            container.AddType(typeof(Logger));
            container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

            var customerBll = (CustomerBLL)container.CreateInstance(typeof(CustomerBLL));

            Assert.IsTrue(customerBll.GetType() == typeof(CustomerBLL));
        }

        [TestMethod]
        public void CreateInstance_MainSetInjection_CustomerBLLPropertyHasInjections()
        {
            container.AddType(typeof(CustomerBLLProperty));
            container.AddType(typeof(Logger));
            container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

            var CustomerBLLProperty = (CustomerBLLProperty)container.CreateInstance(typeof(CustomerBLLProperty));

            Assert.IsTrue(CustomerBLLProperty.GetType() == typeof(CustomerBLLProperty));
            Assert.IsTrue(CustomerBLLProperty.CustomerDAL.GetType() == typeof(CustomerDAL));
            Assert.IsTrue(CustomerBLLProperty.logger.GetType() == typeof(Logger));
        }

        [TestMethod]
        public void CreateInstance_ExecutingAssembly_CustomerBLLPropertyHasInjections()
        {
            container.AddAssembly(Assembly.GetExecutingAssembly());

            var CustomerBLLProperty = (CustomerBLLProperty)container.CreateInstance(typeof(CustomerBLLProperty));

            Assert.IsTrue(CustomerBLLProperty.GetType() == typeof(CustomerBLLProperty));
            Assert.IsTrue(CustomerBLLProperty.CustomerDAL.GetType() == typeof(CustomerDAL));
            Assert.IsTrue(CustomerBLLProperty.logger.GetType() == typeof(Logger));
        }

        [TestMethod]
        public void GenericCreateInstance_ExecutingAssembly_CustomerBLL()
        {
            container.AddAssembly(Assembly.GetExecutingAssembly());

            var actial = container.CreateInstance<CustomerBLL>();

            Assert.IsTrue(actial.GetType() == typeof(CustomerBLL));
        }

        [TestMethod]
        public void GenericCreateInstance_ExecutingAssembly_CustomerBLLProperty()
        {
            container.AddAssembly(Assembly.GetExecutingAssembly());

            var actial = container.CreateInstance<CustomerBLLProperty>();

            Assert.IsTrue(actial.GetType() == typeof(CustomerBLLProperty));
        }

        [TestMethod]
        public void GenericCreateInstance_MainSetInjection_CustomerBll()
        {
            container.AddType(typeof(CustomerBLL));
            container.AddType(typeof(Logger));
            container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

            var customerBll = container.CreateInstance<CustomerBLL>();

            Assert.IsTrue(customerBll.GetType() == typeof(CustomerBLL));
        }

        [TestMethod]
        public void GenericCreateInstance_ExecutingAssembly_CustomerBll()
        {
            container.AddType(typeof(CustomerBLL));
            container.AddType(typeof(Logger));
            container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

            var customerBll = container.CreateInstance<CustomerBLL>();

            Assert.IsTrue(customerBll.GetType() == typeof(CustomerBLL));
        }

        [TestMethod]
        public void GenericCreateInstance_ExecutingAssembly_CustomerBLLPropertyHasInjections()
        {
            container.AddAssembly(Assembly.GetExecutingAssembly());

            var CustomerBLLProperty = container.CreateInstance<CustomerBLLProperty>();

            Assert.IsTrue(CustomerBLLProperty.GetType() == typeof(CustomerBLLProperty));
            Assert.IsTrue(CustomerBLLProperty.CustomerDAL.GetType() == typeof(CustomerDAL));
            Assert.IsTrue(CustomerBLLProperty.logger.GetType() == typeof(Logger));
        }

        [TestMethod]
        public void GenericsCreateInstance_NoAttributeClass_NoInDLLException()
        {
            container.AddAssembly(Assembly.GetExecutingAssembly());

            Action actualException = () => container.CreateInstance<NoAttributeClass>();

            Assert.ThrowsException<NoAttributeException>(actualException);
        }
    }
}
