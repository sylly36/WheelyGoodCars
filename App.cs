using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelyGoodCars.Model;
using WheelyGoodCars.Data;

namespace WheelyGoodCars
{
    internal class App
    {
        bool isRunning = true;
        CarsAppContext context = new CarsAppContext();
        public static User? loggedInUser;

        internal void Run()
        {
            while (isRunning)
            {
                ShowLoginMenu();

                while (loggedInUser is User)
                {
                    string userChoice = Helpers.Choose("What do you want to do?", new string[] { "All Listings", "Add Listing", "Remove Listing", "Log Out", "Quit" });

                    switch (userChoice)
                    {
                        case "All Listings":
                            ShowAll();
                            break;
                        case "Add Listing":
                            AddListing();
                            break;
                        case "Remove Listing":
                            RemoveListing();
                            break;
                        case "Log Out":
                            loggedInUser = null;
                            break;
                        case "Quit":
                            isRunning = false;
                            break;
                    }
                }
            }
        }

        public void ShowLoginMenu()
        {
            Console.Clear();
            string userChoice = Helpers.Choose("What do you want to do?", new string[] { "Login", "Register", "Quit" });

            switch (userChoice)
            {
                case "Login":
                    Login();
                    break;
                case "Register":
                    Register();
                    break;
                case "Quit":
                    isRunning = false;
                    break;
            }
        }

        public void ShowAll()
        {

        }

        public void AddListing()
        {

        }

        public void RemoveListing() 
        { 
        
        }

        public void Login()
        {
            Console.Clear();

            string username = Helpers.AskNotEmpty("Wat is je gebruikersnaam?");
            string password = Helpers.AskPassword("Wat is je wachtwoord?");

            string hashedPassword = Helpers.HashPassword(password);

            User? user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == hashedPassword);

            if (user is User)
            {
                Console.Clear();
                loggedInUser = user;
                Console.WriteLine("Ingelogd!\n");
            }
        }

        public void Register() 
        {
            Console.Clear();
            bool correctUser = false;

            while (!correctUser)
            {
                string username = Helpers.AskNotEmpty("What is your username?");
                string password = Helpers.AskPassword("What is your password?");

                if (context.Users.FirstOrDefault(u => u.Username == username) is User)
                {
                    Console.WriteLine("Username already taken!");
                }
                else
                {
                    correctUser = true;
                    string hashedpasword = Helpers.HashPassword(password);
                    User user = new User(username, hashedpasword);

                    context.Users.Add(user);
                    context.SaveChanges();
                }

            }
        }
    }
}
