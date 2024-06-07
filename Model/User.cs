using System.Collections.Generic;

namespace WheelyGoodCars.Model
{
    internal class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Listing> Listings { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
            Listings = new List<Listing>();
        }

        public override string ToString()
        {
            return $"{Id}";
        }
    }
}