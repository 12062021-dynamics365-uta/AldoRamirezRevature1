using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: InternalsVisibleTo("9_ClassesChallenge.Tests")]
namespace _9_ClassesChallenge
{
    internal class Human2
    {
        private string fName;
        private string lName;
        private string eyeColor;
        private int age;
        private int weight;

        public int Weight { 
            get {return weight; } 
            set 
            {
                if (weight < 0 || weight > 400)
                    weight = 0;
            } 
        }

        public Human2()
        {
            fName = "Pat";
            lName = "Smyth";
        }
        public Human2(string fName, string lName, string eyeColor = "", int age = 0)
        {
            this.fName = fName;
            this.lName = lName;
            this.eyeColor = eyeColor;
            this.age = age;
        }
        public Human2(string fName, string lName, string eyeColor = "")
        {
            this.fName = fName;
            this.lName = lName;
            this.eyeColor = eyeColor;
            this.age = 0;
        }
        public Human2(string fName, string lName, int age = 0)
        {
            this.fName = fName;
            this.lName = lName;
            this.eyeColor = "";
            this.age = age;
        }

        public string AboutMe()
        {
            return $"My name is {fName} {lName}.";
        }

        public string AboutMe2()
        {
            if (age != 0 && eyeColor != "")
                return ($"My name is {fName} {lName}. My age is {age}. My eye color is {eyeColor}.");
            else if (eyeColor != "")
                return ($"My name is {fName} {lName}. My eye color is {eyeColor}.");
            else if (age != 0)
                return ($"My name is {fName} {lName}. My age is {age}.");
            return "";
        }
    }
}