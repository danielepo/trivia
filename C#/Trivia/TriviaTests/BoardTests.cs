using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using NUnit.Framework;
using Trivia;
using UglyTrivia;
using System.Linq;

namespace TriviaTests
{
    [TestFixture]
    public class BoardTests
    {
        private class GameForTests : Game
        {
            public LinkedList<string> PopQuestions { get { return popQuestions; } }
            public LinkedList<string> RockQuestions { get { return rockQuestions; } }
            public LinkedList<string> ScienceQuestions { get { return scienceQuestions; } }
            public LinkedList<string> SportsQuestions { get { return sportsQuestions; } }

        }
        [Test]

        public void BoardInizializzedCorrectly()
        {
            var game = new GameForTests();
            Assert.AreEqual(50, game.PopQuestions.Count);
            Assert.AreEqual(50, game.RockQuestions.Count);
            Assert.AreEqual(50, game.ScienceQuestions.Count);
            Assert.AreEqual(50, game.SportsQuestions.Count);

            for (int i = 0; i < 50; i++)
            {
                Assert.AreEqual("Pop Question " + i, game.PopQuestions.First());
                Assert.AreEqual("Rock Question " + i, game.RockQuestions.First());
                Assert.AreEqual("Science Question " + i, game.ScienceQuestions.First());
                Assert.AreEqual("Sports Question " + i, game.SportsQuestions.First());
                game.PopQuestions.RemoveFirst();
                game.RockQuestions.RemoveFirst();
                game.ScienceQuestions.RemoveFirst();
                game.SportsQuestions.RemoveFirst();
            }
        }
    }

}