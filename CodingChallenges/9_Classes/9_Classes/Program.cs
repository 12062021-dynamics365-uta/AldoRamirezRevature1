using System;

namespace _9_ClassesChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Human defaultHuman = new Human();
            Human human = new Human("Aldo", "Ramirez");

            Console.WriteLine("------Human Class------");
            Console.WriteLine("Human no args:");
            Console.WriteLine(defaultHuman.AboutMe());

            Console.WriteLine("\nHuman with args:");
            Console.WriteLine(human.AboutMe());

            Human2 defalutHuman2 = new Human2();
            Human2 allArgsHuman2 = new Human2("Aldo", "Ramirez", "Blue", 24);
            Human2 noAgeHuman2 = new Human2("Aldo", "Ramirez", "Blue");
            Human2 noEyeClrHuman2 = new Human2("Aldo", "Ramirez", 24);

            Console.WriteLine("\n------Human2 Class------");
            Console.WriteLine("Human2 no args:");
            Console.WriteLine(defalutHuman2.AboutMe());

            Console.WriteLine("\nHuman2 all args:");
            Console.WriteLine(allArgsHuman2.AboutMe2());

            Console.WriteLine("\nHuman2 no age arg:");
            Console.WriteLine(noAgeHuman2.AboutMe2());

            Console.WriteLine("\nHuman2 no eye color arg:");
            Console.WriteLine(noEyeClrHuman2.AboutMe2());
        }
    }
}
