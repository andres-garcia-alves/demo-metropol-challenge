using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Persona> Personas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Persona>().HasKey(p => p.Id);
        }
    }
}
