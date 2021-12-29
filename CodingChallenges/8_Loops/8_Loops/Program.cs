using System;
using System.Collections.Generic;

namespace _8_LoopsChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
        }

        /// <summary>
        /// Return the number of elements in the List<int> that are odd.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int UseFor(List<int> x)
        {
            int odds = 0;
            for(int i = 0; i < x.Count; i++)
            {
                if (x[i] % 2 != 0)
                    odds++;
            }
            return odds;
        }

        /// <summary>
        /// This method counts the even entries from the provided List<object> 
        /// and returns the total number found.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int UseForEach(List<object> x)
        {
            int evens = 0;
            foreach (object i in x)
            {
                switch(Type.GetTypeCode(i.GetType()))
                {
                    case TypeCode.Int16:
                        if ((Int16)i % 2 == 0)
                            evens++;
                        break;
                    case TypeCode.Int32:
                        if ((Int32)i % 2 == 0)
                            evens++;
                        break;
                    case TypeCode.Int64:
                        if ((Int64)i % 2 == 0)
                            evens++;
                        break;
                    case TypeCode.UInt16:
                        if ((UInt16)i % 2 == 0)
                            evens++;
                        break;
                    case TypeCode.UInt32:
                        if ((UInt32)i % 2 == 0)
                            evens++;
                        break;
                    case TypeCode.UInt64:
                        if ((UInt64)i % 2 == 0)
                            evens++;
                        break;
                }
            }
            return evens;
        }

        /// <summary>
        /// This method counts the multiples of 4 from the provided List<int>. 
        /// Exit the loop when the integer 1234 is found.
        /// Return the total number of multiples of 4.
        /// </summary>
        /// <param name="x"></param>
        public static int UseWhile(List<int> x)
        {
            int count = 0;
            int multipleCnt = 0;
            while(count < x.Count)
            {
                if (x[count] % 4 == 0)
                    multipleCnt++;
                if (x[count] == 1234)
                    break;
                count ++;
            }
            return multipleCnt;
        }

        /// <summary>
        /// This method will evaluate the Int Array provided and return how many of its 
        /// values are multiples of 3 and 4.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int UseForThreeFour(int[] x)
        {
            int multiples = 0;

            for(int i = 0; i < x.Length; i++)
            {
                if (x[i] % 4 == 0 && x[i] % 3 == 0)
                    multiples++;
            }
            return multiples;
        }

        /// <summary>
        /// This method takes an array of List<string>'s. 
        /// It concatenates all the strings, with a space between each, in the Lists and returns the resulting string.
        /// </summary>
        /// <param name="stringListArray"></param>
        /// <returns></returns>
        public static string LoopdyLoop(List<string>[] stringListArray)
        {
            string sentence = "";
            foreach (List<string> str in stringListArray)
            {
                foreach (string s in str)
                    sentence += s + " ";
            }
            return sentence;
        }
    }
}