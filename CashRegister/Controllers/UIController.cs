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

        public static void StartApp()
        {
            LoadRepositories();
            LoginMenu();
        }

        public static void LoginMenu()
        {
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("0. Exit application");
            Console.Write("Select option by typing the number in front of it: ");
            LoginActions();
        }

        public static void LoggedMenu()
        {
            Console.WriteLine("1. Add items");
            Console.WriteLine("2. Print items");
            Console.WriteLine("3. New receipt");
            Console.WriteLine("4. Print receipt details");
            Console.WriteLine("5. Receipt report");
            Console.WriteLine("0. Exit application");
            Console.Write("Select option by typing the number in front of it: ");
            LoggedInActions();
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

            switch (option)
            {
                case 1:
                    LoginUser();
                    break;
                case 2:
                    RegisterUser();
                    break;
                case 0:
                    ExitApp();
                    break;
            }

        }

        private static void LoggedInActions()
        {
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    ItemInput();
                    break;
                case 2:
                    ItemOutput();
                    break;
                case 0:
                    ExitApp();
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

            User user = userRepository.GetByUsername(username);

            if (user != null && user.Password.Equals(password))
            {
                Console.WriteLine("You have successfully logged in.");
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
        }

        private static void ItemOutput()
        {

        }

        private static void ExitApp()
        {
            System.Environment.Exit(0);
        }
    }
}
