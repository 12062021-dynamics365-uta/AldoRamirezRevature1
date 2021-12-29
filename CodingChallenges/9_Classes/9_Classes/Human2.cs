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
            if(eyeColor.)
        }
    }
}