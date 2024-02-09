using Microsoft.EntityFrameworkCore;
using WheelyGoodCars.Model;

namespace WheelyGoodCars.Data
{
    internal class CarsAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }

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
    }
}