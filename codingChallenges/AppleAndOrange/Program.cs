using System;
using System.Collections.Generic;

namespace AppleAndOrange
{
    class Program
    {
        public static void countApplesAndOranges(int s, int t, int a, int b, List<int> apples, List<int> oranges)
        {
            int appleCnt = 0;
            int orangeCnt = 0;

            foreach (int d in apples)
            {
                int distTree = a + d;
                if (distTree >= s && distTree <= t)
                    appleCnt++;
            }
            foreach (int d in oranges)
            {
                int distTree = b + d;
                if (distTree >= s && distTree <= t)
                    orangeCnt++;
            }

            Console.WriteLine(appleCnt);
            Console.WriteLine(orangeCnt);
        }
        static void Main(string[] args)
        {
        }
    }
}
