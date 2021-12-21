using System;
using System.Collections.Generic;
using Domain;
using Model;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false; //bool to exit
            bool bloginMenu = false; //bool to login
            bool bregisterMenu = false; //bool to register new user
            bool storeMenu = false; //bool to go to store menu
            bool productMenu = false; //bool to go to product menu

            ShoppingLogic shopping = new ShoppingLogic();

            do//loop until exit
            {
                Console.WriteLine("Hello, welcome to virtual shopping!");
                Console.WriteLine("Please select an option");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("1: Login");
                Console.WriteLine("2: Register");
                Console.WriteLine("3: Exit");
                int userChoice = shopping.validateMainMenuChoice(Console.ReadLine());

                switch(userChoice)
                {
                    case 0:
                        Console.WriteLine("Invalid choice: Please choose by number");
                        break;
                    case 1:
                        bloginMenu = true;
                        break;
                    case 2:
                        bregisterMenu = true;
                        break;
                    case 3:
                        shopping.exit();
                        Environment.Exit(1);
                        break;     
                }

                if (bloginMenu)
                {
                    loginMenu(shopping);
                }
                else
                {
                    registerMenu(shopping);
                }

                //Loop until valid store or logout is selected
                do
                {
                    Console.WriteLine("\nSelect a store to shop from or logout!");
                    Console.WriteLine("----------------------------------------------------------");

                    List<Store> storeList = shopping.getListOfStores();
                    foreach (Store s in storeList)
                        Console.WriteLine($"{s.StoreId}: {s.Location}");
                    Console.WriteLine("5: LOGOUT\n");
                    userChoice = shopping.validateStoreChoice(Console.ReadLine());

                    if (userChoice == 0)
                        Console.WriteLine("Invalid choice: Please choose by number");
                    else if (userChoice == storeList.Count + 1)
                    {
                        Console.WriteLine("LOGGING OUT!");
                        storeMenu = false;
                        bloginMenu = false;
                    }
                    else
                    {
                        Console.WriteLine($"You chose to shop from {storeList.Find(x => x.StoreId == userChoice).Location}");
                        storeMenu = true;
                    }

                } while (userChoice == 0);

                if (storeMenu)
                {
                    //Chose between start shopping, view previous orders, or logout
                    Console.WriteLine("\nPlease select an option");
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine("1: Start Shopping");
                    Console.WriteLine("2:");

                    printOrders(shopping.getListOfOrders());

                    //If start shopping show products, checkout, or cancel
                }
            } while (!exit);
        }
        public static void loginMenu(ShoppingLogic shopping)
        {
            bool loggedIn = false;
            do//loop until logged in
            {
                Console.WriteLine("\nPlease enter username and password to login");
                Console.WriteLine("----------------------------------------------------------");
                Console.Write("Enter User Name: ");
                string userName = Console.ReadLine();
                Console.Write("Enter Password: ");
                string password = Console.ReadLine();

                if (shopping.login(userName, password))
                {
                    loggedIn = true;
                    Console.WriteLine($"\nWelcome back {shopping.CurrentCustomer.Fname} {shopping.CurrentCustomer.Lname}");
                }
                else
                {
                    loggedIn = false;
                    Console.WriteLine("Invalid user name or password: Please try again!");
                }

            } while (!loggedIn);
        }

        public static void registerMenu(ShoppingLogic shopping)
        {
            string fName, lName, uName, password;

            Console.WriteLine("\nPlease enter your information below to create an account");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("Enter First Name: ");
            fName = Console.ReadLine();
            Console.WriteLine("Enter Last Name: ");
            lName = Console.ReadLine();
            Console.WriteLine("Enter User Name: ");
            uName = Console.ReadLine();
            Console.WriteLine("Enter Password");
            password = Console.ReadLine();

            shopping.register(fName, lName, uName, password);
            Console.WriteLine($"\nAccount created! Welcome {fName} {lName}");
        }

        public static void printOrders(List<Order> orders)
        {
            foreach (Order o in orders)
            {
                Console.WriteLine($"Order Number: {o.OrderId} - Total: ${o.TotalCost}");
                foreach (Product p in o.Products)
                    Console.WriteLine($"{p.ProductId}: {p.Name} - {p.Description} = ${p.Price}");
            }
        }
    }
}
