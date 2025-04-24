using FinanceAPI.Models;
using FinanceAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);
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
        if(user.Name == null){
            ctx.Users.Add(user);
            ctx.SaveChanges();
            return Results.Created("", user);
        }
    return Results.BadRequest("O usuário ja existe");
});

app.Run();
