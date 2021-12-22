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

            ShoppingLogic shopping = new ShoppingLogic();

            do//loop until exit
            {
                int menuChoice = mainMenu(shopping);

                if (menuChoice == 1)
                    loginMenu(shopping);
                else if (menuChoice == 2)
                    registerMenu(shopping);
                else
                    break;

                menuChoice = storeListMenu(shopping);

                if (menuChoice == 1)
                {
                    menuChoice = storeMenu(shopping);
                    if (menuChoice == 1)
                        shoppingMenu(shopping);
                    //Shopping menu
                    //view cart
                    //checkout
                }
            } while (!exit);
        }

        public static int mainMenu(ShoppingLogic shopping)
        {
            int userChoice = 0;
            do
            {
                Console.WriteLine("Hello, welcome to virtual shopping!");
                Console.WriteLine("Please select an option");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("1: Login");
                Console.WriteLine("2: Register");
                Console.WriteLine("3: Exit");
                userChoice = shopping.validateMainMenuChoice(Console.ReadLine());

                if(userChoice == 0)
                    Console.WriteLine("Invalid choice: Please choose by number\n");
                if(userChoice == 3)
                    shopping.exit();
            } while (userChoice == 0);
            Console.Clear();
            return userChoice;
        }

        /// <summary>
        /// View that displays the login menu
        /// </summary>
        /// <param name="shopping"></param>
        public static void loginMenu(ShoppingLogic shopping)
        {
            bool loggedIn = false;
            do//loop until logged in
            {
                Console.WriteLine("Please enter username and password to login");
                Console.WriteLine("----------------------------------------------------------");
                Console.Write("Enter User Name: ");
                string userName = Console.ReadLine();
                Console.Write("Enter Password: ");
                string password = Console.ReadLine();

                Console.Clear();
                if (shopping.login(userName, password))
                {
                    loggedIn = true;
                    Console.WriteLine($"Welcome back {shopping.CurrentCustomer.Fname} {shopping.CurrentCustomer.Lname}");
                }
                else
                {
                    loggedIn = false;
                    Console.WriteLine("Invalid user name or password: Please try again!");
                }

            } while (!loggedIn);
        }

        /// <summary>
        /// View that displays the register menu
        /// </summary>
        /// <param name="shopping"></param>
        public static void registerMenu(ShoppingLogic shopping)
        {
            string fName, lName, uName, password;

            Console.WriteLine("Please enter your information below to create an account");
            Console.WriteLine("----------------------------------------------------------");
            Console.Write("Enter First Name: ");
            fName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            lName = Console.ReadLine();
            Console.Write("Enter User Name: ");
            uName = Console.ReadLine();
            Console.Write("Enter Password: ");
            password = Console.ReadLine();

            shopping.register(fName, lName, uName, password);
            Console.Clear();
            Console.WriteLine($"Account created! Welcome {fName} {lName}");
        }

        public static int storeListMenu(ShoppingLogic shopping)
        {
            int menuInt = 0;
            int userChoice = 0;
            do
            {
                Console.WriteLine("Select a store to shop from or logout!");
                Console.WriteLine("----------------------------------------------------------");

                List<Store> storeList = shopping.CurrentCustomer.StoreLocations;
                foreach (Store s in storeList)
                    Console.WriteLine($"{s.StoreId}: {s.Location}");
                Console.WriteLine("5: LOGOUT");
                userChoice = shopping.validateStoreChoice(Console.ReadLine());

                Console.Clear();
                if (userChoice == 0)
                    Console.WriteLine("Invalid choice: Please choose by number");
                else if (userChoice == storeList.Count + 1)
                {
                    Console.WriteLine("LOGGING OUT!");
                }
                else
                {
                    Console.WriteLine($"Welcom to {storeList.Find(x => x.StoreId == userChoice).Location}!");
                    menuInt = 1;
                }

            } while (userChoice == 0);
            return menuInt;
        }

        public static int storeMenu(ShoppingLogic shopping)
        {
            int userChoice;
            do
            {
                //Chose between start shopping, view previous orders, or logout
                Console.WriteLine("Please select an option");
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("1: Start Shopping");
                Console.WriteLine("2: View Previous Orders");
                Console.WriteLine("3: LOGOUT");
                userChoice = shopping.validateStoreMenuChoice(Console.ReadLine());

                switch (userChoice)
                {
                    case 0:
                        Console.WriteLine("Invalid choice: Please choose by number");
                        break;
                    case 1:
                        //show products
                        break;
                    case 2:
                        printOrders(shopping);
                        break;
                    case 3:
                        Console.WriteLine("LOGGING OUT!");
                        break;
                }
            } while (userChoice == 0 || userChoice == 2);
            Console.Clear();
            return userChoice;
        }

        /// <summary>
        /// View that displays previous orders
        /// </summary>
        /// <param name="orders"></param>
        public static void printOrders(ShoppingLogic shopping)
        {
            Console.WriteLine($"Previous orders from {shopping.CurrentStore.Location}");
            Console.WriteLine("----------------------------------------------------------");
            List<Order> orders = shopping.getListOfOrders();
            foreach (Order o in orders)
            {
                Console.WriteLine($"Order Number: {o.OrderId} - Total: ${o.TotalCost}");
                foreach (Product p in o.Products)
                    Console.WriteLine($"\t{p.ProductId}: {p.Name} - {p.Description} = ${p.Price}");
            }
        }

        public static void shoppingMenu(ShoppingLogic shopping)
        {
            List<Product> products = shopping.CurrentStore.Products;
            List<Product> cart;
            int userChoice = 0;
            do
            {
                int maxNum = 1;
                cart = shopping.CurrentCustomer.Cart;
                if (cart.Count != 0)
                {
                    Console.WriteLine("Current Items In Cart:");
                    foreach (Product c in cart)
                        Console.WriteLine($"\t${c.Price} : {c.Name}");
                    Console.WriteLine("Total:\t$\n");
                }
                Console.WriteLine($"Products in {shopping.CurrentStore.Location}");
                Console.WriteLine("Enter product number to add to cart or view cart, checkout, logout");
                Console.WriteLine("----------------------------------------------------------");
                foreach (Product p in products)
                {
                    Console.WriteLine($"{maxNum}: ${p.Price} -- {p.Name} - {p.Description}");
                    maxNum++;
                }
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"{maxNum}: Checkout");
                Console.WriteLine($"{++maxNum}: LOGOUT\n");
                userChoice = shopping.validateShoppingMenuChoice(Console.ReadLine(), maxNum);

                for (int i = 0; i <= maxNum; i++)
                {
                    if (userChoice == 0)
                    {
                        Console.WriteLine("Invalid choice: Please choose by number");
                        break;
                    }
                    else if (userChoice == maxNum)
                    {
                        Console.Clear();
                        Console.WriteLine("LOGGING OUT!");
                        break;
                    }
                    else if (userChoice == maxNum - 1)
                    {
                        Console.WriteLine("Checking out");
                        break;
                    }
                    else
                    {
                        Product p = products[userChoice - 1];
                        cart.Add(p);
                        Console.Clear();
                        Console.WriteLine($"{p.Name} added to cart\n");
                        break;
                    }
                }

                //Console.Clear();
            } while (userChoice == 0 || userChoice >= 1 && userChoice <= products.Count);
        }
    }
}
