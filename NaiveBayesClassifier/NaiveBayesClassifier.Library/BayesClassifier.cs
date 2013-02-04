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
            var maxProb = 0.0;
            var bestCategory = "";
            
            var scores = CategoryScores(text);
            foreach (var score in scores)
            {
                if (score.Value > maxProb)
                {
                    maxProb = score.Value;
                    bestCategory = score.Key;
                }
            }

            if (bestCategory == "")
                return null;

            foreach (var score in scores)
            {
                if (score.Key == bestCategory)
                    continue;
                if (score.Value * 1.0 > maxProb)
                    return null;
            }

            return bestCategory;
        }

        public Storage Storage
        {
            set;
            private get;
        }

        private Dictionary<string, double> CategoryScores(string text)
        {
            var result = new Dictionary<string, double>();
            Storage.Categories.Keys.ToList().ForEach(category => result.Add(category, TextProbability(category, text)));
            return result;
        }

        private double TextProbability(string category, string text)
        {
            var categoryProbability = (double)Storage.Categories[category] / (double)Storage.Categories.Values.Sum();
            var documentProbability = DocumentProbability(category, text);

            return categoryProbability * documentProbability;
        }

        private double DocumentProbability(string category, string text)
        {
            var removedPunctuationText = Regex.Replace(text, @"[^\w\s]", " ");
            var words = removedPunctuationText.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(word => !_stopWordSet.Contain(word));
            var docProbability = words.ToList().Select(word => WordWeightedAverage(category, word))
                .Aggregate(1.0, (result, next) => result *= next);
            return docProbability;
        }

        private double WordWeightedAverage(string category, string word)
        {
            var wordProbabolity = WordProbability(category, word);
            double totalWordCount = 0.0;
            if (Storage.Words.ContainsKey(word))
                totalWordCount = Storage.Words[word].Aggregate(0, (wordCount, dict) => wordCount += dict.Value);


            return (0.1 + totalWordCount * wordProbabolity) / (totalWordCount + 1);
        }

        private double WordProbability(string category, string word)
        {
            double totalWordsInCategory = 0.0;
            if (Storage.Categories.ContainsKey(category))
            {
                foreach (var wordObj in Storage.Words)
                    if(wordObj.Value.ContainsKey(category))
                        totalWordsInCategory += wordObj.Value[category];
            }

            if (totalWordsInCategory == 0.0)
                return totalWordsInCategory;
            else
            {
                double wordCount = 0.0;
                if (Storage.Words.ContainsKey(word) && Storage.Words[word].ContainsKey(category))
                    wordCount = Storage.Words[word][category];
                return wordCount / totalWordsInCategory;
            }
        }
    }
}
