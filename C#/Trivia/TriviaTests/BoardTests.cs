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
        private class CardDeckForTests : CardDeck
        {
            public LinkedList<string> PopQuestions { get { return popQuestions; } }
            public LinkedList<string> RockQuestions { get { return rockQuestions; } }
            public LinkedList<string> ScienceQuestions { get { return scienceQuestions; } }
            public LinkedList<string> SportsQuestions { get { return sportsQuestions; } }


            public string AskQuestion(string category)
            {
                return Question(category);
            }

        }
        private class GameForTests : Game
        {
            
            public void AskQuestion()
            {
                askQuestion();
            }

            protected override string currentCategory()
            {
                return CategoryStub;
            }

            public string CategoryStub { get; set; }

            public GameForTests() : base(new CardDeck())
            {
            }
        }
        [Test]

        public void DeckInizializzedCorrectly()
        {
            var game = new CardDeckForTests();
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
            var game = new CardDeckForTests();
            Assert.AreEqual(50, game.PopQuestions.Count);
            Assert.AreEqual(50, game.RockQuestions.Count);
            Assert.AreEqual(50, game.ScienceQuestions.Count);
            Assert.AreEqual(50, game.SportsQuestions.Count);

            
            Assert.AreEqual("Rock Question 0", game.AskQuestion("Rock"));
            Assert.AreEqual(50, game.PopQuestions.Count);
            Assert.AreEqual(49, game.RockQuestions.Count);
            Assert.AreEqual(50, game.ScienceQuestions.Count);
            Assert.AreEqual(50, game.SportsQuestions.Count);

            Assert.AreEqual("Pop Question 0", game.AskQuestion("Pop"));
            Assert.AreEqual(49, game.PopQuestions.Count);
            Assert.AreEqual(49, game.RockQuestions.Count);
            Assert.AreEqual(50, game.ScienceQuestions.Count);
            Assert.AreEqual(50, game.SportsQuestions.Count);
            
            Assert.AreEqual("Science Question 0", game.AskQuestion("Science"));
            Assert.AreEqual(49, game.PopQuestions.Count);
            Assert.AreEqual(49, game.RockQuestions.Count);
            Assert.AreEqual(49, game.ScienceQuestions.Count);
            Assert.AreEqual(50, game.SportsQuestions.Count);

            Assert.AreEqual("Sports Question 0", game.AskQuestion("Sports"));
            Assert.AreEqual(49, game.PopQuestions.Count);
            Assert.AreEqual(49, game.RockQuestions.Count);
            Assert.AreEqual(49, game.ScienceQuestions.Count);
            Assert.AreEqual(49, game.SportsQuestions.Count);
            
        }


        [Test]
        public void GameAsksQuestionForCategory()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            var reader = new StreamReader(stream);
            Console.SetOut(writer);

            var game = new GameForTests();
            game.CategoryStub = "Rock";
            game.AskQuestion();
            game.CategoryStub = "Pop";
            game.AskQuestion();
            game.CategoryStub = "Science";
            game.AskQuestion();
            game.CategoryStub = "Sports";
            game.AskQuestion();

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