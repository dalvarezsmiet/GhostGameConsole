using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GhostGame;

namespace TestGhostGameConsole
{
    [TestClass]
    public class TestHumanWebPlayer
    {
        [TestMethod]
        public void HumanWebPlayerPlayReturnsNotNullTest()
        {
            HumanWebPlayer human = new HumanWebPlayer();

            char letter = human.Play("game");

            Assert.IsNotNull(letter);
            Assert.AreEqual('g', letter);
        }

        [TestMethod]
        public void HumanWebPlayerPlayReturnsFirstCharOfStringTest()
        {
            HumanWebPlayer human = new HumanWebPlayer();

            char letter = human.Play("gamma");

            Assert.AreEqual('g', letter);
        }

        [TestMethod]
        public void HumanWebPlayerPlayReturnsDefaultXifStringIsEmptyTest()
        {
            HumanWebPlayer human = new HumanWebPlayer();

            char letter = human.Play("");

            Assert.AreEqual('x', letter);
        }
    }
}
