﻿using System.Collections.Generic;
using BusinessRules;
using Core.Domain;
using DataAccess;
using DataAccess.DAO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessRulesTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class SearchEngineManagerTest
    {
        public SearchEngineManagerTest()
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
        public void ConfigureAddinsTest()
        {
            SearchEngineManager.ConfigureAddins();
            var searchEngineManager = new SearchEngineManager();
            var result =searchEngineManager.Search(string.Empty, new List<string>() {"SearchEngineMock"});
            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public void CreateDataBase()
        {
            NHSessionManager.CreateDB = true;
            NHSessionManager.Instance.BeginTransaction();
            NHSessionManager.Instance.CommitTransaction();
        }

        [TestMethod]
        public void CreateTags()
        {
            NHSessionManager.Instance.BeginTransaction();
            var tagRepository = new TagRepository();
            tagRepository.SaveOrUpdate(new Tag() {Name = "tag1"});
            tagRepository.SaveOrUpdate(new Tag() { Name = "tag2" });
            tagRepository.SaveOrUpdate(new Tag() { Name = "tag3" });
            tagRepository.SaveOrUpdate(new Tag() { Name = "tag4" });
            NHSessionManager.Instance.CommitTransaction();
        }

        [TestMethod]
        public void CreateWords()
        {
            NHSessionManager.Instance.BeginTransaction();
            var wordRepository = new WordRepository();

            wordRepository.SaveOrUpdate(new Word() { Name = "Conchuda", Sentiment = "Negativo"});
            wordRepository.SaveOrUpdate(new Word() { Name = "concha", Sentiment = "Negativo"});
            wordRepository.SaveOrUpdate(new Word() { Name = "boludo", Sentiment = "Negativo"});
            wordRepository.SaveOrUpdate(new Word() { Name = "puto", Sentiment = "Negativo"});

            wordRepository.SaveOrUpdate(new Word() { Name = "idolo", Sentiment = "Positivo"});
            wordRepository.SaveOrUpdate(new Word() { Name = "exito", Sentiment = "Positivo"});
            wordRepository.SaveOrUpdate(new Word() { Name = "amo", Sentiment = "Positivo"});
            wordRepository.SaveOrUpdate(new Word() { Name = "genio", Sentiment = "Positivo"});

            NHSessionManager.Instance.CommitTransaction();
        }
    }
}
