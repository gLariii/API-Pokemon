using API_Pokemon.Entidades;
using Microsoft.EntityFrameworkCore;

namespace API_Pokemon
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 

        }
        public DbSet<Pokemon> Pokemon { get; set; }
        public DbSet<Tipo> Tipo { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.Tipo)
                .WithMany()
                .HasForeignKey(p => p.IdTipo);
        }

    }
}


