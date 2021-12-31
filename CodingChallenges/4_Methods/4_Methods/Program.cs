using System;

namespace _4_MethodsChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string name, greeting;
            name = GetName();
            greeting = GreetFriend(name);
            Console.WriteLine(greeting+"\n");

            double x, y, result;
            x = GetNumber();
            y = GetNumber();

            int action;
            action = GetAction();
            result = DoAction(x, y, action);
        }

        public static string GetName()
        {
            string name;
            Console.Write("Please enter your name: ");
            name = Console.ReadLine();
            return name;
        }

        public static string GreetFriend(string name)
        {
            return $"Hello, {name}. You are my friend.";
        }

        public static double GetNumber()
        {
            double convertedNum;
            bool isNum;
            do
            {
                Console.Write("Enter a double: ");
                isNum = Double.TryParse(Console.ReadLine(), out convertedNum);
                if (!isNum)
                    Console.WriteLine("Invalid input!");
            } while (!isNum);
            return convertedNum;
        }

        public static int GetAction()
        {
            bool validInput = false;
            int choice = 0;
            do
            {
                Console.Write("Enter 1)Add, 2)Subtract, 3)Multiply, or 4)Divide: ");
                string input = Console.ReadLine();
                switch(input)
                {
                    case "1":
                        choice = 1;
                        validInput = true;
                        break;
                    case "2":
                        choice = 2;
                        validInput = true;
                        break;
                    case "3":
                        choice = 3;
                        validInput = true;
                        break;
                    case "4":
                        choice = 4;
                        validInput = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
            } while (!validInput);
            return choice;
        }

        public static double DoAction(double x, double y, int action)
        {
            double result;

            switch (action)
            {
                case 1:
                    result = x + y;
                    Console.WriteLine($"{x} + {y} = {result}");
                    break;
                case 2:
                    result = x - y;
                    Console.WriteLine($"{x} - {y} = {result}");
                    break;
                case 3:
                    result = x * y;
                    Console.WriteLine($"{x} * {y} = {result}");
                    break;
                case 4:
                    result = x / y;
                    Console.WriteLine($"{x} / {y} = {result}");
                    break;
                default:
                    result = 0;
                    throw new FormatException();
            }
            return Math.Abs(result);
        }
    }
}
