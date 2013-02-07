using NaiveBayesClassifier.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayesClassifier
{
    class Program
    {
        static void Main(string[] args)
        {
            //SpamFilter.Train("spam.txt", "ham.txt");
            for (int i = 1; i <= 20; ++i)
            {
                var spamFile = string.Format("spam\\spam_test{0}.txt", i);
                var hamFile = string.Format("ham\\ham_test{0}.txt", i);
                
                Console.WriteLine(spamFile + " is spam " + SpamFilter.IsSpam(spamFile));
                Console.WriteLine(hamFile + " is ham " + !SpamFilter.IsSpam(hamFile));
            }
        }
    }
}
