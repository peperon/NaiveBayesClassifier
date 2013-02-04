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
        public Storage Read(string filePath)
        {
            Storage storage = null;
            using (var streamReader = new StreamReader(filePath))
            {
                var text = streamReader.ReadToEnd();
                if(text != null || text != "")
                    storage = CreateStorage(text);
            }

            return storage;
        }

        private Storage CreateStorage(string text)
        {
            var result = new Storage();
            var isReadingCategories = true;
            var lines = text.Replace("[", "").Replace("]", "").Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                if (line == "END_CATEGORIES")
                {
                    isReadingCategories = false;
                    continue;
                }
                var splited_line = line.Split(new string[] { ", " }, StringSplitOptions.None);
                if (isReadingCategories)
                    result.Categories.Add(splited_line[0], int.Parse(splited_line[1]));
                else
                {
                    if (result.Words.ContainsKey(splited_line[0]))
                        result.Words[splited_line[0]].Add(splited_line[1], int.Parse(splited_line[2]));
                    else
                        result.Words.Add(splited_line[0], new Dictionary<string, int> { { splited_line[1], int.Parse(splited_line[2]) } });
                }
            }
            return result;
        }
    }
}
