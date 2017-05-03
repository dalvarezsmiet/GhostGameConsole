using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GhostGame;

namespace TestGhostGameConsole
{
    [TestClass]
    public class TestComputerPlayer
    {
        [TestMethod]
        public void ForcedWinReturnsNullNodeIfComputerIsToLoseTest()
        {
            ComputerPlayer computer = new ComputerPlayer();

            LetterNode node = computer.ForcedWin(new LetterNode("game"));

            Assert.IsNull(node);
        }

        [TestMethod]
        public void ForcedWinReturnsNextBestPlayNodeIfComputerIsToWinTest()
        {
            ComputerPlayer computer = new ComputerPlayer();

            LetterNode node = computer.ForcedWin(new LetterNode("gamma"));

            Assert.AreEqual('a', node.Letter());
        }

        [TestMethod]
        public void LongestWordReturnsNextBestPlayNodeIfComputerIsToWinTest()
        {
            ComputerPlayer computer = new ComputerPlayer();

            LetterNode node = computer.LongestWord(new LetterNode("aye"));

            Assert.IsTrue(node.Child('e').LeafNode);

        }

        [TestMethod]
        public void LongestWordReturnsLongestPlayNodeIfComputerIsToLoseTest()
        {
            ComputerPlayer computer = new ComputerPlayer();

            LetterNode node = computer.LongestWord(new LetterNode("game"));

            Assert.IsFalse(node.Child('m').LeafNode);

        }
    }
}
