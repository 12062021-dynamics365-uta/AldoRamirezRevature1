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
        private int? age;
        private int weight;
        public int Weight { 
            get {return weight; } 
            set 
            {
                if (value < 0 || value > 400)
                    weight = 0;
                else
                    weight = value;
            } 
        }

        public Human2()
        {
            fName = "Pat";
            lName = "Smyth";
        }
        public Human2(string fName, string lName, string eyeColor, int age)
        {
            this.fName = fName;
            this.lName = lName;
            this.eyeColor = eyeColor;
            this.age = age;
        }
        public Human2(string fName, string lName, int age)
        {
            this.fName = fName;
            this.lName = lName;
            this.age = age;
        }
        public Human2(string fName, string lName, string eyeColor)
        {
            this.fName = fName;
            this.lName = lName;
            this.eyeColor = eyeColor;
        }

        public string AboutMe()
        {
            return $"My name is {fName} {lName}.";
        }

        public string AboutMe2()
        {
            if (eyeColor == null && age == null)
                return $"My name is {fName} {lName}.";
            else if (eyeColor == null)
                return $"My name is {fName} {lName}. My age is {age}.";
            else if (age == null)
                return $"My name is {fName} {lName}. My eye color is {eyeColor}.";
            else
                return $"My name is {fName} {lName}. My age is {age}. My eye color is {eyeColor}.";
        }
    }
}