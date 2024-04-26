using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LittlePetAPI.Models;
using System.Drawing;

namespace LittlePetAPI.Data
{
    public class MyContext : IdentityDbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<TipoProduto> TiposProdutos { get; set; }
        public DbSet<VendaProduto> VendasProdutos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<AnimalProduto> AnimalProdutos { get; set; }
        public DbSet<FormaPagamento> FormasPagamentos { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Pet>().ToTable("Pets");
            modelBuilder.Entity<Venda>().ToTable("Vendas");
            modelBuilder.Entity<Produto>().ToTable("Produtos");
            modelBuilder.Entity<Servico>().ToTable("Serviços");
            modelBuilder.Entity<TipoProduto>().ToTable("TiposProdutos");
            modelBuilder.Entity<VendaProduto>().ToTable("VendasProdutos");
            modelBuilder.Entity<Fornecedor>().ToTable("Fornecedores");
            modelBuilder.Entity<AnimalProduto>().ToTable("AnimalProdutos");
            modelBuilder.Entity<FormaPagamento>().ToTable("FormasPagamentos");
            modelBuilder.Entity<Agendamento>().ToTable("Agendamentos");
        }
    }
}
