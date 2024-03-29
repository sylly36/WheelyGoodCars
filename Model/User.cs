﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.Username = username;
            this.Password = password;
            this.Listings = new List<Listing>();
        }

        public override string ToString()
        {
            return $"{Id}";
        }
    }
}
