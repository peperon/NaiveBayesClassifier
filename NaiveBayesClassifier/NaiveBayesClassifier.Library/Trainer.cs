using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NaiveBayesClassifier.Library
{
    public class Trainer : ITrainer
    {
        private StopWordsSet _stopWordsSet;

        public Trainer()
        {
            Storage = new Storage();
            _stopWordsSet = new StopWordsSet();
        }

        public Storage Storage { get; private set; }

        public void Train(string text, string category)
        {
            var removedPunctuationText = Regex.Replace(text, @"[^\w\s]", " ");
            var words = removedPunctuationText.ToLower().Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                if (!_stopWordsSet.Contain(word) && word.Length > 3)
                    IncrementWord(word.Trim(), category);
            }
        }


        private void IncrementWord(string word, string category)
        {
            if (!Storage.Words.ContainsKey(word))
                Storage.Words.Add(word, new Dictionary<string, int> { { category, 1 } });
            else if (!Storage.Words[word].ContainsKey(category))
                Storage.Words[word].Add(category, 1);
            else
                Storage.Words[word][category] += 1;
        }
    }
}
