using System.Data.Entity;

namespace DatabaseCars
{
    public class CarRegistryContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Owner> Owners { get; set; }

        public CarRegistryContext() : base("master") { }
    }
}