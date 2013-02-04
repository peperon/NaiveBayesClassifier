using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayesClassifier.Library
{
    public interface IClassifier
    {
        Storage Storage { set; }

        string Classify(string text);
    }
}
