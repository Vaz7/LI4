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
            // Configure composite primary key for Licitacao
            modelBuilder.Entity<Licitacao>()
                .HasKey(l => new { l.emailUtilizador, l.idLeilao });

            // Configure foreign key relationships
            modelBuilder.Entity<Licitacao>()
                .HasOne(l => l.utilizador)
                .WithMany(u => u.Licitacoes)
                .HasForeignKey(l => l.emailUtilizador)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Licitacao>()
                .HasOne(l => l.leilao)
                .WithMany(le => le.Licitacoes)
                .HasForeignKey(l => l.idLeilao);

            // Other configurations...
        }
    }

}
