using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using WebApplication1.Controllers;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace WebApplication1.Context
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

        public System.Data.Entity.DbSet<WebApplication1.Controllers.OtoparkArac> OtoparkAracLists { get; set; }
    }
}
