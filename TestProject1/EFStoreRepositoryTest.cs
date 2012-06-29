using Store.Domain.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Store.Domain.Entities;

namespace TestProject1
{
    
    
    /// <summary>
    ///This is a test class for EFStoreRepositoryTest and is intended
    ///to contain all EFStoreRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EFStoreRepositoryTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for RegisterUser
        ///</summary>
        [TestMethod()]
        public void RegisterUserTest()
        {
            EFStoreRepository target = new EFStoreRepository(); // TODO: Initialize to an appropriate value
            User user = new User { Email = "s@asa.com", Password = "aaa" };
            Customer customer = new Customer { CustomerID ="r1144", CompanyName = "aa", ContactName = "aa", ContactTitle = "aa", Address = "aa", City = "aa", Country = "aa", Fax = "aa", Phone = "aa", PostalCode = "aa", Region = "aa" };
            target.RegisterUserWithCustomer(user, customer);

        }
    }
}
