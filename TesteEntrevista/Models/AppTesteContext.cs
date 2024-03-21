using Microsoft.EntityFrameworkCore;

namespace TesteEntrevista.Models
{
    public class AppTesteContext : DbContext
    {
        public AppTesteContext(DbContextOptions<AppTesteContext> options) : base(options)
        {   
        }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Venda> Venda { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasKey(x => x.idCliente);

            modelBuilder.Entity<Produto>()
                .HasKey(x => x.idProduto);

            modelBuilder.Entity<Venda>()
                .HasKey(x => x.idVenda);

            modelBuilder.Entity<Venda>()
                .HasOne(x => x.Cliente)
                .WithMany(x => x.Vendas)
                .HasForeignKey(x => x.idCliente);

            modelBuilder.Entity<Venda>()
                .HasOne(x => x.Produto)
                .WithMany(x => x.Vendas)
                .HasForeignKey(x => x.idProduto);
        }
    }
}
