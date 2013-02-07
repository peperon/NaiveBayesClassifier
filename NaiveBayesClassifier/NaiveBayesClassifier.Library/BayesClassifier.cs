using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NaiveBayesClassifier.Library
{
    public class BayesClassifier : IClassifier
    {
        private StopWordsSet _stopWordSet;

        public BayesClassifier(Storage storage)
        {
            _stopWordSet = new StopWordsSet();
            Storage = storage;
        }

        public string Classify(string text)
        {
            var spamProbability = 1.0;
            var hamProbability = 1.0;

            var removedPunctuationText = Regex.Replace(text, @"[^\w\s]", " ");
            var words = removedPunctuationText.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(word => !_stopWordSet.Contain(word));

            foreach (var word in words)
            {
                var spamCounts = 0.0;
                var hamCount = 0.0;
                var spamIndex = 0.0;

                if (Storage.Words.ContainsKey(word) && Storage.Words[word].ContainsKey("spam"))
                    spamCounts = Storage.Words[word]["spam"];
                if (Storage.Words.ContainsKey(word) && Storage.Words[word].ContainsKey("ham"))
                    hamCount = Storage.Words[word]["ham"];
                if (spamCounts != 0.0)
                    spamIndex = (1.0 * spamCounts) / (1.0 * (spamCounts + hamCount));
                else if (hamCount == 0.0)
                    spamIndex = 0.6;

                spamProbability *= spamIndex != 0.0 ? spamIndex : 1.0;
                hamProbability *= spamIndex != 1.0 ? (1.0 - spamIndex) : 1.0;
            }

            if (spamProbability >= hamProbability)
                return "spam";
            else
                return "ham";
        }

        public Storage Storage
        {
            set;
            private get;
        }

    }
}
