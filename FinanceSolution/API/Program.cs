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


//   TRANSACTIONS

// GET para listar todas as transacoes
app.MapGet("/api/transactions", ([FromServices] AppDataContext ctx) =>
{
    var transactions = ctx.Transactions.ToList();

    if (transactions.Count == 0)
    {
        return Results.NotFound("Nenhuma transação encontrada.");
    }

    return Results.Ok(transactions);
});

//POST para cadastrar as transacoes
// app.MapPost("/api/transactions", ([FromBody] Transaction transaction,[FromServices] AppDataContext ctx) =>{
        
//         if (transaction.Amount <= 0){
//             return Results.BadRequest("O valor da transação deve ser maior que zero.");
//         }

//         if (transaction.Type != "entrada" && transaction.Type != "saida"){
//             return Results.BadRequest("Tipo de transação deve ser 'entrada' ou 'saida'.");
//         }

//             // Busca o usuário
//         var user = ctx.Users.Find(transaction.UserId);
//         if (user == null)
//         {
//             return Results.BadRequest("Usuário não encontrado.");
//         }


//         // Cria a transação associando com o usuário
//     var transactionUser = new Transaction
//     {
//         Description = transaction.Description,
//         Amount = transaction.Amount,
//         Date = transaction.Date,
//         Type = transaction.Type,
//         CategoryId = transaction.CategoryId,
//         UserId = transaction.UserId
//     };

//     ctx.Transactions.Add(transaction);
//     ctx.SaveChanges();

//      // Retorna com o nome do usuário e o ID
//     var finalTransaction = new
//     {
//         transaction.Id,
//         transaction.Description,
//         transaction.Amount,
//         transaction.Date,
//         transaction.Type,
//         transaction.CategoryId,
//         transaction.UserId,
//         UserName = user.Name
//     };
//     return Results.Created($"/api/transactions/{transaction.Id}", finalTransaction);


// });



app.MapGet("/api/transactions/user/{userId}", ([FromRoute] int userId, [FromServices] AppDataContext ctx) =>
{
    var userExists = ctx.Users.Any(u => u.Id == userId);
    if (!userExists)
    {
        return Results.NotFound("Usuário não encontrado.");
    }

    var transactions = ctx.Transactions
        .Where(t => t.UserId == userId)
        .Join(ctx.Categories,
              t => t.CategoryId,
              c => c.Id,
              (t, c) => new {
                  t.Id,
                  t.Description,
                  t.Amount,
                  t.Date,
                  t.Type,
                  t.UserId,
                  CategoryName = c.Name
              })
        .ToList();

    return Results.Ok(transactions);
});


app.MapPost("/api/transactions", ([FromBody] Transaction transaction, [FromServices] AppDataContext ctx) =>
{
    // Verifica se o usuário existe
    var user = ctx.Users.Find(transaction.UserId);
    if (user == null)
    {
        return Results.BadRequest("Usuário não encontrado.");
    }

    // Verifica se a categoria existe
    var category = ctx.Categories.Find(transaction.CategoryId);
    if (category == null)
    {
        return Results.BadRequest("Categoria não encontrada.");
    }

    ctx.Transactions.Add(transaction);
    ctx.SaveChanges();

    return Results.Created($"/api/transactions/{transaction.Id}", transaction);
});

//DELETE transctions

app.MapDelete("/api/transactions/{transactionId}/user/{userId}", (
    [FromRoute] int transactionId,
    [FromRoute] int userId,
    [FromServices] AppDataContext ctx) =>
{
    var transaction = ctx.Transactions
        .FirstOrDefault(t => t.Id == transactionId && t.UserId == userId);

    if (transaction == null)
    {
        return Results.NotFound("Transação não encontrada para este usuário.");
    }

    ctx.Transactions.Remove(transaction);
    ctx.SaveChanges();

    return Results.Ok("Transação removida com sucesso.");
});

// CATEGORIAS

// GET: Listar todas as categorias
app.MapGet("/api/categories", ([FromServices] AppDataContext ctx) =>
{
    var categories = ctx.Categories.ToList();

    if (!categories.Any())
    {
        return Results.NotFound("Nenhuma categoria encontrada.");
    }

    return Results.Ok(categories);
});

// POST: Criar uma nova categoria
app.MapPost("/api/categories", ([FromBody] Category category, [FromServices] AppDataContext ctx) =>
{
    ctx.Categories.Add(category);
    ctx.SaveChanges();
    return Results.Created($"/api/categories/{category.Id}", category);
});

// PUT: Atualizar uma categoria existente
app.MapPut("/api/categories/{id}", ([FromRoute] int id, [FromBody] Category categoryUpdate, [FromServices] AppDataContext ctx) =>
{
    var category = ctx.Categories.Find(id);

    if (category == null)
    {
        return Results.NotFound("Categoria não encontrada.");
    }

    category.Name = categoryUpdate.Name;
    ctx.Categories.Update(category);
    ctx.SaveChanges();

    return Results.Ok(category);
});

// DELETE: Remover uma categoria
app.MapDelete("/api/categories/{id}", ([FromRoute] int id, [FromServices] AppDataContext ctx) =>
{
    var category = ctx.Categories.Find(id);

    if (category == null)
    {
        return Results.NotFound("Categoria não encontrada.");
    }

    ctx.Categories.Remove(category);
    ctx.SaveChanges();

    return Results.Ok("Categoria removida com sucesso.");
});

app.Run();
