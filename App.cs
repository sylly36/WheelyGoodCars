using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelyGoodCars.Model;
using WheelyGoodCars.Data;
using System.Runtime.ConstrainedExecution;

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
                    Console.Clear();
                    string userChoice = Helpers.Choose("\nWhat do you want to do?", new string[] { "Show My Listings", "All Listings", "Add Listing", "Edit Listing", "Remove Listing", "Log Out", "Quit" });

                    switch (userChoice)
                    {
                        case "Show My Listings":
                            ShowMyListings();
                            break;
                        case "All Listings":
                            ShowAll();
                            break;
                        case "Add Listing":
                            AddListing();
                            break;
                        case "Edit Listing":
                            EditListing();
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
            string userChoice = Helpers.Choose("What do you want to do?", new string[] { "Login", "Register", "All Listings", "Quit" });

            switch (userChoice)
            {
                case "Login":
                    Login();
                    break;
                case "Register":
                    Register();
                    break;
                case "All Listings":
                    ShowAll();
                    break;
                case "Quit":
                    isRunning = false;
                    break;
            }
        }
        public void ShowMyListings()
        {
            Console.Clear();
            Console.WriteLine("Your listings:");
            List<Listing> listings = context.Listings.ToList();
            string userListingId = loggedInUser.Id.ToString();

            foreach (Listing listing in listings)
            {
                string listingUser = listing.UserListing.ToString();

                if (listingUser == userListingId)
                {
                    Console.WriteLine(listing);
                }            
            }
            Helpers.Wait();
        }

        public void ShowAll()
        {
            Console.Clear();
            Console.WriteLine("All Listings:");
            List<Listing> listings = context.Listings.ToList();

            foreach (Listing listing in listings)
            {
                Console.WriteLine(listing);
            }
            Helpers.Wait();
        }

        public void AddListing()
        {
            Console.Clear();
            Console.WriteLine("Add New Listing:");

            User? user = loggedInUser;

            string licensePlate = Helpers.AskNotEmpty("Whats the license plate?");
            bool IsNew = VerifyLicensePlate(licensePlate);

            if (IsNew == true) 
            { 
                string brand = Helpers.AskNotEmpty("Whats the brand name?");
                decimal price =Helpers.AskForDecimal("Whats the price?");
                int mileage = Helpers.AskForInt("Whats the mileage?");
                string color = Helpers.AskNotEmpty("Whats the color?");

                int? seats = null;
                int? doors = null;
                string wantsSeatsAndDoors = Helpers.AskNotEmpty("Do you want to add the amount of seats and doors? Yes/No");
                if (wantsSeatsAndDoors == "yes")
                {
                    seats = Helpers.AskForInt("How many seats are there?");
                    doors = Helpers.AskForInt("How many doors are there?");
                }

                int? productionYear = null;
                string wantProductionYear = Helpers.AskNotEmpty("Do you want to add the production year? Yes/No");
                if (wantProductionYear == "Yes")
                {
                    productionYear = Helpers.AskForInt("Whats the production year?");
                }

                int? weight = null;
                string wantWeight = Helpers.AskNotEmpty("Do you want to add the weight? Yes/No");
                if (wantWeight == "Yes")
                {
                    weight = Helpers.AskForInt("What the weight?");
                }

                Listing newListing = new Listing(brand, licensePlate, price, mileage, seats, doors, productionYear, weight, color);
                newListing.UserListing = user;
                context.Listings.Add(newListing);

                context.SaveChanges();

                Console.Clear();
                Console.WriteLine("Listing has been made.");
            }
            else
            {
                Console.WriteLine("Car already exists!\n");          
            }

            Helpers.Wait();
        }

        public bool VerifyLicensePlate(string licensePlate)
        {
            List<Listing> listings = context.Listings.ToList();
            foreach (Listing listing in listings)
            {
                if (listing.LicensePlate == licensePlate) 
                {
                    return false;
                }
            }
            return true;
        }

        public void RemoveListing() 
        { 
        
        }

        public void EditListing()
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
                Helpers.Wait();
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
