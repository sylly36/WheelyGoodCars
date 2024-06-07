using Microsoft.EntityFrameworkCore;
using WheelyGoodCars.Model;

namespace WheelyGoodCars.Data
{
    internal class CarsAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Listing> Listings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    "server=localhost;" +
                    "port=3306;" +
                    "user=c_sharp_dev;" +
                    "password=c_sharp_dev;" +
                    "database=WheelyGoodCars;"
                    , Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.17-mariadb")
                );
            }
        }

        public void Seeder()
        {
            string hashedPassword = Helpers.HashPassword("wachtwoord");

            User a1 = new User("frank", hashedPassword);
            User a2 = new User("admin", hashedPassword);
            User a3 = new User("sylvester", hashedPassword);
            User a4 = new User("jesse", hashedPassword);
            User a5 = new User("zakaria", hashedPassword);

            Users.Add(a1);
            Users.Add(a2);
            Users.Add(a3);
            Users.Add(a4);
            Users.Add(a5);

            Listing b1 = new Listing("Volvo", "AB-1234", 15000, 9000, null, 5, 2006, 100, "green");
            Listing b2 = new Listing("Volkswagen", "AB-123-CD", 3, 1272, 4, null, 2018, 112, "green");
            Listing b3 = new Listing("Toyota", "12-AB-34", 35000, 19811, null, null, null, 109, "geel");
            Listing b4 = new Listing("Ciat", "123-AB-4", 2000, 771, null, 5, 15, 89, "blauw");
            Listing b5 = new Listing("Skoda", "AB-12-34", 1000, 4, 4, 5, null, 210, "paars");

            Listings.Add(b1);
            Listings.Add(b2); 
            Listings.Add(b3);
            Listings.Add(b4);
            Listings.Add(b5);

            SaveChanges();
        }
    }
}