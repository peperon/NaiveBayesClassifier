using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NaiveBayesClassifier.Lib;

namespace NaiveBayesClassifier.Test
{
    [TestClass]
    public class TrainingTest
    {        
        [TestMethod]
        public void Can_Train()
        {
            var spamText = "Logitech Cordless Mouseman Optical - SAVE 19% "
                + "buy.com price: $56.95 List price: $69.95 "
                + "Everything you want in a mouse: cordless, precise, and "
                + "comfortable. ";

            var trainer = new Trainer();
            trainer.Train(spamText, "spam");

            Assert.AreEqual(2, trainer.Storage.Words["price:"]["spam"]);
            Assert.AreEqual(1, trainer.Storage.Categories["spam"]);
        }

        [TestMethod]
        public void Dont_Count_Stop_Words()
        {
            var spamText = "Logitech Cordless Mouseman Optical - SAVE 19% "
                + "buy.com price: $56.95 List price: $69.95 "
                + "Everything you want in a mouse: cordless, precise, and "
                + "comfortable. ";

            var trainer = new Trainer();
            trainer.Train(spamText, "spam");

            Assert.IsFalse(trainer.Storage.Words.ContainsKey("and"));
            Assert.IsFalse(trainer.Storage.Words.ContainsKey("you"));
            Assert.IsFalse(trainer.Storage.Words.ContainsKey("in"));
        }
    }
}