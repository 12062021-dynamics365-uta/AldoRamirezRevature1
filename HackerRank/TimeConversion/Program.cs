using System;

namespace TimeConversion
{
    class Program
    {
        /*
     * Complete the 'timeConversion' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts STRING s as parameter.
     */

        public static string timeConversion(string s)
        {
            string subTime = s.Substring(0, s.Length - 2);
            string abbrev = s.Substring(s.Length - 2);

            if (abbrev.Equals("AM")) //Check if its AM
            {
                //If its 12 replace with 00
                if (subTime.Substring(0, 2).Equals("12"))
                    return "00" + subTime.Substring(2);
            }
            else // else its PM
            {
                //if not 12 replace with military time(13,14,15...)
                if (!subTime.Substring(0, 2).Equals("12"))
                {
                    int num = Int32.Parse(subTime.Substring(0, 2));
                    num += 12;
                    return num.ToString() + subTime.Substring(2);
                }
            }
            return subTime;
        }
        static void Main(string[] args)
        {
        }
    }
}
