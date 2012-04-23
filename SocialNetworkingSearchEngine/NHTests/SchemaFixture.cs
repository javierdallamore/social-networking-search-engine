using DataAccess;
using NUnit.Framework;
using SharpTestsEx;

namespace NHTests
{
    [TestFixture]
    //[Category("DataAcces")]
    public class SchemaFixture
    {

        #region Tests Setups

        [TestFixtureSetUp]
        public void InitFixture()
        {
        }

        [TestFixtureTearDown]
        public void DisposeFixture()
        {
        }

        [SetUp]
        public void InitTest()
        {
            /* ... */
        }

        [TearDown]
        public void DisposeTest()
        {
            /* ... */
        }

        #endregion

        [Test]
        public void GenerateSchema()
        {
            NHSessionManager.CreateDB = true;
            NHSessionManager.Instance.GetSession();
        }
    }
}