using System;

namespace DayOfTheProgrammer
{
    class Program
    {
        /*
     * Complete the 'dayOfProgrammer' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts INTEGER year as parameter.
     */

        public static string dayOfProgrammer(int year)
        {
            //find the 256th day of the given year according to the russian calendar
            //if year equals 1918 add 14 days, transition from the Julian to Gregorian
            if (year == 1918)
                return "26.09.1918";
            //check if leap year 
            //Julian divisible by 4
            //Gregorian divisible by 400 or divisible by 4 but not not divisible by 100
            else if (year % 4 == 0 && year < 1918 ||
                year % 400 == 0 && year > 1918 ||
                year % 4 == 0 && year % 100 != 0)
                return "12.09." + year;
            else
                return "13.09." + year;
        }
        static void Main(string[] args)
        {
        }
    }
}
