using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _11_ArraysAndListsChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Console.Write("Array: ");
            Array.ForEach(array, x => Console.Write($"{x} "));
            Console.WriteLine($"\nAverage of array: {AverageOfValues(array)}\n");

            array = SunIsShining(array);
            Console.WriteLine("Every value in array incremented by 2: ");
            Console.Write("Array: ");
            Array.ForEach(array, x => Console.Write($"{x} "));

            ArrayList myArrayList = new ArrayList() { 5, 1.1, 0, 2.2, "hellos", 9, 3 };
            Console.Write("\n\nArrayList: ");
            foreach (var i in myArrayList)
                Console.Write($"{i} ");
            Console.WriteLine($"\nAverage of array {ArrayListAvg(myArrayList)}\n");

            List<int> list = new List<int>() { 9, 8, 7, 6, 5, 3, 2, 1 };
            int newNum = 4;
            Console.Write("List: ");
            list.ForEach(x => Console.Write($"{x} "));
            Console.WriteLine($"\nNumber to insert: {newNum}");
            Console.WriteLine($"Rank of inserted number after sorting: {ListAscendingOrder(list, newNum)}\n");

            List<string> words = new List<string>() { "Hi", "My", "Name", "is", "Aldo" };
            string word1 = "Aldo";
            string word2 = "Hello";
            Console.Write("List: ");
            words.ForEach(x => Console.Write($"{x} "));
            Console.WriteLine($"\n'{word1}' found? {FindStringInList(words, word1)}");
            Console.WriteLine($"'{word2}' found? {FindStringInList(words, word2)}");
        }//EoM

        /// <summary>
        /// This method takes an array of integers and returns a double, the average 
        /// value of all the integers in the array
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static double AverageOfValues(int[] array)
        {
            return array.Average();
        }

        /// <summary>
        /// This method increases each array element by 2 and 
        /// returns an array with the altered values.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int[] SunIsShining(int[] x)
        {
            for (int i = 0; i < x.Length; i++)
                x[i] += 2;
            return x;
        }

        /// <summary>
        /// This method takes an ArrayList containing types of double, int, and string 
        /// and returns the average of the ints and doubles only, as a decimal.
        /// It ignores the string values and rounds the result to 3 decimal places toward the nearest even number.
        /// </summary>
        /// <param name="myArrayList"></param>
        /// <returns></returns>
        public static decimal ArrayListAvg(ArrayList myArrayList)
        {
            decimal avg = 0;
            int count = 0;
            for (int i = 0; i < myArrayList.Count; i++)
            {
                if (myArrayList[i].GetType() != typeof(string))
                {
                    avg += Convert.ToDecimal(myArrayList[i]);
                    count++;
                }
            }
            return decimal.Round(avg / count, 3);
        }

        /// <summary>
        /// This method returns the rank (starting with 1st place) of a new 
        /// score entered into a list of randomly ordered scores.
        /// </summary>
        /// <param name="myArray1"></param>
        public static int ListAscendingOrder(List<int> scores, int yourScore)
        {
            scores.Add(yourScore);
            scores.Sort();
            return scores.IndexOf(yourScore) + 1;
        }

        /// <summary>
        /// This method has with two parameters takes a List<string> and a string.
        /// The method returns true if the string parameter is found in the List, otherwise false.
        /// </summary>
        /// <param name="myArray2"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool FindStringInList(List<string> myArray2, string word)
        {
            return myArray2.Contains(word);
        }
    }//EoP
}// EoN