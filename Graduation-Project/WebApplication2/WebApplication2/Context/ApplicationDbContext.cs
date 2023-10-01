using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Domain.User> Users { get; set; }
        public DbSet<Domain.Category> Categories { get; set; }
        public DbSet<Domain.Product> Products { get; set; }
        public DbSet<Domain.Cart> Carts { get; set; }
        public DbSet<Domain.CartItem> CartItems { get; set; }
    }
}