using System;
using System.Collections.Generic;
using System.Linq;

// ok let's start with some minor adjustments

namespace UglyTrivia
{
    public class Game
    {
        List<string> players = new List<string>();
        // the player is going places...
        // let's promote this one
        protected int[] Places = new int[6];
        int[] _purses = new int[6];

        readonly bool[] _inPenaltyBox = new bool[6];

        LinkedList<string> popQuestions = new LinkedList<string>();
        LinkedList<string> scienceQuestions = new LinkedList<string>();
        LinkedList<string> sportsQuestions = new LinkedList<string>();
        LinkedList<string> rockQuestions = new LinkedList<string>();

        // by default 0
        private int _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;

        public Game()
        {
            for (var i = 0; i < 50; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast(("Science Question " + i));
                sportsQuestions.AddLast(("Sports Question " + i));
                rockQuestions.AddLast(CreateRockQuestion(i));
            }
        }

        public string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool IsPlayable()
        {
            return (HowManyPlayers() >= 2);
        }

        // what does add means?
        // this should be at least addPlayer
        public bool Add(String playerName)
        {
            // ADD?
            players.Add(playerName);
            Places[HowManyPlayers()] = 0;
            _purses[HowManyPlayers()] = 0;
            _inPenaltyBox[HowManyPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int HowManyPlayers()
        {
            return players.Count;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(players[_currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_inPenaltyBox[_currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(players[_currentPlayer] + " is getting out of the penalty box");
                    Places[_currentPlayer] = Places[_currentPlayer] + roll;
                    if (Places[_currentPlayer] > 11) Places[_currentPlayer] = Places[_currentPlayer] - 12;

                    Console.WriteLine($"{players[_currentPlayer]}\'s new location is {Places[_currentPlayer]}");
                    Console.WriteLine("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(players[_currentPlayer] + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                Places[_currentPlayer] = Places[_currentPlayer] + roll;
                // looks like there are ate most 12 places
                if (Places[_currentPlayer] > 11)
                    Places[_currentPlayer] = Places[_currentPlayer] - 12;

                Console.WriteLine($"{players[_currentPlayer]}\'s new location is {Places[_currentPlayer]}");
                Console.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
            }
        }

        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                Console.WriteLine(popQuestions.First());
                popQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Science")
            {
                Console.WriteLine(scienceQuestions.First());
                scienceQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Sports")
            {
                Console.WriteLine(sportsQuestions.First());
                sportsQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Rock")
            {
                Console.WriteLine(rockQuestions.First());
                rockQuestions.RemoveFirst();
            }
        }

        // let's promote this method to protected virtual in order to testit
        // testability wins over encapsulation
        // how does it helps me?
        // well actually not the virtual part so..

        // ok, now first of all I don't want to handle strigns where I should have an enum


        // it's important to notice that the postiion of the categories is an attribute 
        // of the board and not Game itself. should be extracted to a class...
        private readonly Category[] _categories = {
                Category.Pop,
                Category.Science,
                Category.Sports,
                Category.Rock,
                Category.Pop,
                Category.Science,
                Category.Sports,
                Category.Rock,
                Category.Pop,
                Category.Science,
                Category.Sports,
                Category.Rock
            };

        // this should actually return Category but we should first cover all the code that calls it...

        protected string CurrentCategory()
        {
            var place = Places[_currentPlayer];
            return _categories[place].ToString();

        }

        public bool WasCorrectlyAnswered()
        {
            if (_inPenaltyBox[_currentPlayer])
            {
                // here we have some duplication
                if (_isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    _purses[_currentPlayer]++;
                    Console.WriteLine($"{players[_currentPlayer]} now has {_purses[_currentPlayer]} Gold Coins.");

                    bool winner = DidPlayerWin();
                    _currentPlayer++;
                    if (_currentPlayer == players.Count)
                        _currentPlayer = 0;

                    return winner;
                }
                else
                {
                    _currentPlayer++;
                    if (_currentPlayer == players.Count)
                        _currentPlayer = 0;
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Answer was corrent!!!!");
                _purses[_currentPlayer]++;
                Console.WriteLine($"{players[_currentPlayer]} now has {_purses[_currentPlayer]} Gold Coins.");

                bool winner = DidPlayerWin();
                _currentPlayer++;
                if (_currentPlayer == players.Count)
                    _currentPlayer = 0;

                return winner;
            }
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[_currentPlayer] + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;

            _currentPlayer++;
            if (_currentPlayer == players.Count) _currentPlayer = 0;
            return true;
        }


        private bool DidPlayerWin()
        {
            return _purses[_currentPlayer] != 6;
        }
    }

    public enum Category
    {
        Pop,
        Rock,
        Sports,
        Science
    }
}