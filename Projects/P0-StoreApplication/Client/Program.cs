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
            MenuChoice mc = MenuChoice.MainMenu;
            ShoppingLogic shopping = new ShoppingLogic();
            do//loop until exit
            {
                switch(mc)
                {
                    case MenuChoice.MainMenu:
                        mc = mainMenu(shopping);
                        break;
                    case MenuChoice.LoginMenu:
                        mc = loginMenu(shopping);
                        break;
                    case MenuChoice.RegisterMenu:
                        mc = registerMenu(shopping);
                        break;
                    case MenuChoice.StoreListMenu:
                        mc = storeListMenu(shopping);
                        break;
                    case MenuChoice.StoreMenu:
                        mc = storeMenu(shopping);
                        break;
                    case MenuChoice.ShoppingMenu:
                        mc = shoppingMenu(shopping);
                        break;
                    case MenuChoice.OrderSuccsessMenu:
                        orderSuccessMenu(shopping);
                        exit = true;
                        break;
                }
            } while (!exit);
        }

        public static MenuChoice mainMenu(ShoppingLogic shopping)
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
                userChoice = shopping.validateMainMenuChoice(Console.ReadLine());

                if(userChoice == 0)
                    Console.WriteLine("Invalid choice: Please choose by number\n");
                if (userChoice == 1)
                    mc = MenuChoice.LoginMenu;
                if (userChoice == 2)
                    mc = MenuChoice.RegisterMenu;
                if(userChoice == 3)
                    shopping.exit();
            } while (userChoice == 0);
            Console.Clear();
            return mc;
        }

        /// <summary>
        /// View that displays the login menu
        /// </summary>
        /// <param name="shopping"></param>
        public static MenuChoice loginMenu(ShoppingLogic shopping)
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

            return MenuChoice.StoreListMenu;
        }

        /// <summary>
        /// View that displays the register menu
        /// </summary>
        /// <param name="shopping"></param>
        public static MenuChoice registerMenu(ShoppingLogic shopping)
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

            return MenuChoice.StoreListMenu;
        }

        public static MenuChoice storeListMenu(ShoppingLogic shopping)
        {
            int userChoice = 0;
            MenuChoice mc = MenuChoice.MainMenu;
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
                    Console.WriteLine($"Welcome to {storeList.Find(x => x.StoreId == userChoice).Location}!");
                    mc = MenuChoice.StoreMenu;
                }

            } while (userChoice == 0);
            return mc;
        }

        public static MenuChoice storeMenu(ShoppingLogic shopping)
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
                Console.WriteLine("3: LOGOUT");
                userChoice = shopping.validateStoreMenuChoice(Console.ReadLine());

                switch (userChoice)
                {
                    case 0:
                        Console.WriteLine("Invalid choice: Please choose by number");
                        break;
                    case 2:
                        printOrders(shopping);
                        break;
                    case 3:
                        Console.WriteLine("LOGGING OUT!");
                        mc = MenuChoice.MainMenu;
                        break;
                }
            } while (userChoice == 0 || userChoice == 2);
            Console.Clear();
            return mc;
        }

        /// <summary>
        /// Displays shopping menu
        /// </summary>
        /// <param name="shopping"></param>
        public static MenuChoice shoppingMenu(ShoppingLogic shopping)
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
                    printCart(cart);

                Console.WriteLine($"Products in {shopping.CurrentStore.Location}");
                Console.WriteLine("Enter product number to add to cart or view cart, checkout, logout");
                Console.WriteLine("----------------------------------------------------------");
                printProducts(products);
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"{maxNum}: Checkout");
                Console.WriteLine($"{++maxNum}: LOGOUT");
                userChoice = shopping.validateShoppingMenuChoice(Console.ReadLine(), maxNum);

                if (userChoice == 0)
                    Console.WriteLine("Invalid choice: Please choose by number");
                else if (userChoice == maxNum)
                {
                    Console.Clear();
                    Console.WriteLine("LOGGING OUT!");
                    mc = MenuChoice.MainMenu;
                }
                else if (userChoice == maxNum - 1)
                {
                    Console.WriteLine("ORDER SUCCESSFULL");
                    mc = MenuChoice.OrderSuccsessMenu;
                }
                else
                {
                    Product p = products[userChoice - 1];
                    Console.Clear();
                    if (shopping.addProductToCart(p))
                        Console.WriteLine($"{p.Name} added to cart\n");
                    else
                        Console.WriteLine($"{p.Name} cannot be added to cart due to $500 limit!\n");
                }
            } while (userChoice == 0 || userChoice >= 1 && userChoice <= products.Count);
            return mc;
        }

        public static void orderSuccessMenu(ShoppingLogic shopping)
        {
            Console.WriteLine("Testing");
        }

        /// <summary>
        /// Prints products in store
        /// </summary>
        /// <param name="products"></param>
        public static void printProducts(List<Product> products)
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
        /// <param name="cart"></param>
        public static void printCart(List<Product> cart)
        {
            double total = 0.0;
            Console.WriteLine("Current Items In Cart:");
            foreach (Product c in cart)
            {
                Console.WriteLine($"\t${c.Price} : {c.Name}");
                total += c.Price;
            }
            Console.WriteLine($"Total:\t${total}\n");
        }

        /// <summary>
        /// Prints customers previous orders
        /// </summary>
        /// <param name="orders"></param>
        public static void printOrders(ShoppingLogic shopping)
        {
            Console.Clear();
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
    }
}
