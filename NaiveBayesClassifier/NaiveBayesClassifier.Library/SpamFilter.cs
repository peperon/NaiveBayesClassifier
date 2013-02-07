using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NaiveBayesClassifier.Library
{
    public class SpamFilter
    {
        private static readonly string WordsBin = "words.bin";
        private static readonly string WordsTxt = "words.txt";

        public static void Train(string spamFile, string hamFile)
        {
            if (!File.Exists(spamFile))
                throw new FileNotFoundException("Spam file is not found!");
            if (!File.Exists(hamFile))
                throw new FileNotFoundException("Ham file is not found!");

            var trainer = new Trainer();
            var storageWriter = new StorageWriter();

            using (var streamReader = new StreamReader(spamFile))
            {
                var text = streamReader.ReadToEnd();
                trainer.Train(text, "spam");
            }

            using (var streamReader = new StreamReader(hamFile))
            {
                var text = streamReader.ReadToEnd();
                trainer.Train(text, "ham");
            }

            storageWriter.Write(trainer.Storage, WordsBin, WordsTxt);
        }

        public static bool IsSpam(string emailFilePath)
        {
            if (!File.Exists(emailFilePath))
                throw new FileNotFoundException("Spam file is not found!");

            var reader = new StorageReader();
            string text = null;
            using (var streamReader = new StreamReader(emailFilePath))
            {
                text = streamReader.ReadToEnd();
            }

            if (text == null || text == "")
                return false;

            var storage = reader.Read(WordsBin, WordsTxt);
            var classifier = new BayesClassifier(storage);

            var result = classifier.Classify(text);

            return result == "spam";
        }
    }
}
