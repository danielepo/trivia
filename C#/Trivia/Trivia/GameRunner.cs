using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UglyTrivia;

namespace Trivia
{
    public class GameRunner
    {

        private static bool notAWinner;
        // ok in the second iteration we have 20 minutes to build a golden master
        public static void Main(String[] args)
        {
            var seed = int.Parse(args[1]);
            var writeMaster = bool.Parse(args[0]);
            if (writeMaster)
            {
                Console.WriteLine("**********");
                Console.WriteLine(seed);
                Console.WriteLine("----------");
            }

            Game aGame = new Game(new CardDeck());

            aGame.add("Chet");
            aGame.add("Pat");
            aGame.add("Sue");
            Random rand = new Random(seed);

            do
            {

                aGame.roll(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                {
                    notAWinner = aGame.wrongAnswer();
                }
                else
                {
                    notAWinner = aGame.wasCorrectlyAnswered();
                }



            } while (notAWinner);
            if (writeMaster)
                Console.WriteLine("__________");

        }


    }

}

