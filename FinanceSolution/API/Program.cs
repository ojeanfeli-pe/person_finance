using FinanceAPI.Models;
using FinanceAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

// Endopints

// GET: users = Listar todos os users cadastrados
app.MapGet("/users", 
    ([FromServices] AppDataContext ctx) => 
    {
        if(ctx.Users.Any()){
            return Results.Ok(ctx.Users.ToList());
        }
        return Results.NotFound();
});

// Post: users/register = Registrar e salver um user
app.MapPost("/users/register",
    ([FromBody] User user, [FromServices] AppDataContext ctx) =>
    {
        // Verifica se o nome já existe
        if (ctx.Users.Any(u => u.Name == user.Name))
        {
            return Results.BadRequest("O usuário já existe.");
        }

        // Gera um código embaralhado (hash) da senha usando SHA256. Substitui a senha original pelo hash. Salva no banco com nome e senha já protegida.
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(user.Password ?? "");
        var hash = sha256.ComputeHash(bytes);
        user.Password = Convert.ToBase64String(hash);

        ctx.Users.Add(user);
        ctx.SaveChanges();

        return Results.Created("", user);
});

app.Run();
