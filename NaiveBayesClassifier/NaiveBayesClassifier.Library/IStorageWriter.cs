using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayesClassifier.Library
{
    public interface IStorageWriter
    {
        void Write(Storage storage, string fileSpecificPath, string fileWordsPath);
    }
}
