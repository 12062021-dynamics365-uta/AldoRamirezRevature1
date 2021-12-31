using System;

namespace StringManipulationChallenge
{
    public class Program
    {
        static void Main(string[] args)
        {
            //StringToUpper
            string toUpper;
            Console.Write("Enter a lowercase string: ");
            toUpper = StringToUpper(Console.ReadLine());
            Console.WriteLine($"New string is: {toUpper}\n");

            //StringToLower
            string toLower;
            Console.Write("Enter an uppercase string: ");
            toLower = StringToLower(Console.ReadLine());
            Console.WriteLine($"New string is: {toLower}\n");

            //StringTrim
            string trim;
            Console.Write("Enter string with whitespace before and after: ");
            trim = StringTrim(Console.ReadLine());
            Console.WriteLine($"New string is: {trim}\n");

            //StringSubstring
            string testString = "This is a string!";
            Console.WriteLine(testString);
            Console.Write("Enter an start index to get substring from string above: ");
            int index = Int32.Parse(Console.ReadLine());
            Console.Write("Enter the length of the substring: ");
            int length = Int32.Parse(Console.ReadLine());
            string subString = StringSubstring(testString, index, length);
            Console.WriteLine($"Substring: {subString}\n");

            //SearchChar
            string input;
            char a;
            Console.Write("Enter a string: ");
            input = Console.ReadLine();
            Console.Write("Enter a char you want to search for: ");
            a = char.Parse(Console.ReadLine());
            int charIndex = SearchChar(input, a);
            Console.WriteLine($"Index of char {a} is {charIndex}\n");

            //ConcatNames
            string fName, lName, fullName;
            Console.Write("Enter first name: ");
            fName = Console.ReadLine();
            Console.Write("Enter last name: ");
            lName = Console.ReadLine();
            fullName = ConcatNames(fName, lName);
            Console.WriteLine($"Hello {fullName}!");
        }

        /// <summary>
        /// This method has one string parameter and will: 
        /// 1) change the string to all upper case and 
        /// 2) return the new string.
        /// </summary>
        /// <param name="usersString"></param>
        /// <returns></returns>
        public static string StringToUpper(string usersString)
        {
            return usersString.ToUpper();
        }

        /// <summary>
        /// This method has one string parameter and will:
        /// 1) change the string to all lower case,
        /// 2) return the new string into the 'lowerCaseString' variable
        /// </summary>
        /// <param name="usersString"></param>
        /// <returns></returns>       
        public static string StringToLower(string usersString)
        {
            return usersString.ToLower();
        }

        /// <summary>
        /// This method has one string parameter and will:
        /// 1) trim the whitespace from before and after the string, and
        /// 2) return the new string.
        /// HINT: When getting input from the user (you are the user), try inputting "   a string with whitespace   " to see how the whitespace is trimmed.
        /// </summary>
        /// <param name="usersStringWithWhiteSpace"></param>
        /// <returns></returns>
        public static string StringTrim(string usersStringWithWhiteSpace)
        {
            return usersStringWithWhiteSpace.Trim();
        }

        /// <summary>
        /// This method has three parameters, one string and two integers. It will:
        /// 1) get the substring based on the first integer element and the length 
        /// of the substring desired.
        /// 2) return the substring.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="firstElement"></param>
        /// <param name="lastElement"></param>
        /// <returns></returns>
        public static string StringSubstring(string x, int firstElement, int lengthOfSubsring)
        {
            return x.Substring(firstElement, lengthOfSubsring);
        }

        /// <summary>
        /// This method has two parameters, one string and one char. It will:
        /// 1) search the string parameter for first occurrance of the char parameter and
        /// 2) return the index of the char.
        /// HINT: Think about how StringTrim() (above) would be useful in this situation
        /// when getting the char from the user. 
        /// </summary>
        /// <param name="userInputString"></param>
        /// <param name="charUserWants"></param>
        /// <returns></returns>
        public static int SearchChar(string userInputString, char charUserWants)
        {
            return userInputString.IndexOf(charUserWants);
        }

        /// <summary>
        /// This method has two string parameters. It will:
        /// 1) concatenate the two strings with a space between them.
        /// 2) return the new string.
        /// HINT: You will need to get the users first and last name in the 
        /// main method and send them as arguments.
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <returns></returns>
        public static string ConcatNames(string fName, string lName)
        {
            return $"{fName} {lName}";
        }
    }//end of program
}
