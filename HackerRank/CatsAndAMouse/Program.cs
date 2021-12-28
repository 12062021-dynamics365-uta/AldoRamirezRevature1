using System;

namespace CatsAndAMouse
{
    class Program
    {
        // Complete the catAndMouse function below.
        static string catAndMouse(int x, int y, int z)
        {

            if (Math.Abs(x - z) == Math.Abs(y - z))
                return "Mouse C";
            else if (Math.Abs(x - z) < Math.Abs(y - z))
                return "Cat A";
            else
                return "Cat B";
        }
        static void Main(string[] args)
        {
        }
    }
}
