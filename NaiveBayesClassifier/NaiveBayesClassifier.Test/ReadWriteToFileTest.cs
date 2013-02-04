using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NaiveBayesClassifier.Library;
using System.IO;

namespace NaiveBayesClassifier.Test
{
    [TestClass]
    public class ReadWriteToFileTest
    {
        private Storage _storage;
        private const string _filename = "test.txt";

        public ReadWriteToFileTest()
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

            _storage = trainer.Storage;
        }

        [TestMethod]
        public void Write_To_File()
        {            
            var writer = new StorageWriter();
            writer.Write(_storage, _filename);

            Assert.IsTrue(File.Exists(_filename));
        }
    }
}
