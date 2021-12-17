using System;
using System.Collections.Generic;
using Domain;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string userFName;
            string userLName;

            ShoppingLogic shopping = new ShoppingLogic();

            Console.WriteLine("Hello, welcome to virtual shopping!");
            Console.WriteLine("Please enter your first and last name to login");
            Console.Write("Enter first name: ");
            userFName = Console.ReadLine();
            Console.Write("Enter last name: ");
            userLName = Console.ReadLine();

            //Login the customer
            shopping.login(userFName, userLName);

            
        }
    }
}
