using System;
using System.Collections.Generic;

namespace Rock_Paper_Scissors_Demo1
{
	class Program
	{
		static void Main(string[] args)
		{
			bool playAgain = true;

			do
			{
				Choice userChoice = Choice.invalid;
				Choice computerChoice;
				int roundWon = 0;
				int roundTied = 0;
				int roundLost = 0;
				int randNum;

				//get input form the user
				Console.WriteLine("Hello. Welcome to Rock-Paper-Scissors Game!");
				Console.WriteLine("What is your first name?");
				string userFName = Console.ReadLine();
				Console.WriteLine("What is your last name?");
				string userLName = Console.ReadLine();
				// save the name as a new player


				GamePlayLogic game = new GamePlayLogic(userFName, userLName);

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
					try
                    {
						Console.WriteLine($"The winner of this round is {roundWinner.Fname} {roundWinner.Lname}");
                    }
					catch (SystemException ex)
                    {
						Console.WriteLine("this is the system exception class");
						Console.WriteLine("\n\nTied Game!");
                    }
					catch (Exception ex)
                    {
						Console.WriteLine("This is the exception class");
                    }
                    finally
                    {
						Console.WriteLine("This is the finally block");
                    }

				}

				Player gameWinner = game.WinnerYet();

				//Show score
				if (roundWon == 2)
					Console.WriteLine("\nCONGRATS YOU WON!");
				else
					Console.WriteLine("\nSORRY YOU LOST!");

				Console.Write($"\nTotal rounds Won - {roundWon}\nTotal rounds Lost - {roundLost}\nTotal Rounds Tied - {roundTied}");

				Console.Write("\nWould you like to play again?");
				string playAgainInput = Console.ReadLine();
				if (playAgainInput.ToLower().Equals("no"))
					playAgain = false;

			} while (playAgain);
		}
	}
}
