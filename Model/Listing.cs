using System;

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
        public string Status { get; set; }
        public int UserId { get; set; }
        public User UserListing { get; set; }

        public Listing(string brand, string licensePlate, decimal price, int mileage, int? seats, int? doors, int? productionYear, int? weight, string color, string status)
        {
            Brand = brand;
            LicensePlate = licensePlate;
            Price = price;
            Mileage = mileage;
            Seats = seats;
            Doors = doors;
            ProductionYear = productionYear;
            Weight = weight;
            Color = color;
            Status = status;
        }

        public override string ToString()
        {
            if (ProductionYear != null)
            {
                return $"{Id} | {Brand} ({LicensePlate}) in {Color}, Cost: {Price}, Mileage: {Mileage}, Made in: {ProductionYear}";
            }
            return $"{Id} | {Brand} ({LicensePlate}) in {Color}, Cost: {Price}, Mileage: {Mileage}";
        }
    }
}