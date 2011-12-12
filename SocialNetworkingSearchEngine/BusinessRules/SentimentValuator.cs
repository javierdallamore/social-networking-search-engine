using System.Collections.Generic;
using System.Linq;
using SearchEnginesBase.Entities;

namespace BusinessRules
{
    public class SentimentValuator
    {
        public const string SentimentNegative = "Negativo";
        public const string SentimentPositive = "Positivo";
        public const string SentimentNeutral = "Neutro";

        public List<string> NegativeWords { get; set; }
        public List<string> PositiveWords { get; set; }
        public List<string> IgnoreChars { get; set; }

        public SentimentValuator()
        {
            NegativeWords = new List<string>();
            PositiveWords = new List<string>();
            IgnoreChars= new List<string>();
        }

        private int _negativeCount;

        public int NegativeCount
        {
            get { return _negativeCount; }
        }

        private int _neutralCount;
        public int NeutralCount
        {
            get { return _neutralCount; }
        }

        private int _positiveCount;
        public int PositiveCount
        {
            get { return _positiveCount; }
        }

        public void ProcessItem(SocialNetworkingItem item)
        {
            string content = item.Content.ToLower();
            foreach (var ignoreChar in IgnoreChars)
            {
                content = content.Replace(ignoreChar, " ");
            }
            var cantNeg = NegativeWords.Count(word => content.Contains(" " + word + " "));
            var cantPos = PositiveWords.Count(word => content.Contains(word));
            
            item.Sentiment = SentimentNeutral;
            if (cantNeg > 0)
            {
                item.Sentiment = SentimentNegative;
            }
            if (cantPos > 0)
            {
                item.Sentiment = SentimentPositive;
            }
            if (cantNeg > 0 && cantPos > 0) item.Sentiment = SentimentNeutral;

            if (item.Sentiment == SentimentNegative) _negativeCount++;
            if (item.Sentiment == SentimentPositive) _positiveCount++;
            if (item.Sentiment == SentimentNeutral) _neutralCount++;
        }

        public void ResetCounters()
        {
            _positiveCount = 0;
            _negativeCount = 0;
            _neutralCount = 0;
        }

        public void ProcessItems(List<SocialNetworkingItem> list)
        {
            foreach (var item in list)
            {
                ProcessItem(item);
            }
        }
    }
}