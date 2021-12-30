using System;
using System.Collections.Generic;
using System.Linq;

namespace MinMaxSum
{
    class Program
    {
        public static void miniMaxSum(List<int> arr)
        {
            List<long> arrSum = new List<long>() { 0, 0, 0, 0, 0 };

            for (int i = 0; i < arrSum.Count; i++) //Loop through sum array
            {
                for (int j = 0; j < arr.Count; j++) //Loop through passed array
                {
                    //sum everything except counter index and save in arrSum
                    if (j == i)
                        continue;

                    arrSum[i] += arr[j];
                }
            }
            //print min and max of arrSum
            Console.WriteLine($"{arrSum.Min()} {arrSum.Max()}");
        }

        public static void Main(String[] args)
        {

        }
    }
}
