using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayesClassifier.Library
{
    public class StorageWriter : IStorageWriter
    {
        public void Write(Storage storage, string filePath)
        {
            using (var streamWriter = new StreamWriter(filePath))
            {
                streamWriter.Write(storage.ToString());
            }
        }
    }
}
