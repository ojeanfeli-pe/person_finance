using FinanceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceAPI.Data;

public class AppDataContext : DbContext{

    public DbSet<User> Users {get; set;}
    public DbSet<Category> Categories {get; set;}
    public DbSet<Transaction> Transactions {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       optionsBuilder.UseSqlite("Data Source=finances.db");
    }

   
}