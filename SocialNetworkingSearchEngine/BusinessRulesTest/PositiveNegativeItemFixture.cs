using System.Collections.Generic;
using BusinessRules;
using NUnit.Framework;
using SearchEnginesBase.Entities;
using SharpTestsEx;

namespace BusinessRulesTest
{
    [TestFixture]
    //[Category("DataAcces")]
    public class SentimentEvaluatorFixture
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

        private List<string> negativeWords = new List<string>() { "Conchuda", "concha", "boludo", "puto" };
        private List<string> positiveWords = new List<string>() { "idolo", "exito", "amo" };
        private List<string> ignoreWords = new List<string>() { ".", "," };

        [Test]
        public void WhenContainConchaDeTuMadreShloudByNegativeTest()
        {
            //arrange
            var item = new SocialNetworkingItem() {Content = "andate a la concha de tu madre"};
            var sentimentValuator = GetSentimentValuator();
            //act
            sentimentValuator.ProcessItem(item);

            //assert
            item.Sentiment.Should().Be.EqualTo(SentimentValuator.SentimentNegative);
        }

        [Test]
        public void WhenContainIdoloShloudByPositiveTest()
        {
            //arrange
            var item = new SocialNetworkingItem() { Content = "picante pereyra, sos mi idolo." };
            var sentimentValuator = GetSentimentValuator();
            //act
            sentimentValuator.ProcessItem(item);

            //assert
            item.Sentiment.Should().Be.EqualTo(SentimentValuator.SentimentPositive);
        }

        [Test]
        public void WhenContainComputoShloudByNeutralTest()
        {
            //arrange
            var item = new SocialNetworkingItem() { Content = "El computo esta mal hecho." };
            var sentimentValuator = GetSentimentValuator();
            //act
            sentimentValuator.ProcessItem(item);

            //assert
            item.Sentiment.Should().Be.EqualTo(SentimentValuator.SentimentNeutral);
        }

        [Test]
        public void WhenContainPutoAndIdoloShloudByNeutralTest()
        {
            //arrange
            var item = new SocialNetworkingItem() { Content = "El puto es mi idolo" };
            var sentimentValuator = GetSentimentValuator();
            //act
            sentimentValuator.ProcessItem(item);

            //assert
            item.Sentiment.Should().Be.EqualTo(SentimentValuator.SentimentNeutral);
        }


        [Test]
        public void WhenContainPutoMasPuntoShouldByNegativeTest()
        {
            //arrange
            var item = new SocialNetworkingItem() { Content = "El puto." };
            var sentimentValuator = GetSentimentValuator();
            //act
            sentimentValuator.ProcessItem(item);

            //assert
            item.Sentiment.Should().Be.EqualTo(SentimentValuator.SentimentNegative);
        }


        [Test]
        public void ProcessItemShouldIncrementCountsTest()
        {
            //arrange
            var item1 = new SocialNetworkingItem() { Content = "El puto." };
            var item2 = new SocialNetworkingItem() { Content = "El idolo." };
            var item3 = new SocialNetworkingItem() { Content = "asd asd as d" };
            var sentimentValuator = GetSentimentValuator();
            //act
            sentimentValuator.ProcessItem(item1);
            sentimentValuator.ProcessItem(item2);
            sentimentValuator.ProcessItem(item3);
            //assert
            sentimentValuator.NegativeCount.Should().Be.EqualTo(1);
            sentimentValuator.PositiveCount.Should().Be.EqualTo(1);
            sentimentValuator.NeutralCount.Should().Be.EqualTo(1);
        }

        [Test]
        public void ResetCountShouldSetCountsToCeroTest()
        {
            //arrange
            var item1 = new SocialNetworkingItem() { Content = "El puto." };
            var item2 = new SocialNetworkingItem() { Content = "El idolo." };
            var item3 = new SocialNetworkingItem() { Content = "asd asd as d" };
            var sentimentValuator = GetSentimentValuator();
            sentimentValuator.ProcessItem(item1);
            sentimentValuator.ProcessItem(item2);
            sentimentValuator.ProcessItem(item3);
            //act
            sentimentValuator.ResetCounters();
            //assert
            sentimentValuator.NegativeCount.Should().Be.EqualTo(0);
            sentimentValuator.PositiveCount.Should().Be.EqualTo(0);
            sentimentValuator.NeutralCount.Should().Be.EqualTo(0);
        }


        private SentimentValuator GetSentimentValuator()
        {
            SentimentValuator sentimentValuator = new SentimentValuator();
            sentimentValuator.NegativeWords = negativeWords;
            sentimentValuator.PositiveWords = positiveWords;
            sentimentValuator.IgnoreChars = ignoreWords;
            return sentimentValuator;
        }
    }
}