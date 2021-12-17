using System;
using System.Collections.Generic;

namespace P0_StoreApplication
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

            List<Store> locations = shopping.CurrentCustomer.StoreLocations;
            int storeChoice = 0;
            do
            {
                Console.WriteLine("\nPlease select a store location by entering number!");
                for (int i = 0; i < locations.Count; i++)
                    Console.WriteLine($"{i + 1}: {locations[i].Location}");
                storeChoice = shopping.validateStoreChoice(Console.ReadLine());

            } while (storeChoice == 0);

            shopping.selectStore(storeChoice);
            Console.WriteLine($"You chose {shopping.CurrentStore.Location}");
        }
    }
}
