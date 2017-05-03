using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GhostGame;

namespace TestGhostGame
{
    [TestClass]
    public class TestGhostDictionary
    {
        const string filename = @"..\..\..\wordlist.txt";

        [TestMethod]
        public void GhostDictionaryCreatesValidObjectWithSizeAndMaxLegthGreaterThanZeroTest()
        {
            GhostDictionary dictionary = new GhostDictionary(filename);

            Console.WriteLine("Created dictionary with " + dictionary.Size() + " words, the longest having " + dictionary.MaxLength() + " letters.");
            Assert.IsTrue(dictionary.Size() > 0);
            Assert.IsTrue(dictionary.MaxLength() > 0);
        }

        [TestMethod]
        public void GhostDictionaryTerminalNodeReturnsNullWithNonsenseTest()
        {
            GhostDictionary dictionary = new GhostDictionary(filename);

            Assert.IsNull(dictionary.TerminalNode("wqzx"));
        }

        [TestMethod]
        public void GhostDictionaryTerminalNodeReturnsNotNullWithRealWordTest()
        {
            GhostDictionary dictionary = new GhostDictionary(filename);

            Assert.IsNotNull(dictionary.TerminalNode("llama"));
        }

        [TestMethod]
        public void GhostDictionaryIsFullWordReturnsFalseWithNonsenseTest()
        {
            GhostDictionary dictionary = new GhostDictionary(filename);

            Assert.IsFalse(dictionary.IsFullWord("wqzx"));
        }

        [TestMethod]
        public void GhostDictionaryIsFullWordReturnsTrueWithRealWordTest()
        {
            GhostDictionary dictionary = new GhostDictionary(filename);

            Assert.IsTrue(dictionary.IsFullWord("llama"));
        }

        [TestMethod]
        public void GhostDictionaryIsWordStemReturnsFalseWithNearlyFullWordTest()
        {
            GhostDictionary dictionary = new GhostDictionary(filename);

            Assert.IsFalse(dictionary.IsWordStem("llamd"));
        }

        [TestMethod]
        public void GhostDictionaryIsWordStemReturnsFalseWithNonsenseWordTest()
        {
            GhostDictionary dictionary = new GhostDictionary(filename);

            Assert.IsFalse(dictionary.IsWordStem("jsdhbfwef"));
        }

        [TestMethod]
        public void GhostDictionaryIsWordStemReturnsTrueIncompleteWordTest()
        {
            GhostDictionary dictionary = new GhostDictionary(filename);

            Assert.IsTrue(dictionary.IsWordStem("llam"));
        }
    }
}
