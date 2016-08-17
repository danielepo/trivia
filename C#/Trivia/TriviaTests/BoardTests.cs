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
    public class DeckTests
    {
        private class GameForTests : Game
        {
            public LinkedList<string> PopQuestions { get { return popQuestions; } }
            public LinkedList<string> RockQuestions { get { return rockQuestions; } }
            public LinkedList<string> ScienceQuestions { get { return scienceQuestions; } }
            public LinkedList<string> SportsQuestions { get { return sportsQuestions; } }

            protected override string currentCategory()
            {
                return CategoryStub;
            }

            public void AskQuestion()
            {
                askQuestion();
            }

            public string CategoryStub { get; set; }
        }
        [Test]

        public void DeckInizializzedCorrectly()
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
        [Test]

        public void DeckReturnsQuestionForCategory()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            var reader = new StreamReader(stream);
            Console.SetOut(writer);

            var game = new GameForTests();
            Assert.AreEqual(50, game.PopQuestions.Count);
            Assert.AreEqual(50, game.RockQuestions.Count);
            Assert.AreEqual(50, game.ScienceQuestions.Count);
            Assert.AreEqual(50, game.SportsQuestions.Count);

            game.CategoryStub = "Rock";
            game.AskQuestion();
            Assert.AreEqual(50, game.PopQuestions.Count);
            Assert.AreEqual(49, game.RockQuestions.Count);
            Assert.AreEqual(50, game.ScienceQuestions.Count);
            Assert.AreEqual(50, game.SportsQuestions.Count);

            game.CategoryStub = "Pop";
            game.AskQuestion();
            Assert.AreEqual(49, game.PopQuestions.Count);
            Assert.AreEqual(49, game.RockQuestions.Count);
            Assert.AreEqual(50, game.ScienceQuestions.Count);
            Assert.AreEqual(50, game.SportsQuestions.Count);
            
            game.CategoryStub = "Science";
            game.AskQuestion();
            Assert.AreEqual(49, game.PopQuestions.Count);
            Assert.AreEqual(49, game.RockQuestions.Count);
            Assert.AreEqual(49, game.ScienceQuestions.Count);
            Assert.AreEqual(50, game.SportsQuestions.Count);
            
            game.CategoryStub = "Sports";
            game.AskQuestion();
            Assert.AreEqual(49, game.PopQuestions.Count);
            Assert.AreEqual(49, game.RockQuestions.Count);
            Assert.AreEqual(49, game.ScienceQuestions.Count);
            Assert.AreEqual(49, game.SportsQuestions.Count);
            
            
            writer.Flush();
            stream.Position = 0;
            Assert.AreEqual("Rock Question 0", reader.ReadLine());
            Assert.AreEqual("Pop Question 0", reader.ReadLine());
            Assert.AreEqual("Science Question 0", reader.ReadLine());
            Assert.AreEqual("Sports Question 0", reader.ReadLine());

            stream.Close();
        }
    }

}