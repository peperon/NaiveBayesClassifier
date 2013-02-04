using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NaiveBayesClassifier.Library;

namespace NaiveBayesClassifier.Test
{
    [TestClass]
    public class ClassifierTest
    {
        private IClassifier _classifier;

        public ClassifierTest()
        {
            var trainer = new Trainer();
            trainer.Train("Dogs are awesome, cats too. I love my dog", "dog");
            trainer.Train("Cats are more preferred by software developers. I never could stand cats. I have a dog", "cat");
            trainer.Train("My dog's name is Willy. He likes to play with my wife's cat all day long. I love dogs", "dog");
            trainer.Train("Cats are difficult animals, unlike dogs, really annoying, I hate them all", "cat");
            trainer.Train("So which one should you choose? A dog, definitely.", "dog");
            trainer.Train("The favorite food for cats is bird meat, although mice are good, but birds are a delicacy", "cat");
            trainer.Train("A dog will eat anything, including birds or whatever meat", "dog");
            trainer.Train("My cat's favorite place to purr is on my keyboard", "cat");
            trainer.Train("My dog's favorite place to take a leak is the tree in front of our house", "dog");

            _classifier = new BayesClassifier(trainer.Storage);
        }

        [TestMethod]
        public void Can_Do_Basic_Classification_For_Cat_Category()
        {
            Assert.AreEqual("cat", _classifier.Classify("This test is about cats."));
            Assert.AreEqual("cat", _classifier.Classify("I hate ..."));
            Assert.AreEqual("cat", _classifier.Classify("The most annoying animal on earth."));
            Assert.AreEqual("cat", _classifier.Classify("The preferred company of software developers."));
            Assert.AreEqual("cat", _classifier.Classify("My precious, my favorite!"));
            Assert.AreEqual("cat", _classifier.Classify("Kill that bird!"));
            Assert.AreEqual("cat", _classifier.Classify("Cats or Dogs?"));
        }

        [TestMethod]
        public void Can_Do_Basic_Classification_For_Dog_Category()
        {
            Assert.AreEqual("dog", _classifier.Classify("This test is about dogs."));            
            Assert.AreEqual("dog", _classifier.Classify("What pet will I love more?"));
            Assert.AreEqual("dog", _classifier.Classify("Willy, where the heck are you?"));
            Assert.AreEqual("dog", _classifier.Classify("I like big buts and I cannot lie."));
            Assert.AreEqual("dog", _classifier.Classify("Why is the front door of our house open?"));
            Assert.AreEqual("dog", _classifier.Classify("Who ate my meat?"));            
        }
    }
}
