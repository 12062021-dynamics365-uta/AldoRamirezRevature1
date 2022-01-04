using System;

namespace SweetnSaltyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int groupCount = 0; //Group count
            int sweetCnt = 0; //sweet count
            int saltyCnt = 0; //salty count
            int sweetNSalty = 0; //sweet'nSalty count

            for(int i = 1; i <= 1000; i++) //Loop from 1 to 1000
            {
                groupCount++; //Increment group count

                if (i % 3 == 0 && i % 5 == 0) //Multiples of both 3 and 5
                {
                    Console.Write("sweet'nSalty "); //Print sweet'nSalty
                    sweetNSalty++; //Increment sweet'nSalty count
                }
                else if (i % 3 == 0) //Multiples of only 3
                {
                    Console.Write("sweet "); //Print sweet
                    sweetCnt++; //Increment sweet count
                }
                else if (i % 5 == 0) //Multiples of only 5
                {
                    Console.Write("salty "); //Print salty
                    saltyCnt++; //Increment salty count
                }
                else //Not multiple of either 3 or 5
                    Console.Write($"{i} "); //Print number

                if (groupCount == 20) //Number of prints per line reaches 20
                {
                    Console.WriteLine(); //New line
                    groupCount = 0; //Reset group count
                }
            }

            Console.WriteLine($"\nTotal sweet: {sweetCnt}"); //Print sweet total
            Console.WriteLine($"Total salty: {saltyCnt}"); //Print salty total
            Console.WriteLine($"Total sweet'nSalty: {sweetNSalty}"); //Print sweet'nSalty total

        }
    }
}
