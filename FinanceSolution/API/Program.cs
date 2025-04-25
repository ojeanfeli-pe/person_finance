using FinanceAPI.Models;
using FinanceAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    /*options.EnableAnnotations();*/
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        
        Title = "Swagger Documentação Web API Finanças Pessoais",
        Description = "EndPoints para gerenciar Finanças Pessoais",
        Contact = new OpenApiContact(){
            Name = "Jean Moreira, Andre Nichele, Pablo P",
            Url = new Uri("https://github.com/ojeanfeli-pe/person_finance"),
        },
    
        License = new OpenApiLicense(){
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
    }
);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Endopints dos usuarios

// GET: users = Listar todos os users cadastrados
app.MapGet("/api/users", 
    ([FromServices] AppDataContext ctx) => 
    {
        if(ctx.Users.Any()){
            return Results.Ok(ctx.Users.ToList());
        }
        return Results.NotFound();
});

// Post: users/register = Registrar e salver um user
app.MapPost("/api/users/register",
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


// Login de usuário
app.MapPost("/api/users/login", 
    ([FromBody] User login,[FromServices] AppDataContext ctx) =>{

        var user = ctx.Users.FirstOrDefault(u => u.Name == login.Name); //FirstOrDefault retorna o primeiro que encontrar ou null se não achar ninguém

        if (user == null){
            return Results.BadRequest("Usuário ou senha inválidos.");
        }

        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(login.Password ?? ""); // Transforma a senha digitada (string)
        var hash = sha256.ComputeHash(bytes); // Calcula o hash SHA256 da senha em bytes
        var hashedInputPassword = Convert.ToBase64String(hash); //Converte esse hash (que é binário) em uma string base64 (mesmo formato que foi salvo no cadastro)

        if (user.Password != hashedInputPassword){
            return Results.BadRequest("Usuário ou senha inválidos.");
        }

        return Results.Ok("Login realizado com sucesso.");
});


// Atualiza o usuario e senha pelo ID

app.MapPut("/api/users/{id}", ([FromRoute] int id, [FromBody] User user, [FromServices] AppDataContext ctx) =>{

    User? userUpdate = ctx.Users.Find(id);

     if(userUpdate.Name == null){
        return Results.BadRequest("Usuario não existe!");
    }
           // Gera um código embaralhado (hash) da senha usando SHA256. Substitui a senha original pelo hash. Salva no banco com nome e senha já protegida.
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(user.Password ?? "");
        var hash = sha256.ComputeHash(bytes);
        user.Password = Convert.ToBase64String(hash);

   if   (userUpdate != null){
        userUpdate.Name = user.Name;
        userUpdate.Password = user.Password;
        ctx.Users.Update(userUpdate);
        ctx.SaveChanges();
        return Results.Ok(userUpdate);
   }
   return Results.NotFound();

});

//Deletar usuario e senha pelo ID

app.MapDelete("/api/users/{id}", ([FromRoute] int id, [FromServices] AppDataContext ctx) =>{
    
    User? user = ctx.Users.Find(id);
    
    if(user == null){
        return Results.NotFound();
    }
    ctx.Users.Remove(user);
    ctx.SaveChanges();
    return Results.NoContent();
});





//

app.Run();
