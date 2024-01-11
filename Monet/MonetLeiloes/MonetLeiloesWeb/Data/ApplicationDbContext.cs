using MonetLeiloesWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace MonetLeiloesWeb.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Morada> Moradas { get; set; }
        public DbSet<Utilizador> Utilizador { get; set;}
        public DbSet<Leilao> Leiloes { get; set; }
        public DbSet<Licitacao> Licitacoes { get; set; }
        public DbSet<Quadro> Quadros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Leilao>().HasKey(m => new { m.Id});
            modelBuilder.Entity<Licitacao>().HasKey(m => new { m.emailUtilizador,m.idLeilao });
            modelBuilder.Entity<Licitacao>()
                .HasOne(l => l.utilizador)
                .WithMany()
                .HasForeignKey(l => l.emailUtilizador)
                .OnDelete(DeleteBehavior.Restrict); // You can use DeleteBehavior.Cascade if appropriate

            modelBuilder.Entity<Licitacao>()
                .HasOne(l => l.leilao)
                .WithMany()
                .HasForeignKey(l => l.idLeilao)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Morada>().HasKey(m => new { m.Id});
            modelBuilder.Entity<Quadro>().HasKey(m => new { m.Id});
            modelBuilder.Entity<Utilizador>().HasKey(m => new { m.email});
        }
    }

}
