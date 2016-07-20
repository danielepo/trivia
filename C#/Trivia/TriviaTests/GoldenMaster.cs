using System;
using System.IO;
using NUnit.Framework;
using Trivia;
namespace TriviaTests
{
    [TestFixture]
    public class GoldenMaster
    {
        [Test]
        public void TestMethod1()
        {
            // let's try to log all the output from a sigle game
            
            var file = File.CreateText("gold.txt");

            Console.SetOut(file);
            string[] args = new string[1];
            for (int i = 0; i < 100; i++)
            {
                args[0] = i.ToString();
                GameRunner.Main(args);
            }

            file.Flush();
            file.Close();
        }

    }
}