using FinanceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceAPI.Data;

public class AppDataContext : DbContext
{


    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relacionamento entre Transação e Categoria (N:1)
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Category)
            .WithMany(c => c.Transactions)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Cascade); // Se preferir não excluir em cascata, use .Restrict

        // Dados iniciais
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Necessidades" },
            new Category { Id = 2, Name = "Contas" },
            new Category { Id = 3, Name = "Mercado" },
            new Category { Id = 4, Name = "Aluguel" },
            new Category { Id = 5, Name = "Restaurante" },
            new Category { Id = 6, Name = "Assinaturas" }
        );
    }
}