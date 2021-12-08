using System;
using System.Collections.Generic;

namespace Rock_Paper_Scissors_Demo1
{
	class Program
	{
		static void Main(string[] args)
		{
			int roundWon = 0;
			int roundTied = 0;
			int roundLost = 0;
			int randNum;

			//get input form the user
			Console.WriteLine("Hello. Welcome to Rock-Paper-Scissors Game!");

			while (roundWon < 2 && roundLost < 2)
			{
				int convertedNumber = -1;
				bool conversionBool = false;
				do
				{
					Console.WriteLine("\nPlease enter enter 1 for ROCK, 2 for PAPER, 3 for SCISSORS");
					string userInput = Console.ReadLine();

					//validate the use input as a 1, 2, or 3
					//this version of TryParse() takes a string and the second argument is an out variable that is instantiated in that moment.
					conversionBool = Int32.TryParse(userInput, out convertedNumber);
					if (!conversionBool || convertedNumber < 1 || convertedNumber > 3)
					{
						Console.WriteLine("Hey, buddy... that wasn't a 1 or 2 or 3!");
					}

				} while (!(convertedNumber > 0 && convertedNumber < 4));

				randNum = new Random().Next(1, 4);// inclusive of the first (lower) value and exclusive of hte second(upper) value.

				switch(convertedNumber)
                {
					case 1:
						Console.WriteLine("You chose ROCK");
						if (randNum == 1)
						{
							Console.WriteLine("Opponent chose ROCK\nROCK vs ROCK - Round tied!");
							roundTied++;
						}
						else if (randNum == 2)
						{
							Console.WriteLine("Opponent chose PAPER\nROCK vs PAPER - Round lost!");
							roundLost++;
						}
						else
						{
							Console.WriteLine("Opponent chose SCISSORS\nROCK vs SCISSORS - Round Won!");
							roundWon++;
						}
						break;
					case 2:
						Console.WriteLine("You chose PAPER");
						if (randNum == 1)
						{
							Console.WriteLine("Opponent chose ROCK\nPAPER vs ROCK - Round Won!");
							roundWon++;
						}
						else if (randNum == 2)
						{
							Console.WriteLine("Opponent chose PAPER\nPAPER vs PAPER - Round Tied!");
							roundTied++;
						}
						else
						{
							Console.WriteLine("Opponent chose SCISSORS\nPAPER vs SCISSORS - Round Lost!");
							roundLost++;
						}
						break;
					case 3:
						Console.WriteLine("You chose SCISSORS");
						if (randNum == 1)
						{
							Console.WriteLine("Opponent chose ROCK\nSCISSORS vs ROCK - Round Lost!");
							roundLost++;
						}
						else if (randNum == 2)
						{
							Console.WriteLine("Opponent chose PAPER\nSCISSORS vs PAPER - Round Won!");
							roundWon++;
						}
						else
						{
							Console.WriteLine("Opponent chose SCISSORS\nSCISSORS vs SCISSORS - Round Tied!");
							roundTied++;
						}
						break;
                }
			}

			if (roundWon == 2)
				Console.WriteLine("\nCONGRATS YOU WON!");
			else
				Console.WriteLine("\nSORRY YOU LOST!");

			Console.Write($"\nTotal rounds Won - {roundWon}\nTotal rounds Lost - {roundLost}\nTotal Rounds Tied - {roundTied}");
		}
	}
}
