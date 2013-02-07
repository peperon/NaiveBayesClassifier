using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayesClassifier.Library
{
    public class StorageWriter : IStorageWriter
    {
        public void Write(Storage storage, string fileSpecificPath, string fileWordsPath)
        {
            var bitmap = new List<byte>();
            
            var words = GenerateWordsStringAndBitmap(bitmap, storage);
            if (words == null || words == "")
                return;
            var bytemap = new List<byte>();

            while (bitmap.Count > 0)
            {
                var oneByte = bitmap.Take(8).ToList();
                bitmap = bitmap.Skip(8).ToList();
                bytemap.Add(oneByte.Aggregate((byte)0, (result, bit) => (byte)((result << 1) | bit)));
            }

            using (var streamWriter = new StreamWriter(fileWordsPath))
            {
                streamWriter.Write(words);
            }
            using (var fileStream = new FileStream(fileSpecificPath, FileMode.OpenOrCreate))
            {
                fileStream.Write(bytemap.ToArray(), 0, bytemap.Count);
                fileStream.Close();
            }
        }

        private string GenerateWordsStringAndBitmap(List<byte> bitmap, Storage storage)
        {
            var strBuilder = new StringBuilder();
            
            foreach (var word in storage.Words)
            {
                if (word.Value.ContainsKey("spam"))
                {
                    strBuilder.AppendLine(word.Key + ", " + word.Value["spam"]);
                    bitmap.Add(1);
                }
                if (word.Value.ContainsKey("ham"))
                {
                    strBuilder.AppendLine(word.Key + ", " + word.Value["ham"]);
                    bitmap.Add(0);
                }
            }
            return strBuilder.ToString();
        }
    }
}
