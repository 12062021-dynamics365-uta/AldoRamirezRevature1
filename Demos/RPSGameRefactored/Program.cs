using System;
using System.Collections.Generic;

namespace Rock_Paper_Scissors_Demo1
{
	class Program
	{
		static void Main(string[] args)
		{
			Choice userChoice = Choice.invalid;
			Choice computerChoice;
			bool logout = false;
			GamePlayLogic game = new GamePlayLogic();

			do
			{
				//get input form the user
				Console.WriteLine("Hello. Welcome to Rock-Paper-Scissors Game!");
				Console.WriteLine("What is your first name?");
				string userFName = Console.ReadLine();
				Console.WriteLine("What is your last name?");
				string userLName = Console.ReadLine();

				//log in the player
				game.Login(userFName, userLName);

				do
				{
					Console.WriteLine("\nEnter 1 to play a game.\nEnter 2 to see game history.\nEnter 3 to quit.");
					string menuChoice = Console.ReadLine();
					userChoice = game.ValidateUserChoice(menuChoice);
					if (userChoice == Choice.invalid)
						Console.WriteLine("Hey, buddy... that wasn't a 1 or 2 or 3!");

					switch (userChoice)
					{
						case Choice.Rock:
							break;
						case Choice.Paper:
							List<Game> userGames = game.PrintUserGames();
							PrintUserGames1(userGames);
							break;
						case Choice.Scissors:
							Environment.Exit(1);
							break;
					}
				} while (userChoice == Choice.invalid);
				
				bool playAgain = true;

				userChoice = Choice.invalid;
				do
				{
					game.StartNewGame();
					//loop till one player has won 2 rounds
					while (game.WinnerYet() == null)
					{
						do
						{
							Console.WriteLine("\nPlease enter enter 1 for ROCK, 2 for PAPER, 3 for SCISSORS");
							string userInput = Console.ReadLine();

							userChoice = game.ValidateUserChoice(userInput);
							if (userChoice == Choice.invalid)
							{
								Console.WriteLine("Hey, buddy... that wasn't a 1 or 2 or 3!");
							}

						} while (userChoice == Choice.invalid);

						//get the computers choice
						computerChoice = game.GetComputerChoice();
						Console.WriteLine($"Computer chose {computerChoice}");

						Player roundWinner = game.PlayRound(computerChoice, userChoice);

						if (roundWinner != null)
						{
							Console.WriteLine($"The winner of this round is {roundWinner.Fname} {roundWinner.Lname}");
						}
						else
						{
							Console.WriteLine("Tied Round!");
							/*try
							{
								Console.WriteLine($"The winner of this round is {roundWinner.Fname} {roundWinner.Lname}");
							}
							catch (SystemException ex)
							{
								Console.WriteLine("\nTied Round!");
							}
							catch (Exception ex)
							{
								Console.WriteLine("This is the exception class");
							}*/
						}

					}

					//Player gameWinner = game.GetWinnerOfLastGame();


					//Show score
					if (game.GetUserWins() == 2)
						Console.WriteLine("\nCONGRATS YOU WON!");
					else
						Console.WriteLine("\nSORRY YOU LOST!");

					Console.Write($"\nTotal rounds Won - {game.GetUserWins()}\nTotal rounds Lost - {game.GetComputerWins()}\nTotal Rounds Tied - {game.GetTies()}");
					Console.Write($"\nThis game was {game.GetNumRounds()} rounds long!");

					Console.Write("\nWould you like to play again?");
					string playAgainInput = Console.ReadLine();
					Console.Write("\n");
					if (playAgainInput.ToLower().Equals("no"))
					{
						playAgain = false;
						logout = true;
					}

				} while (playAgain);
				game.ResetGame();

			} while (logout);
		}

		public static void PrintUserGames1(List<Game> games)
        {
			int counter = 0;
			Console.WriteLine($"\nThere are {games.Count} games in your history");

			foreach (Game game in games)
            {
				counter++;
				Console.WriteLine($"\nThis is game {counter}.");
				Console.WriteLine($"This game was between {game.Player1.Fname} {game.Player1.Lname} and {game.Player2.Fname} {game.Player1.Lname}.");
				foreach (Round r in game.rounds)
                {
					if (r.Winner != null)
						Console.WriteLine($"{r.Winner.Fname} {r.Winner.Lname} Won round {1}");
					else
						Console.WriteLine($"Round {1} was a tie");
                }
            }
        }
	}
}
