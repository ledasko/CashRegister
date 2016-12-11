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

        #region Configuration
        public static void StartApp()
        {
            LoadRepositories();
            LoginMenu();
        }

        private static void LoadRepositories()
        {
            itemRepository = new ItemRepository();
            userRepository = new UserRepository();
            receiptRepository = new ReceiptRepository();
        }

        private static void ExitApp()
        {
            System.Environment.Exit(0);
        }
        #endregion

        #region Logged In
        public static void LoggedMenu()
        {
            Console.WriteLine("Cash register options.");
            if (loggedAdmin || loggedBasic)
                Console.WriteLine("You are currently logged in as " + currentlyLogged.Username + " (" + currentlyLogged.Type + ")");
            if (loggedAdmin)
                Console.WriteLine("1. Add items");
            Console.WriteLine("2. Print items");
            Console.WriteLine("3. New receipt");
            //Console.WriteLine("4. Print receipt details");
            Console.WriteLine("4. Receipt report");
            if (loggedAdmin)
                Console.WriteLine("6. Accout management");
            if (loggedAdmin || loggedBasic)
                Console.WriteLine("9. Logout");
            Console.WriteLine("0. Exit application");
            Console.Write("Select option by typing the number in front of it: ");
            LoggedActions();
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
                        GoToLoggedMenu();
                    }
                    else
                    {
                        Console.WriteLine("You are not authorized for this option.");
                    }
                    break;
                case 2:
                    ItemOutput();
                    GoToLoggedMenu();
                    break;
                case 3:
                    NewReceipt();
                    break;
                case 4:
                    ReceiptMenu();
                    break;
                case 0:
                    ExitApp();
                    break;
                case 6:
                    if (loggedAdmin)
                    {
                        throw new NotImplementedException();
                    }
                    else
                    {
                        Console.WriteLine("You are not authorized for this option.");
                    }
                    break;
                case 9:
                    if (loggedBasic || loggedAdmin)
                        Logout();
                    break;
                default:
                    GoToLoggedMenu();
                    break;
            }

            GoToLoggedMenu();
        }
        #endregion

        #region Login
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
        #endregion

        #region Items
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
        }

        private static void ItemOutput()
        {
            Console.WriteLine("Listing all items in database.");
            foreach (Item i in itemRepository.GetItemList())
            {
                Console.WriteLine(i.ToString());
            }
            Console.WriteLine();
        }

        private static void GoToLoggedMenu()
        {
            LoggedMenu();
        }
        #endregion

        #region Receipt
        private static void NewReceipt()
        {
            Console.Write("NEW RECEIPT - ");
            DateTime now = DateTime.Now;
            Console.WriteLine(now);

            bool done = false;
            List<Quantity> quantityOfItems = new List<Quantity>();

            while (!done)
            {
                ItemOutput();
                Console.Write("Choose an item to add to receipt: ");
                int option = int.Parse(Console.ReadLine());
                Item item = null;

                if ((item = itemRepository.GetById(option)) == null)
                {
                    Console.WriteLine("Item you have chosen does not exist.");
                }
                else
                {
                    Console.WriteLine("Select quantity of item: ");
                    float size = float.Parse(Console.ReadLine());
                    Console.WriteLine(item.Volume.Equals(Quantify.KG)
                        ? "You have chosen " + size + " kilograms."
                        : "You have chosen " + size + " pieces.");

                    quantityOfItems.Add(new Quantity(item, size));

                    Console.WriteLine("Currently adding: ");
                    foreach (Quantity q in quantityOfItems)
                    {
                        Console.WriteLine(q.ToString());
                    }

                    Console.WriteLine();
                    Console.WriteLine("1. Confirm receipt");
                    Console.WriteLine("2. More..");
                    Console.WriteLine("3. Cancel");
                    option = int.Parse(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            done = true;
                            receiptRepository.Add(new Receipt(receiptRepository.GetFollowingId(), now, quantityOfItems));
                            break;
                        case 2:
                            done = false;
                            break;
                        case 3:
                            done = true;
                            break;
                    }
                }
            }

            GoToLoggedMenu();
        }
        private static void ReceiptMenu()
        {
            Console.WriteLine("Receipt menu.");
            Console.WriteLine();
            Console.WriteLine("1. Receipt report by day");
            Console.WriteLine("2. Receipt report by item");
            Console.WriteLine("3. All receipts");
            Console.WriteLine("9. Back to the cash register menu");
            Console.WriteLine("0. Exit application");
            Console.Write("Select option by typing the number in front of it: ");

            ReceiptActions();
        }

        private static void ReceiptActions()
        {
            int option = int.Parse(Console.ReadLine());
            Console.WriteLine();

            switch (option)
            {
                case 1:
                    ReceiptByDay();
                    break;
                case 2:
                    ReceiptByItem();
                    break;
                case 3:
                    ReceiptAll();
                    break;
                case 9:
                    GoToLoggedMenu();
                    break;
                case 0:
                    ExitApp();
                    break;
            }
        }

        private static void ReceiptAll()
        {
            Console.WriteLine("Listing all receipts in database.");
            foreach (Receipt r in receiptRepository.GetReceiptList())
            {
                Console.WriteLine(r.ToString());
            }
            Console.WriteLine();
        }

        private static void ReceiptByDay()
        {
            throw new NotImplementedException();
        }

        private static void ReceiptByItem()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Logout
        private static void Logout()
        {
            if (loggedAdmin || loggedBasic || currentlyLogged != null)
            {
                Console.WriteLine("You have been successfully logged out.");
                currentlyLogged = null;
            }

            LoginMenu();
        }
        #endregion

        #region Admin
        private static void AdminInfo()
        {
            Console.WriteLine("Admin account information: ");
            Console.WriteLine("Username: admin");
            Console.WriteLine("Password: admin");
            Console.WriteLine();

            LoginMenu();
        }
        #endregion
    }
}
