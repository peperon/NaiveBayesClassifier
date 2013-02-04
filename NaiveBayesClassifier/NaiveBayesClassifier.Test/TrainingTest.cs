using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NaiveBayesClassifier.Library;

namespace NaiveBayesClassifier.Test
{
    [TestClass]
    public class TrainingTest
    {        
        [TestMethod]
        public void Can_Train()
        {
            var storage = Training();

            Assert.AreEqual(2, storage.Words["price:"]["spam"]);
            Assert.AreEqual(1, storage.Categories["spam"]);
        }

        [TestMethod]
        public void Dont_Count_Stop_Words()
        {
            var storage = Training();

            Assert.IsFalse(storage.Words.ContainsKey("and"));
            Assert.IsFalse(storage.Words.ContainsKey("you"));
            Assert.IsFalse(storage.Words.ContainsKey("in"));
        }

        private Storage Training()
        {
            var spamText = "Logitech Cordless Mouseman Optical - SAVE 19% "
                + "buy.com price: $56.95 List price: $69.95 "
                + "Everything you want in a mouse: cordless, precise, and "
                + "comfortable. ";

            var trainer = new Trainer();
            trainer.Train(spamText, "spam");
            return trainer.Storage;
        }
    }
}