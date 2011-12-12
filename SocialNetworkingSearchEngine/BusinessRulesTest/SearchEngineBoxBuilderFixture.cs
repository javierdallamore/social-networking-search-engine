using System.Collections.Generic;
using Core.Domain;
using NUnit.Framework;
using SearchEnginesBase.Entities;
using SharpTestsEx;
using SocialNetWorkingSearchEngine;
using SocialNetWorkingSearchEngine.Models;

namespace BusinessRulesTest
{
    [TestFixture]
    //[Category("DataAcces")]
    public class SearchEngineBoxBuilderFixture
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
        public void Process1ItemShouldIncrementCounterTest()
        {
            //arrange
            SearchEngineBoxBuilder builder = new SearchEngineBoxBuilder();
            var item1 = new Post(){SocialNetworkName = "Twitter"};
            //act
            builder.ProcessItem(item1);

            //assert
            builder.Engines.Count.Should().Be.EqualTo(1);
            builder.Engines[0].Name.Should().Be.EqualTo(item1.SocialNetworkName);
            builder.Engines[0].Counter.Should().Be.EqualTo(1);
        }

        [Test]
        public void Process2ItemShouldSetCounterTo2Test()
        {
            //arrange
            SearchEngineBoxBuilder builder = new SearchEngineBoxBuilder();
            var item1 = new Post() {SocialNetworkName = "Twitter"};
            var item2 = new Post() { SocialNetworkName = "Twitter" };
            //act
            builder.ProcessItem(item1);
            builder.ProcessItem(item2);

            //assert
            builder.Engines.Count.Should().Be.EqualTo(1);
            builder.Engines[0].Counter.Should().Be.EqualTo(2);
        }

        [Test]
        public void Process2ItemFromFrom2EnginesShouldSetCounterTo2Test()
        {
            //arrange
            SearchEngineBoxBuilder builder = new SearchEngineBoxBuilder();
            var item1 = new Post() { SocialNetworkName = "Twitter" };
            var item2 = new Post() { SocialNetworkName = "Twitter" };
            var item3 = new Post() { SocialNetworkName = "Facebook" };
            var item4 = new Post() { SocialNetworkName = "Facebook" };
            //act
            builder.ProcessItem(item1);
            builder.ProcessItem(item2);
            builder.ProcessItem(item3);
            builder.ProcessItem(item4);

            //assert
            builder.Engines.Count.Should().Be.EqualTo(2);
            builder.Engines[0].Counter.Should().Be.EqualTo(2);
            builder.Engines[1].Counter.Should().Be.EqualTo(2);
        }

        [Test]
        public void buildBoxWith2SearchEnginecon3y2ItemsTest()
        {
            //arrange
            SearchEngineBoxBuilder builder = new SearchEngineBoxBuilder();
            var model = new SearchResultModel();
            var item1 = new Post() { SocialNetworkName = "Twitter" };
            var item2 = new Post() { SocialNetworkName = "Twitter" };
            var item3 = new Post() { SocialNetworkName = "Twitter" };
            var item4 = new Post() { SocialNetworkName = "Facebook" };
            var item5 = new Post() { SocialNetworkName = "Facebook" };
            model.Items.AddRange(new List<Post>() { item1, item2, item3, item4, item5 });
            //act
            builder.BuildBox(model);

            //assert
            model.StatBoxs.Count.Should().Be.EqualTo(1);
            model.StatBoxs[0].StatItems.Count.Should().Be.EqualTo(2);
            model.StatBoxs[0].StatItems[0].Title.Should().Be.EqualTo("Twitter");
            model.StatBoxs[0].StatItems[1].Title.Should().Be.EqualTo("Facebook");
            model.StatBoxs[0].StatItems[0].ValueText.Should().Be.EqualTo("60,00%");
            model.StatBoxs[0].StatItems[1].ValueText.Should().Be.EqualTo("40,00%");
        }
    }
}