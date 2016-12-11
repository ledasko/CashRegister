using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CashRegister.DAL;
using CashRegister.Model;
using CashRegister.Accessories;

namespace CashRegister.Controllers
{
    class UIController
    {
        static ItemRepository itemRepository;
        static UserRepository userRepository;
        static ReceiptRepository receiptRepository;

        static User currentlyLogged;

        static bool loggedBasic = false;
        static bool loggedAdmin = false;

        public static void StartApp()
        {
            LoadRepositories();
            LoginMenu();
        }

        public static void LoginMenu()
        {
            Console.WriteLine("LOGIN MENU");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Admin information");
            Console.WriteLine("0. Exit application");
            Console.Write("Select option by typing the number in front of it: ");
            LoginActions();
        }

        public static void LoggedMenu()
        {
            Console.WriteLine("Cash register options.");
            if (loggedAdmin || loggedBasic)
                Console.WriteLine("You are currently logged in as " + currentlyLogged.Username + " (" + currentlyLogged.Type + ")");
            if (loggedAdmin)
                Console.WriteLine("1. Add items");
            Console.WriteLine("2. Print items");
            Console.WriteLine("3. New receipt");
            Console.WriteLine("4. Print receipt details");
            Console.WriteLine("5. Receipt report");
            if (loggedAdmin)
                Console.WriteLine("6. Accout management");
            Console.WriteLine("0. Exit application");
            Console.Write("Select option by typing the number in front of it: ");
            LoggedActions();
        }

        private static void LoadRepositories()
        {
            itemRepository = new ItemRepository();
            userRepository = new UserRepository();
            receiptRepository = new ReceiptRepository();
        }

        private static void LoginActions()
        {
            int option = int.Parse(Console.ReadLine());
            Console.WriteLine();

            switch (option)
            {
                case 1:
                    LoginUser();
                    break;
                case 2:
                    RegisterUser();
                    break;
                case 3:
                    AdminInfo();
                    break;
                case 0:
                    ExitApp();
                    break;
                default:
                    LoginMenu();
                    break;
            }
        }

        private static void LoggedActions()
        {
            int option = int.Parse(Console.ReadLine());
            Console.WriteLine();

            switch (option)
            {
                case 1:
                    if (loggedAdmin)
                    {
                        ItemInput();
                    }
                    else
                    {
                        Console.WriteLine("You are not authorized for this option.");
                    }
                    break;
                case 2:
                    ItemOutput();
                    break;
                case 0:
                    ExitApp();
                    break;
                case 6:
                    if (loggedAdmin)
                    {

                    }
                    else
                    {
                        Console.WriteLine("You are not authorized for this option.");
                    }
                    break;
                default:
                    LoggedMenu();
                    break;
            }

            LoggedMenu();
        }

        private static void RegisterUser()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            User user = new User(userRepository.GetFollowingId(), username, password, UserType.BASIC);
            userRepository.Add(user);

            LoginMenu();
        }

        private static void LoginUser()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            currentlyLogged = userRepository.GetByUsername(username);

            if (currentlyLogged != null && currentlyLogged.Password.Equals(password))
            {
                Console.WriteLine("You have successfully logged in.");
                if (currentlyLogged.Type.Equals(UserType.ADMIN))
                {
                    loggedAdmin = true;
                }
                else if (currentlyLogged.Type.Equals(UserType.BASIC))
                {
                    loggedBasic = true;
                }
                Console.WriteLine();

                LoggedMenu();
            }
            else
            {
                Console.WriteLine("Unsuccessful login.");
                LoginMenu();
            }
        }

        private static void ItemInput()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Price: ");
            float price = float.Parse(Console.ReadLine());
            Console.Write("Tax: ");
            int tax = int.Parse(Console.ReadLine());
            Console.WriteLine("1 - Kilograms");
            Console.WriteLine("2 - Pieces");
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Item itemKG = new Item(itemRepository.GetFollowingId(), name, price, tax, Quantify.KG);
                    itemRepository.Add(itemKG);
                    break;
                case 2:
                    Item itemN = new Item(itemRepository.GetFollowingId(), name, price, tax, Quantify.N);
                    itemRepository.Add(itemN);
                    break;
            }

            LoggedMenu();
        }

        private static void ItemOutput()
        {
            Console.WriteLine("Listing all items in database.");
            foreach (Item i in itemRepository.GetItemList())
            {
                Console.WriteLine(i.ToString());
            }
            Console.WriteLine();
            LoggedMenu();
        }

        private static void AdminInfo()
        {
            Console.WriteLine("Admin account information: ");
            Console.WriteLine("Username: admin");
            Console.WriteLine("Password: admin");
            Console.WriteLine();

            LoginMenu();
        }

        private static void ExitApp()
        {
            System.Environment.Exit(0);
        }
    }
}
