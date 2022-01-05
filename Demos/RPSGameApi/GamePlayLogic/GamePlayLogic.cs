using System;
using Models;
using Storage;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;

namespace Domain
{
    public class GamePlayLogic : IGamePlayLogic
    {
        List<Player> players;// all the registered players
        List<Game> games;// all the played games
        Random randNum;
        private Game currentGame;
        private Player currentLoggedInPlayer;
        private readonly IDataBaseAccess _dataBaseAccess;
        private readonly IMapper _mapper;

        //constructor
        public GamePlayLogic(IDataBaseAccess dba, IMapper mapper)
        {
            players = new List<Player>();
            games = new List<Game>();
            randNum = new Random();
            this._dataBaseAccess = dba;
            this._mapper = mapper;
        }
        // overload constructor that is called as the first constructor or the first game after compilation
        public GamePlayLogic(string fname, string lname)
        {
            randNum = new Random();// get the random generator working
            //create a enw player based on the names.. after varifying that the player doesn't already exist.
            this.players = new List<Player>();// the new player
            Player computer = new Player("Max", "HeadRoom");
            this.currentGame = new Game();// the current game
            Player player = new Player(fname, lname);//create a new player
            this.players.Add(player);// add the new player to the list of players
            currentGame.Player1 = computer;
            currentGame.Player2 = player;
        }

        public List<Player> GetAllPlayers()
        {
            List<Player> players = new List<Player>();
            players = this._dataBaseAccess.GetAllPlayers();
            return players;
        }

        public List<Game> PrintUsersGames()
        {
            return games.Where(x => x.Player2.Fname == this.currentLoggedInPlayer.Fname
            && x.Player2.Lname == this.currentLoggedInPlayer.Lname).ToList();
        }

        /// <summary>
        /// This method will see if the loggin in user already exists
        /// if so, will assign that player to currentLoggedInPlayer
        /// if not, will create a new player and assign that player to currentLoggedInPlayer
        /// </summary>
        /// <param name="userFName"></param>
        /// <param name="userLName"></param>
        public Player Login(string fname, string lname)
        {
            //Call the DbAccess method to get the user with these credentials
            //if non null result, then th eplayer is returned to the client
            DataTableReader dtr = _dataBaseAccess.Login(fname, lname);

            //run the return through the mapper to return a Player (or null)
            Player p = _mapper.EntityToPlayer(dtr);

            return p;
            #region old code
            //cant use this bc state can't be helo in the API (thats what the db is for
            /*Player p = players.Where(p => p.Fname == userFName && p.Lname == userLName).FirstOrDefault();

            if (p == null)
            {
                Player p1 = new Player(userFName, userLName);
                this.currentLoggedInPlayer = p1;
                players.Add(p1);
                //this.currentGame.Player2 = p1;
            }
            else
            {
                this.currentLoggedInPlayer = p;
            }*/
            #endregion
        }

        /// <summary>
        /// This method will create a new game with the 
        /// currentLoggedinPlayer as P2 and the computer as P1.
        /// </summary>
        public void StartNewGame()
        {
            this.currentGame = new Game();
            // create the computer
            Player comp = players.Where(p => p.Fname == "Max" && p.Lname == "HeadRoom").FirstOrDefault();
            if (comp == null)
            {
                Player comp1 = new Player("Max", "HeadRoom");
                this.players.Add(comp1);
                this.currentGame.Player1 = comp1;
                this.currentGame.Player2 = this.currentLoggedInPlayer;
            }
            else
            {
                this.currentGame.Player1 = comp;
                this.currentGame.Player2 = this.currentLoggedInPlayer;
            }
        }

        /// <summary>
        /// This method will set the currentloggedinplayer and current game to null.
        /// ready to start another game.
        /// </summary>
        public void ResetGame()
        {
            this.currentGame = null;
            this.currentLoggedInPlayer = null;
        }

        /// <summary>
        /// This method validates that the choice that the user made was a 1, 2, or 3.
        /// Returns Choice.invalid if not.
        /// </summary>
        /// <param name="choice"></param>
        /// <returns></returns>
        public Choice ValidateUserChoice(string choice)
        {
            int convertedNumber = -1;
            bool conversionBool = false;
            conversionBool = Int32.TryParse(choice, out convertedNumber);
            if (!conversionBool || convertedNumber < 1 || convertedNumber > 3)
            {
                return Choice.invalid;
            }
            return (Choice)convertedNumber;
        }

        public Choice GetComputerChoice()
        {
            Choice computerChoice = (Choice)randNum.Next(1, 4);// inclusive of the first (lower) value and exclusive of hte second(upper) value. 
            return computerChoice;
        }


        /// <summary>
        /// this method will take the choices of the 2 players, 
        /// create a new round, 
        /// evaluate the choices,
        /// and determine if a winner has prevailed.
        /// </summary>
        /// <param name="p1Choice"></param>
        /// <param name="p2Choice"></param>
        public Player PlayRound(Choice p1Choice, Choice p2Choice)
        {
            //create a round
            Round round = new Round();
            round.Player1 = currentGame.Player1;//assign the correct players to the round
            round.Player2 = currentGame.Player2;
            round.P1Choice = p1Choice;
            round.P2Choice = p2Choice;
            currentGame.Rounds.Add(round);

            // all the ways that the user wins.
            if (((int)p1Choice == 1 && p2Choice == Choice.Paper) ||
                ((int)p1Choice == 2 && p2Choice == Choice.Scissors) ||
                ((int)p1Choice == 3 && p2Choice == Choice.Rock))
            {
                round.Winner = round.Player2;
                return round.Winner;
                // Console.WriteLine("You won this round.\n");
            }
            else if (p1Choice == p2Choice)
            {
                //Console.WriteLine("This round was a tie.\n");
                round.Winner = null;
                return null;
            }
            else
            {
                //Console.WriteLine("The computer won this round.\n");
                round.Winner = round.Player1;
                return round.Winner;
            }
        }

        /// <summary>
        /// This method iterates over currentGame.Rounds to see is ther is a winner yet.
        /// </summary>
        /// <returns></returns>
        public Player WinnerYet()
        {
            //iterate over the Rounds List while keeping track of who won each round
            if (this.currentGame.Rounds.Count < 2) return null;// there can't be a winner yet if there are less than 2 rounds total.
            int p1RoundWins = 0;
            int p2RoundWins = 0;

            foreach (Round r in this.currentGame.Rounds)
            {
                if (r.Winner == this.currentGame.Player1) p1RoundWins++;
                if (r.Winner == this.currentGame.Player2) p2RoundWins++;
            }
            if (p1RoundWins == 2)
            {
                //store the game in the List<Game>
                games.Add(this.currentGame);
                Player p = this.currentGame.Player1;
                //this.currentGame = null;
                return p;
            }
            if (p2RoundWins == 2)
            {
                //store the game in the List<Game>
                games.Add(this.currentGame);
                Player p = currentGame.Player2;
                //this.currentGame = null;
                return p;
            }
            return null;
        }

        /// <summary>
        /// This method returns how many rounds the computer won.
        /// </summary>
        /// <returns></returns>
        public int GetComputerWins()
        {
            int compWins = 0;
            foreach (Round r in currentGame.Rounds)
            {
                if (r.Winner == r.Player1) compWins++;
            }
            return compWins;
        }

        /// <summary>
        /// This method returns how many rounds the user won.
        /// </summary>
        /// <returns></returns>
        public int GetUserWins()
        {
            int userWins = 0;
            foreach (Round r in currentGame.Rounds)
            {
                if (r.Winner == r.Player2) userWins++;
            }
            return userWins;
        }

        /// <summary>
        /// This method returns how many rounds were a tie.
        /// </summary>
        /// <returns></returns>
        public int GetTies()
        {
            int ties = 0;
            foreach (Round r in currentGame.Rounds)
            {
                if (r.Winner == null) ties++;
            }
            return ties;
        }

        /// <summary>
        /// This method returns how many rounds wer played in the current, completed, game
        /// </summary>
        /// <returns></returns>
        public int GetNumRounds()
        {
            return this.currentGame.Rounds.Count;
        }
    }
}
