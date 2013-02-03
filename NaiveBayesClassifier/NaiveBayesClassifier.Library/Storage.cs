using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayesClassifier.Library
{
    public class Storage : IStorage
    {
        public Storage()
        {
            Categories = new Dictionary<string, int>();
            Words = new Dictionary<string, Dictionary<string, int>>();
        }

        public Dictionary<string, int> Categories { get; set; }

        public Dictionary<string, Dictionary<string, int>> Words { get; set; }
    }
}
