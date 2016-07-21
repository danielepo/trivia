using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
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
            string[] args = new string[2];
            args[0] = "true";
            var rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                args[1] = i.ToString();//rand.Next().ToString();
                GameRunner.Main(args);
            }

            file.Flush();
            file.Close();
        }

        [Test]
        public void TestWithGold()
        {
            var file = File.ReadLines("gold.txt");
            bool parseSeed = false;
            MemoryStream stream;
            StreamWriter writer;
            StreamReader reader;
            stream = new MemoryStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            Console.SetOut(writer);
            string currentSeed = String.Empty;
            foreach (var line in file)
            {

                switch (line)
                {
                    case "**********":
                        parseSeed = true;
                        continue;
                    case "----------":
                        continue;
                    case "__________":
                        stream.SetLength(0);
                        continue;
                }

                if (parseSeed)
                {
                    currentSeed = line;
                    parseSeed = false;
                    GameRunner.Main(new[] { "false", currentSeed });
                    writer.Flush();
                    stream.Position = 0;
                    continue;
                }


                Assert.AreEqual(line, reader.ReadLine(), "Seed " + currentSeed);
            }
        }

        [Test]
        public void testRandomSeed()
        {
            var seed1 = 1079290332;
            var seed2 = 2051736430;
            var rand = new Random(seed1);
            List<int> mem1 = new List<int>();

            for (int i = 0; i < 1000000; i++)
            {
                mem1.Add(rand.Next());
            }
            rand = new Random(seed2);
            List<int> mem2 = new List<int>();

            for (int i = 0; i < 1000000; i++)
            {
                mem2.Add(rand.Next());
            }
            rand = new Random(seed1);

            for (int i = 0; i < 1000000; i++)
            {
                Assert.AreEqual(mem1[i], rand.Next());
            }
            rand = new Random(seed2);

            for (int i = 0; i < 1000000; i++)
            {
                Assert.AreEqual(mem2[i], rand.Next());
            }
        }
    }

}