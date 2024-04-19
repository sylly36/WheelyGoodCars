using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelyGoodCars.Model;
using WheelyGoodCars.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Runtime.ConstrainedExecution;
using iTextSharp.text.pdf;

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
            Console.WriteLine("\n");
            string userChoice = Helpers.Choose("What do you want to do?", new string[] { "Export data", "Go back" });

            switch (userChoice)
            {
                case "Export data":
                    ExportData();
                    break;
                case "Go back":
                    isRunning = false;
                    break;
            }
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

        public void ExportData()
        {
            Document document = new Document();

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string pdfFilePath = Path.Combine(desktopPath, "MyListings.pdf");
            PdfWriter.GetInstance(document, new FileStream(pdfFilePath, FileMode.Create));

            document.Open();

            List<Listing> listings = context.Listings.ToList();
            string userListingId = loggedInUser.Id.ToString();

            foreach (Listing listing in listings)
            {
                string listingUser = listing.UserListing.ToString();

                if (listingUser == userListingId)
                {
                    document.Add(new Paragraph($"-----------------------------------------------"));
                    document.Add(new Paragraph($"{listing.Brand}        Kentekenplaat:{listing.LicensePlate}"));
                    document.Add(new Paragraph($"               Prijs:{listing.Price}\n"));
                    document.Add(new Paragraph($"               Gemaakt in:{listing.ProductionYear}"));
                    document.Add(new Paragraph($" "));
                    document.Add(new Paragraph($"Kleur:{listing.Color}      Mileage:{listing.Mileage}"));
                    if (listing.Seats != null || listing.Doors != null)
                    {
                        document.Add(new Paragraph($"Deuren:{listing.Doors}         Zitplekken:{listing.Seats}"));
                    }
                }
                
            }
            document.Add(new Paragraph($"-----------------------------------------------"));

            document.Close();

            Console.WriteLine("Data Exported: MyListings.pdf created on desktop");
            Helpers.Wait();
        }

        public void AddListing()
        {
            Console.Clear();
            Console.WriteLine("Add New Listing:");

            User? user = loggedInUser;

            Console.WriteLine("____________ 0%\n");
            string licensePlate = Helpers.AskNotEmpty("Whats the license plate?");
            bool IsNew = VerifyLicensePlate(licensePlate);

            if (IsNew == true) 
            {
                Console.Clear();
                Console.WriteLine("█___________ 9%\n");
                string brand = Helpers.AskNotEmpty("Whats the brand name?");

                Console.Clear();
                Console.WriteLine("██__________ 18%\n");
                decimal price =Helpers.AskForDecimal("Whats the price?");

                Console.Clear();
                Console.WriteLine("███_________ 27%\n");
                int mileage = Helpers.AskForInt("Whats the mileage?");

                Console.Clear();
                Console.WriteLine("████________ 36%\n");
                string color = Helpers.AskNotEmpty("Whats the color?");

                int? seats = null;
                int? doors = null;

                Console.Clear();
                Console.WriteLine("█████_______ 45%\n");
                string wantsSeatsAndDoors = Helpers.AskNotEmpty("Do you want to add the amount of seats and doors? Yes/No");
                if (wantsSeatsAndDoors == "yes")
                {
                    Console.Clear();
                    Console.WriteLine("██████______ 54%\n");
                    seats = Helpers.AskForInt("How many seats are there?");

                    Console.Clear();
                    Console.WriteLine("███████_____ 63%\n");
                    doors = Helpers.AskForInt("How many doors are there?");
                }

                int? productionYear = null;

                Console.Clear();
                Console.WriteLine("████████____ 72%\n");
                string wantProductionYear = Helpers.AskNotEmpty("Do you want to add the production year? Yes/No");

                if (wantProductionYear == "Yes")
                {
                    Console.Clear();
                    Console.WriteLine("█████████___ 81%\n");
                    productionYear = Helpers.AskForInt("Whats the production year?");
                }

                int? weight = null;

                Console.Clear();
                Console.WriteLine("██████████__ 90%\n");
                string wantWeight = Helpers.AskNotEmpty("Do you want to add the weight? Yes/No");

                if (wantWeight == "Yes")
                {
                    Console.Clear();
                    Console.WriteLine("███████████_ 99%\n");
                    weight = Helpers.AskForInt("What the weight?");
                }

                Listing newListing = new Listing(brand, licensePlate, price, mileage, seats, doors, productionYear, weight, color);
                newListing.UserListing = user;
                context.Listings.Add(newListing);

                context.SaveChanges();

                Console.Clear();
                Console.WriteLine("████████████ 100%\n");
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
            Listing selectedListing = SelectListing();

            string userSure = Helpers.AskNotEmpty($"Are you sure you want to delete listing with id {selectedListing.Id}? yes/no");
            if (userSure == "yes") 
            {
                context.Listings.Remove(selectedListing);
                context.SaveChanges();

                Console.WriteLine($"Listing with id {selectedListing.Id} has been deleted.");
                Helpers.Wait();
            }
        }

        public Listing SelectListing()
        {
            ShowMyListings();

            int listingId;
            Listing? selectedListing = null;
            Listing? listing = null;

            do
            {
                listingId = Helpers.AskForInt("\nEnter the id of the listing you wish to remove:");
                selectedListing = context.Listings.Find(listingId);
                if (selectedListing.UserListing == loggedInUser)
                {
                    listing = selectedListing;
                }
            }
            while (listing  == null);

            return listing;
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
