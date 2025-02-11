using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().HasKey(x => x.IdPerson);
            modelBuilder.Entity<Product>().HasKey(x => x.IdProduct);
            modelBuilder.Entity<Delivery>().HasKey(x => x.IdDelivery);
            modelBuilder.Entity<Supplier>().HasKey(x => x.IdSupplier);

        }
    }
}
