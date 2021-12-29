using System;
using System.Collections.Generic;

namespace _7_GuessingGameChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool playAgain;

            Console.WriteLine("Welcome to the guessing game, the computer will generate\n" +
                "a number between 0 and 100. Your goal is to try and guess that number\n" +
                "within 10 tries! Good Luck!");
            Console.WriteLine("------------------------------------------------------------------------");
            do
            {
                int roundCnt = 1;
                int randNumber = GetRandomNumber();
                List<int> guesses = new List<int>();

                while (roundCnt <= 10)
                {
                    int userNumber = GetUsersGuess();
                    guesses.Add(userNumber);

                    int result = CompareNums(randNumber, userNumber);
                    if (result == 0)
                    {
                        Console.WriteLine("\nCongratulations you guessed right!");
                        break;
                    }
                    else if (result == 1)
                        Console.WriteLine("You guessed to low!");
                    else
                        Console.WriteLine("You guessed to high!");

                    Console.Write($"Your guesses: {string.Join(", ", guesses)}\n\n");
                    roundCnt++;
                }
                playAgain = PlayGameAgain();
                Console.WriteLine();
            } while (playAgain);
        }

        /// <summary>
        /// This method returns a randomly chosen number between 1 and 100, inclusive.
        /// </summary>
        /// <returns></returns>
        public static int GetRandomNumber()
        {
            return new Random().Next(0, 101);
        }

        /// <summary>
        /// This method gets input from the user, 
        /// verifies that the input is valid and 
        /// returns an int.
        /// </summary>
        /// <returns></returns>
        public static int GetUsersGuess()
        {
            int number;
            bool isValid;
            do
            {
                Console.Write("Enter a number between 0 and 100: ");
                isValid = Int32.TryParse(Console.ReadLine(), out number);
                if (!isValid)
                    Console.WriteLine("\nError: Invalid input!");

                if (number < 0 || number > 100)
                {
                    Console.WriteLine("\nError: Number must be between 0 and 100!");
                    isValid = false;
                }
            } while (!isValid);

            return number;
        }

        /// <summary>
        /// This method will has two int parameters.
        /// It will:
        /// 1) compare the first number to the second number
        /// 2) return -1 if the first number is less than the second number
        /// 3) return 0 if the numbers are equal
        /// 4) return 1 if the first number is greater than the second number
        /// </summary>
        /// <param name="randomNum"></param>
        /// <param name="guess"></param>
        /// <returns></returns>
        public static int CompareNums(int randomNum, int guess)
        {
            if (randomNum == guess)
                return 0;
            else if (randomNum < guess)
                return -1;
            else
                return 1;
        }

        /// <summary>
        /// This method offers the user the chance to play again. 
        /// It returns true if they want to play again and false if they do not.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static bool PlayGameAgain()
        {
            Console.Write("Type yes if you want to play again: ");
            string input = Console.ReadLine();

            if (input.ToLower() == "yes")
                return true;
            else
                return false;
        }
    }
}
