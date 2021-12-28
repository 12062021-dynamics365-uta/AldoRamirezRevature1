using System;
using System.Collections.Generic;
using System.Linq;

namespace MigratoryBirds
{
    class Program
    {
        public static int migratoryBirds(List<int> arr)
        {
            int maxFrequency = 0; // stores the highest frequency
            int[] frequencies = new int[arr.Max()]; //int array for frequencies
            int birdId = 0; // stores the bird id

            foreach (int id in arr)
            {
                frequencies[id - 1]++; //stores id frequency in frequencies array (id - 1 because arrayList has index starting at 0)
            }

            //loops through frequencies array
            /*for (int i = 0; i < frequencies.Length; i++)
            {
                //if the current maxFrequency is less than frequency at index i
                if (maxFrequency < frequencies[i])
                {
                    //store new maxFrequency
                    maxFrequency = frequencies[i];
                    //store birdId (+1 because arrayList index starts at 0)
                    birdId = i + 1;
                }
            }*/

            //no looping
            maxFrequency = frequencies.Max();
            birdId = frequencies.ToList().IndexOf(maxFrequency);

            return birdId + 1;
        }

        static void Main(string[] args)
        {
        }
    }
}
