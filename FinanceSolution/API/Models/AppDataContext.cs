using System;
using Microsoft.EntityFrameworkCore;

public class AppDataContext : DbContext{


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       optionsBuilder.UseSqlite("Data Source=finances.db");
    }

   
}