using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchEngineMock;
using SearchEngineMock.DAO;
using SearchEnginesBase.Entities;

namespace SearchEnginesTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class SearchEngineMockTest
    {
        public SearchEngineMockTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CreateDataBase()
        {
            NHSessionManager.CreateDB = true;
            NHSessionManager.Instance.BeginTransaction();
            NHSessionManager.Instance.CommitTransaction();
        }

        [TestMethod]
        public void CreateItems()
        {
            NHSessionManager.Instance.BeginTransaction();
            var daoSocialNetworkingItem = new DaoSocialNetworkingItem();
            var r = new Random();
            for (int i = 0; i < 100; i++)
            {
                var entity = new SocialNetworkingItem {Content =  r.NextDouble() + " Something " + r.NextDouble(), Date = DateTime.Now};
                daoSocialNetworkingItem.SaveOrUpdate(entity);
            }
            NHSessionManager.Instance.CommitTransaction();
        }
    }
}
