using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayesClassifier.Library
{
    public class Storage
    {
        public Storage()
        {
            Categories = new Dictionary<string, int>();
            Words = new Dictionary<string, Dictionary<string, int>>();
        }

        public Dictionary<string, int> Categories { get; set; }

        public Dictionary<string, Dictionary<string, int>> Words { get; set; }

        public override string ToString()
        {
            var strBuilder = new StringBuilder();

            foreach (var category in Categories)
                strBuilder.AppendLine(String.Format("[{0}, {1}]", category.Key, category.Value));
            strBuilder.AppendLine("END_CATEGORIES");
            foreach (var wordEntry in Words)
                foreach (var wordCategories in wordEntry.Value)
                    strBuilder.AppendLine(String.Format("[{0}, {1}, {2}]", wordEntry.Key, wordCategories.Key, wordCategories.Value));
            
            return strBuilder.ToString();
        }
    }
}
