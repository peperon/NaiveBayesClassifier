using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayesClassifier.Library
{
    public class StorageReader : IStorageReader
    {
        public Storage Read(string fileSpecificPath, string fileWordsPath)
        {
            Storage storage = null;
            List<int> bits = null;

            var fileInfo = new FileInfo(fileSpecificPath);
            using(var fileStream = new FileStream(fileSpecificPath, FileMode.Open))
            {
                var bytes = new byte[fileInfo.Length];
                fileStream.Read(bytes, 0, (int)fileInfo.Length);
                bits = bytes.SelectMany(GetBits).ToList();
            }

            if (bits == null || bits.Count == 0)
                return null;

            storage = new Storage();

            using (var stream = new StreamReader(fileWordsPath))
            {
                for (int i = 0; i < bits.Count && !stream.EndOfStream; ++i)
                {
                    var line = stream.ReadLine();
                    var pair = line.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                    var category = bits[i] == 1 ? "spam" : "ham";
                    if (!storage.Words.ContainsKey(pair[0]))
                        storage.Words.Add(pair[0], new Dictionary<string, int> { { category, int.Parse(pair[1]) } });
                    else
                        storage.Words[pair[0]].Add(category, int.Parse(pair[1]));
                }
            }

            return storage;
        }

        private IEnumerable<int> GetBits(byte b)
        {
            for (int i = 0; i < 8; i++)
            {
                yield return (b & 0x80) != 0 ? 1 : 0;
                b *= 2;
            }
        }
    }
}
