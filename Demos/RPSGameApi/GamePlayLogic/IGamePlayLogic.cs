using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace GamePlayLogic
{
    public interface IGamePlayLogic
    {
        Player WinnerYet();
        List<Player> GetAllPlayers();
        List<Game> PrintUsersGames();
        void Login(string userFName, string userLName);
        void StartNewGame();
        void ResetGame();
        Choice ValidateUserChoice(string choice);
        Choice GetComputerChoice();
        Player PlayRound(Choice p1Choice, Choice p2Choice);
        int GetComputerWins();
        int GetUserWins();
        int GetTies();
        int GetNumRounds();
    }
}
