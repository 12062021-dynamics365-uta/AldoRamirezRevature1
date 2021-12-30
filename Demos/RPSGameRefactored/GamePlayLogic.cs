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
        Random randNum;
        private Game currentGame;
        private Player currenLoggedInPlayer;

        //constructor
        public GamePlayLogic()
        {
            players = new List<Player>();
            games = new List<Game>();
            randNum = new Random();
            //this.currentGame = new Game();
            //Player computer = new Player("Bob", "Ross");
            //this.currentGame.Player1 = computer;
        }

        //overload constructor
        public GamePlayLogic(string fName, string lName)
        {
            this.randNum = new Random();
            this.currentGame = new Game();

            //create player after verifying player does not already exist
            this.players = new List<Player>();

            Player player = new Player(fName, lName);
            Player computer = new Player("Bob", "Ross");

            players.Add(player);

            currentGame.Player1 = computer;
            currentGame.Player2 = player;
        }

        internal void Login(string userFName, string userLName)
        {
            Player p = players.Where(p => p.Fname == userFName && p.Lname == userLName).FirstOrDefault();
            if (p == null)
            {
                Player p1 = new Player(userFName, userLName);
                this.currenLoggedInPlayer = p1;
                players.Add(p1);
            }
            else
            {
                this.currenLoggedInPlayer = p;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void StartNewGame()
        {
            this.currentGame = new Game();

            Player comp = players.Where(p => p.Fname == "Bob" && p.Lname == "Ross").FirstOrDefault();
            if (comp == null)
            {
                Player computer = new Player("Bob", "Ross");
                players.Add(computer);
                this.currentGame.Player1 = computer;
                this.currentGame.Player2 = this.currenLoggedInPlayer;
            }
            else
            {
                this.currentGame.Player1 = comp;
                this.currentGame.Player2 = this.currenLoggedInPlayer;
            }

        }

        /// <summary>
        /// This method validates the choice that a user made
        /// and makes sure it is 1, 2, or 3
        /// returns Choice.invalid if not.
        /// </summary>
        /// <param name="choice"></param>
        /// <returns>Choice</returns>
        public Choice ValidateUserChoice(string choice)
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Choice GetComputerChoice()
        {
            Choice computerChoice = (Choice)randNum.Next(1, 4);
            return computerChoice;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1Choice"></param>
        /// <param name="p2Choice"></param>
        public Player PlayRound(Choice p1Choice, Choice p2Choice)
        {
            //create a round
            Round round = new Round();
            round.Player1 = currentGame.Player1;
            round.Player2 = currentGame.Player2;
            round.P1Choice = p1Choice;
            round.P2Choice = p2Choice;
            currentGame.rounds.Add(round);

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
        public Player WinnerYet()
        {
            //iterate over game.rounds
            if (currentGame.rounds.Count < 2)
                return null;

            int p1RoundWinds = 0;
            int p2RoundWinds = 0;

            foreach (Round r in currentGame.rounds)
            {
                if (r.Winner == currentGame.Player1)
                    p1RoundWinds++;
                else if (r.Winner == currentGame.Player2)
                    p2RoundWinds++;
            }
            if (p1RoundWinds == 2)
            {
                games.Add(this.currentGame);
                Player p = currentGame.Player1;
                //this.currentGame = null;
                return p;
            }
            if (p2RoundWinds == 2)
            {
                games.Add(this.currentGame);
                Player p = currentGame.Player2;
                //this.currentGame = null;
                return p;
            }

            return null;
        }

        public int GetComputerWins()
        {
            int compWins = 0;

            foreach (Round r in currentGame.rounds)
            {
                if (r.Winner == r.Player1)
                    compWins++;
            }
            return compWins;
        }

        public int GetUserWins()
        {
            int userWins = 0;

            foreach (Round r in currentGame.rounds)
            {
                if (r.Winner == r.Player2)
                    userWins++;
            }
            return userWins;
        }

        public int GetTies()
        {
            int ties = 0;

            foreach (Round r in currentGame.rounds)
            {
                if (r.Winner == null)
                    ties++;
            }
            return ties;
        }

        public int GetNumRounds()
        {
            return currentGame.rounds.Count;
        }
        /// <summary>
        /// 
        /// </summary>
        internal void ResetGame()
        {
            this.currentGame = null;
            this.currenLoggedInPlayer = null;
        }

        internal List<Game> PrintUserGames()
        {
            return games.Where(x => x.Player2.Fname == this.currenLoggedInPlayer.Fname && x.Player2.Lname == this.currenLoggedInPlayer.Lname).ToList();
        }
    }
}
