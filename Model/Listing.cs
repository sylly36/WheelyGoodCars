using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelyGoodCars.Model
{
    internal class Listing
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string LicensePlate { get; set; }
        public decimal Price { get; set; }
        public int Mileage { get; set; }
        public int? Seats { get; set; }
        public int? Doors { get; set; }
        public int? ProductionYear { get; set; }
        public int? Weight { get; set; }
        public string Color { get; set; }

        public Listing(string brand, string licensePlate, decimal price, int mileage, int? seats, int? doors, int? productionYear, int? weight, string color) 
        { 
            this.Brand = brand;
            this.LicensePlate = licensePlate;
            this.Price = price;
            this.Mileage = mileage;
            this.Seats = seats;
            this.Doors = doors;
            this.ProductionYear = productionYear;
            this.Weight = weight;
            this.Color = color;
        }
    }
}
