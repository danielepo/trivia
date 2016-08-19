using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UglyTrivia
{
    public class Deck
    {
        private readonly IEnumerable<string> popQuestions;
        private readonly IEnumerable<string> scienceQuestions;
        private readonly IEnumerable<string> sportsQuestions;
        private readonly IEnumerable<string> rockQuestions;

        public Deck(IEnumerable<string> pop, IEnumerable<string> science, IEnumerable<string> sports, IEnumerable<string> rock)
        {
            popQuestions = pop;
            scienceQuestions = science;
            sportsQuestions = sports;
            rockQuestions = rock;
        }
        public Tuple<string, Deck> RockQuestion
        {
            get
            {
                string question = rockQuestions.First();
                var newDeck = new Deck(popQuestions, scienceQuestions, sportsQuestions, rockQuestions.Skip(1));
                return Tuple.Create(question, newDeck);
            }
        }
        public Tuple<string, Deck> PopQuestion
        {
            get
            {
                string question = popQuestions.First();
                var newDeck = new Deck(popQuestions.Skip(1), scienceQuestions, sportsQuestions, rockQuestions);
                return Tuple.Create(question, newDeck);
            }
        }
        public Tuple<string, Deck> ScienceQuestion
        {
            get
            {
                string question = scienceQuestions.First();
                var newDeck = new Deck(popQuestions, scienceQuestions.Skip(1), sportsQuestions, rockQuestions);
                return Tuple.Create(question, newDeck);
            }
        }

        public Tuple<string, Deck> SportsQuestion
        {
            get
            {
                string question = scienceQuestions.First();
                var newDeck = new Deck(popQuestions, scienceQuestions, sportsQuestions.Skip(1), rockQuestions);
                return Tuple.Create(question, newDeck);
            }
        }
    }
    public class Game
    {


        List<string> players = new List<string>();

        int[] places = new int[6];
        int[] purses = new int[6];

        bool[] inPenaltyBox = new bool[6];


        int currentPlayer = 0;
        bool isGettingOutOfPenaltyBox;
        private Deck deck;
        public Game()
        {
            IList<string> popQuestions = new List<string>();
            IList<string> scienceQuestions = new List<string>();
            IList<string> sportsQuestions = new List<string>();
            IList<string> rockQuestions = new List<string>();
            for (int i = 0; i < 50; i++)
            {
                popQuestions.Add("Pop Question " + i);
                scienceQuestions.Add(("Science Question " + i));
                sportsQuestions.Add(("Sports Question " + i));
                rockQuestions.Add(createRockQuestion(i));
            }
            deck = new Deck(popQuestions, scienceQuestions, sportsQuestions, rockQuestions);
        }

        public String createRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool isPlayable()
        {
            return (howManyPlayers() >= 2);
        }

        public bool add(String playerName)
        {


            players.Add(playerName);
            places[howManyPlayers()] = 0;
            purses[howManyPlayers()] = 0;
            inPenaltyBox[howManyPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int howManyPlayers()
        {
            return players.Count;
        }

        public void roll(int roll)
        {
            Console.WriteLine(players[currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);
            var cat = currentCategory();

            if (inPenaltyBox[currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;
                    //console is one output of the system

                    Console.WriteLine(players[currentPlayer] + " is getting out of the penalty box");
                    places[currentPlayer] = places[currentPlayer] + roll;
                    if (places[currentPlayer] > 11) places[currentPlayer] = places[currentPlayer] - 12;

                    Console.WriteLine(players[currentPlayer]
                            + "'s new location is "
                            + places[currentPlayer]);
                    Console.WriteLine("The category is " + currentCategory());
                    var question = askQuestion(currentCategory(), deck);
                    Console.WriteLine(question.Item1);
                    deck = question.Item2;

                }
                else
                {
                    Console.WriteLine(players[currentPlayer] + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {

                places[currentPlayer] = places[currentPlayer] + roll;
                if (places[currentPlayer] > 11) places[currentPlayer] = places[currentPlayer] - 12;

                Console.WriteLine(players[currentPlayer]
                        + "'s new location is "
                        + places[currentPlayer]);
                Console.WriteLine("The category is " + currentCategory());

                var question = askQuestion(currentCategory(), deck);
                Console.WriteLine(question.Item1);
                deck = question.Item2;

            }

        }

        static private Tuple<string, Deck> askQuestion(string currentCategory, Deck deck)
        {

            if (currentCategory == "Pop")
            {
                return deck.PopQuestion;
            }
            if (currentCategory == "Science")
            {
                return deck.ScienceQuestion;
            }
            if (currentCategory == "Sports")
            {
                return deck.SportsQuestion;
            }
            if (currentCategory == "Rock")
            {
                return deck.RockQuestion;
            }
            return Tuple.Create(string.Empty, deck);
        }


        private String currentCategory()
        {
            if (places[currentPlayer] == 0) return "Pop";
            if (places[currentPlayer] == 4) return "Pop";
            if (places[currentPlayer] == 8) return "Pop";
            if (places[currentPlayer] == 1) return "Science";
            if (places[currentPlayer] == 5) return "Science";
            if (places[currentPlayer] == 9) return "Science";
            if (places[currentPlayer] == 2) return "Sports";
            if (places[currentPlayer] == 6) return "Sports";
            if (places[currentPlayer] == 10) return "Sports";
            return "Rock";
        }

        public bool wasCorrectlyAnswered()
        {
            if (inPenaltyBox[currentPlayer])
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    purses[currentPlayer]++;
                    Console.WriteLine(players[currentPlayer]
                            + " now has "
                            + purses[currentPlayer]
                            + " Gold Coins.");

                    bool winner = didPlayerWin();
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;

                    return winner;
                }
                else
                {
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;
                    return true;
                }



            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                purses[currentPlayer]++;
                Console.WriteLine(players[currentPlayer]
                        + " now has "
                        + purses[currentPlayer]
                        + " Gold Coins.");

                bool winner = didPlayerWin();
                currentPlayer++;
                if (currentPlayer == players.Count) currentPlayer = 0;

                return winner;
            }
        }

        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer] + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;

            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
            return true;
        }


        private bool didPlayerWin()
        {
            return !(purses[currentPlayer] == 6);
        }
    }
    public static class LinkedListExtensions
    {
        static private LinkedList<string> removeQuestion(this LinkedList<string> questions)
        {
            return new LinkedList<string>(questions.Skip(1));
        }
    }
}
