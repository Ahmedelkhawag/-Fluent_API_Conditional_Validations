using Microsoft.EntityFrameworkCore;

namespace Fluent_API_Conditional_Validations.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
    new Customer { Id = 1, Name = "John Doe", MembershipTier = "Gold" },
    new Customer { Id = 2, Name = "Jane Smith", MembershipTier = "Silver" },
    new Customer { Id = 3, Name = "Emily Johnson", MembershipTier = "Platinum" }
);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
