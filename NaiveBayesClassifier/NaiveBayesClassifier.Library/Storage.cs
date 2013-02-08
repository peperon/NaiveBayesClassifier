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
            Words = new Dictionary<string, Dictionary<string, int>>();
        }

        public Dictionary<string, Dictionary<string, int>> Words { get; set; }

        public int GetAllSpamCount()
        {
            return (from word in Words
                   where word.Value.ContainsKey("spam")
                   select word.Value["spam"]).Sum();
        }

        public int AllCount()
        {
            return (from word in Words
                    select word.Value.Values.Sum()).Sum();
        }
    }
}
