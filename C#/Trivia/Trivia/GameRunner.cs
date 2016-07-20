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
            Console.WriteLine("**********");
            Console.WriteLine(args[0]);
            Console.WriteLine("----------");

            Game aGame = new Game();

            aGame.add("Chet");
            aGame.add("Pat");
            aGame.add("Sue");
            Random rand = new Random(int.Parse(args[0]));

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
            Console.WriteLine("__________");
            
        }


    }

}

