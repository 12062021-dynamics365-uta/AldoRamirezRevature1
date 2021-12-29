using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("9_ClassesChallenge.Tests")]
namespace _9_ClassesChallenge
{
    internal class Human
    {
        private string fName;
        private string lName;

        public Human ()
        {
            fName = "Pat";
            fName = "Smyth";
        }
        public Human (string fName, string lName)
        {
            this.fName = fName;
            this.fName = lName;
        }

        public string AboutMe()
        {
            return $"My name is {fName} {lName}.";
        }
    }//end of class
}//end of namespace