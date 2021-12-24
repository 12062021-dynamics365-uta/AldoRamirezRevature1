using System;
using System.Collections.Generic;
using Domain;
using Model;
using Storage;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuChoice mc = MenuChoice.MainMenu;
            Mapper mapper = new Mapper();
            DataBaseAccess dbAccess = new DataBaseAccess(mapper);
            ShoppingLogic shopping = new ShoppingLogic(dbAccess);

            do//loop until exit
            {
                switch(mc)
                {
                    case MenuChoice.MainMenu:
                        mc = MainMenu(shopping);
                        break;
                    case MenuChoice.LoginMenu:
                        mc = LoginMenu(shopping);
                        break;
                    case MenuChoice.RegisterMenu:
                        mc = RegisterMenu(shopping);
                        break;
                    case MenuChoice.StoreListMenu:
                        mc = StoreListMenu(shopping);
                        break;
                    case MenuChoice.StoreMenu:
                        mc = StoreMenu(shopping);
                        break;
                    case MenuChoice.ShoppingMenu:
                        mc = ShoppingMenu(shopping);
                        break;
                    case MenuChoice.OrderSuccsessMenu:
                        mc = OrderSuccessMenu(shopping);
                        break;
                }
            } while (mc != MenuChoice.Exit);

            //Close database connection
            shopping.Exit();
        }

        /// <summary>
        /// Shows the user the Main Menu
        /// User can chose between login, register or exiting the program
        /// </summary>
        /// <param name="shopping">ShoppingLogic Object</param>
        /// <returns>MenuChoice</returns>
        public static MenuChoice MainMenu(ShoppingLogic shopping)
        {
            int userChoice = 0;
            MenuChoice mc = MenuChoice.MainMenu;
            do
            {
                Console.WriteLine("Hello, welcome to virtual shopping!");
                Console.WriteLine("Please select an option");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("1: Login");
                Console.WriteLine("2: Register");
                Console.WriteLine("3: Exit");
                userChoice = shopping.ValidateMainMenuChoice(Console.ReadLine());

                switch(userChoice)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Invalid choice: Please choose by number\n");
                        break;
                    case 1:
                        mc = MenuChoice.LoginMenu;
                        break;
                    case 2:
                        mc = MenuChoice.RegisterMenu;
                        break;
                    case 3:
                        mc = MenuChoice.Exit;
                        break;
                }
            } while (userChoice == 0);
            Console.Clear();
            return mc;
        }

        /// <summary>
        /// Shows the user the Login Menu
        /// </summary>
        /// <param name="shopping">ShoppingLogic Object</param>
        /// <returns>MenuChoice</returns>
        public static MenuChoice LoginMenu(ShoppingLogic shopping)
        {
            bool loggedIn;
            do//loop until logged in
            {
                Console.WriteLine("Please enter username and password to login");
                Console.WriteLine("----------------------------------------------------------");
                Console.Write("Enter User Name: ");
                string userName = Console.ReadLine();
                Console.Write("Enter Password: ");
                string password = Console.ReadLine();

                Console.Clear();
                if (shopping.Login(userName, password))
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
            return MenuChoice.StoreListMenu;
        }

        /// <summary>
        /// Shows the user the Register Menu
        /// </summary>
        /// <param name="shopping">ShoppingLogic Object</param>
        /// <returns>MenuChoice</returns>
        public static MenuChoice RegisterMenu(ShoppingLogic shopping)
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

            shopping.Register(fName, lName, uName, password);
            Console.Clear();
            Console.WriteLine($"Account created! Welcome {fName} {lName}");

            return MenuChoice.StoreListMenu;
        }

        /// <summary>
        /// Shows the user the Store List Menu
        /// User can choose between stores or to logout
        /// </summary>
        /// <param name="shopping">ShoppingLogic Object</param>
        /// <returns>MenuChoice</returns>
        public static MenuChoice StoreListMenu(ShoppingLogic shopping)
        {
            int userChoice = 0;
            MenuChoice mc = MenuChoice.MainMenu;
            do
            {
                Console.WriteLine("Select a store to shop from or logout!");
                Console.WriteLine("----------------------------------------------------------");

                List<Store> storeList = shopping.CurrentCustomer.StoreLocations;
                foreach (Store s in storeList)
                    Console.WriteLine($"{s.StoreId}: {s.Name}");
                Console.WriteLine("5: LOGOUT");
                userChoice = shopping.ValidateStoreListMenuChoice(Console.ReadLine());

                Console.Clear();
                if (userChoice == 0)
                    Console.WriteLine("Invalid choice: Please choose by number");
                else if (userChoice == storeList.Count + 1)
                {
                    Console.WriteLine("LOGGING OUT!");
                }
                else
                {
                    Console.WriteLine($"Welcome to {storeList.Find(x => x.StoreId == userChoice).Name}!");
                    mc = MenuChoice.StoreMenu;
                }

            } while (userChoice == 0);
            return mc;
        }

        /// <summary>
        /// Shows the user the Store Menu
        /// User can choose between start shopping, view previous orders, changing store, or logging out
        /// </summary>
        /// <param name="shopping">ShoppingLogic Object</param>
        /// <returns>MenuChoice</returns>
        public static MenuChoice StoreMenu(ShoppingLogic shopping)
        {
            int userChoice;
            MenuChoice mc = MenuChoice.ShoppingMenu;
            do
            {
                //Chose between start shopping, view previous orders, or logout
                Console.WriteLine("Please select an option");
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("1: Start Shopping");
                Console.WriteLine("2: View Previous Orders");
                Console.WriteLine("3: Change Stores");
                Console.WriteLine("4: LOGOUT");
                userChoice = shopping.ValidateStoreMenuChoice(Console.ReadLine());

                switch (userChoice)
                {
                    case 0:
                        Console.WriteLine("Invalid choice: Please choose by number");
                        break;
                    case 2:
                        PrintOrders(shopping);
                        break;
                    case 3:
                        mc = MenuChoice.StoreListMenu;
                        break;
                    case 4:
                        Console.WriteLine("LOGGING OUT!");
                        mc = MenuChoice.MainMenu;
                        break;
                }
            } while (userChoice == 0 || userChoice == 2);
            Console.Clear();
            return mc;
        }

        /// <summary>
        /// Shows the user the Shopping Menu
        /// User can choose between adding item to cart, checking out, or logging out
        /// </summary>
        /// <param name="shopping">ShoppingLogic Object</param>
        /// <returns>MenuChoice</returns>
        public static MenuChoice ShoppingMenu(ShoppingLogic shopping)
        {
            List<Product> products = shopping.CurrentStore.Products;
            List<Product> cart;
            MenuChoice mc = MenuChoice.ShoppingMenu;
            int userChoice = 0;
            do
            {
                int maxNum = products.Count + 1;
                cart = shopping.CurrentCustomer.Cart;

                if (cart.Count != 0)
                    PrintCart(cart);

                Console.WriteLine($"Products in {shopping.CurrentStore.Name}");
                Console.WriteLine("Enter product number to add to cart or view cart, checkout, logout");
                Console.WriteLine("----------------------------------------------------------");
                PrintProduct(products);
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"{maxNum}: Checkout");
                Console.WriteLine($"{++maxNum}: Cancel");
                userChoice = shopping.ValidateShoppingMenuChoice(Console.ReadLine(), maxNum);

                if (userChoice == 0)
                    Console.WriteLine("Invalid choice: Please choose by number");
                else if (userChoice == maxNum)
                {
                    Console.Clear();
                    Console.WriteLine("Canceling Shopping!");
                    shopping.CancelOrder();
                    mc = MenuChoice.StoreMenu;
                }
                else if (userChoice == maxNum - 1)
                {
                    Console.Clear();
                    mc = MenuChoice.OrderSuccsessMenu;
                    shopping.Checkout();
                }
                else
                {
                    Product p = products[userChoice - 1];
                    Console.Clear();
                    if (shopping.AddProductToCart(p))
                        Console.WriteLine($"{p.Name} added to cart\n");
                    else
                        Console.WriteLine($"{p.Name} cannot be added to cart due to $500 limit!\n");
                }
            } while (userChoice == 0 || userChoice >= 1 && userChoice <= products.Count);
            return mc;
        }

        /// <summary>
        /// Shows the user if the order was successfull
        /// </summary>
        /// <param name="shopping">ShoppingLogic Object</param>
        /// <returns>MenuChoice</returns>
        public static MenuChoice OrderSuccessMenu(ShoppingLogic shopping)
        {

            Console.Clear();
            Console.WriteLine($"ORDER SUCCESSFULL");
            return MenuChoice.StoreMenu;
        }

        /// <summary>
        /// Prints products in store
        /// </summary>
        /// <param name="products">ShoppingLogic Object</param>
        public static void PrintProduct(List<Product> products)
        {
            int index = 1;
            foreach (Product p in products)
            {
                Console.WriteLine($"{index}: ${p.Price} -- {p.Name} - {p.Description}");
                index++;
            }
        }

        /// <summary>
        /// Prints customers cart
        /// </summary>
        /// <param name="cart">List of Product</param>
        public static void PrintCart(List<Product> cart)
        {
            decimal total = 0;
            Console.WriteLine("Current Items In Cart:");
            foreach (Product c in cart)
            {
                Console.WriteLine($"\t${c.Price} : {c.Name}");
                total += c.Price;
            }
            Console.WriteLine($"Total:\t${Math.Round(total, 2)}\n");
        }

        /// <summary>
        /// Prints customers previous orders
        /// </summary>
        /// <param name="orders">ShoppingLogic Object</param>
        public static void PrintOrders(ShoppingLogic shopping)
        {
            Console.Clear();
            Console.WriteLine($"Previous orders from {shopping.CurrentStore.Name}");
            Console.WriteLine("----------------------------------------------------------");
            List<Order> orders = shopping.GetListOfOrders();
            int i = 1, j = 1;
            foreach (Order o in orders)
            {
                Console.WriteLine($"Order Number: {i++} - Total: ${o.TotalCost}");
                foreach (Product p in o.Products)
                {
                    Console.WriteLine($"\t{j++}: {p.Name} - {p.Description} = ${p.Price}");
                }
                j = 1;
            }
            Console.WriteLine("----------------------------------------------------------\n");
        }
    }
}