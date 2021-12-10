using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rock_Paper_Scissors_Demo1
{
    class GamePlayLogic : RPS_Game
    {
        List<Player> players;
        List<Game> games;
        internal Game game;
        Random randNum;

        //constructor
        public GamePlayLogic()
        {
            players = new List<Player>();
        }

        //overload constructor
        public GamePlayLogic(string fName, string lName)
        {
            this.randNum = new Random();
            this.game = new Game();

            //create player after verifying player does not already exist
            this.players = new List<Player>();

            Player player = new Player(fName, lName);
            Player computer = new Player("Bob", "Ross");

            players.Add(player);

            game.Player1 = computer;
            game.Player2 = player;
        }
        /// <summary>
        /// This method validates the choice that a user made
        /// and makes sure it is 1, 2, or 3
        /// returns Choice.invalid if not.
        /// </summary>
        /// <param name="choice"></param>
        /// <returns>Choice</returns>
        internal Choice ValidateUserChoice(string choice)
        {
            int convertedNumber = -1;
            bool conversionBool = false;

            //this version of TryParse() takes a string and the second argument is an out variable that is instantiated in that moment.
            conversionBool = Int32.TryParse(choice, out convertedNumber);
            if (!conversionBool || convertedNumber < 1 || convertedNumber > 3)
            {
                return Choice.invalid;
            }

            return (Choice)convertedNumber;
        } 

        internal Choice GetComputerChoice()
        {
            Choice computerChoice = (Choice)randNum.Next(1, 4);
            return computerChoice;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1Choice"></param>
        /// <param name="p2Choice"></param>
        internal Player PlayRound(Choice p1Choice, Choice p2Choice)
        {
            //create a round
            Round round = new Round();
            round.Player1 = game.Player1;
            round.Player2 = game.Player2;
            game.rounds.Add(round);

            if ((p1Choice == Choice.Rock && p2Choice == Choice.Paper) || 
                (p1Choice == Choice.Paper && p2Choice == Choice.Scissors) || 
                (p1Choice == Choice.Scissors && p2Choice == Choice.Rock))
            {
                round.Winner = round.Player2;
                return round.Winner;
            }
            else if (p1Choice == p2Choice)
            {
                return null;
            }
            else
            {
                round.Winner = round.Player1;
                return round.Winner;
            }
        }
        /// <summary>
        /// this method iterates over game.rounds to check for winner
        /// </summary>
        /// <returns></returns>
        internal Player WinnerYet()
        {
            //iterate over game.rounds
            if (game.rounds.Count < 2)
                return null;

            int p1RoundWinds = 0;
            int p2RoundWinds = 0;

            foreach (Round r in game.rounds)
            {
                if (r.Winner == game.Player1)
                    p1RoundWinds++;
                else if (r.Winner == game.Player2)
                    p2RoundWinds++;
            }
            if (p1RoundWinds == 2) 
                return game.Player1;
            if (p2RoundWinds == 2)
                return game.Player2;

            return null;
        }

    }
}
