using FinanceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceAPI.Data;

public class AppDataContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       optionsBuilder.UseSqlite("Data Source=finances.db");
    }
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
        // Categorias de ENTRADA
        new Category { Id = 1, Name = "Pagamento", Type = "entrada" },
        new Category { Id = 2, Name = "Recebidos", Type = "entrada" },
        new Category { Id = 3, Name = "Vale alimentação", Type = "entrada" },

        // Categorias de SAÍDA
        new Category { Id = 4, Name = "Necessidades", Type = "saida" },
        new Category { Id = 5, Name = "Contas", Type = "saida" },
        new Category { Id = 6, Name = "Mercado", Type = "saida" },
        new Category { Id = 7, Name = "Aluguel", Type = "saida" },
        new Category { Id = 8, Name = "Restaurante", Type = "saida" },
        new Category { Id = 9, Name = "Assinaturas", Type = "saida" },
        new Category { Id = 10, Name = "Educação", Type = "saida" }
    );
    }
}