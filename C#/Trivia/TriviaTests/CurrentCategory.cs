using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TriviaTests
{
    [TestFixture]
    public class CurrentCategory
    {
        private class GameForTest : UglyTrivia.Game
        {
            public string PublicCurrentCategory()
            {
                return CurrentCategory();
            }

            public int Place
            {
                set { Places[0] = value; }
            }
        }

        [TestCase(0)]
        [TestCase(4)]
        [TestCase(8)]
        public void CategoryPop(int place)
        {
            // we need to set differnt values for places[currentPlayer]
            var game = new GameForTest {Place = place};

            var currentCategory = game.PublicCurrentCategory();
            // I expect it to be

            Assert.AreEqual("Pop", currentCategory);
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(9)]
        public void CategoryScience(int place)
        {
            var game = new GameForTest {Place = place};
            var currentCategory = game.PublicCurrentCategory();
            Assert.AreEqual("Science", currentCategory);
        }

        [TestCase(2)]
        [TestCase(6)]
        [TestCase(10)]
        public void CategorySports(int place)
        {
            var game = new GameForTest {Place = place};
            var currentCategory = game.PublicCurrentCategory();
            Assert.AreEqual("Sports", currentCategory);
        }

        [TestCase(3)]
        [TestCase(7)]
        [TestCase(11)]
        public void CategoryRock(int place)
        {
            var game = new GameForTest {Place = place};
            var currentCategory = game.PublicCurrentCategory();
            Assert.AreEqual("Rock", currentCategory);
        }
    }
}