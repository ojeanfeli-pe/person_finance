using FinanceAPI.Models;
using FinanceAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configura o contexto do banco de dados (SQLite)
builder.Services.AddDbContext<AppDataContext>();

// Adiciona suporte para documentação Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Swagger Documentação Web API Finanças Pessoais",
        Description = "EndPoints para gerenciar Finanças Pessoais",
        Contact = new OpenApiContact()
        {
            Name = "Jean Moreira, Andre Nichele, Pablo P",
            Url = new Uri("https://github.com/ojeanfeli-pe/person_finance"),
        },
        License = new OpenApiLicense()
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
});

var app = builder.Build();

// Ativa o Swagger na aplicação
app.UseSwagger();
app.UseSwaggerUI();


// CATEGORIAS

// Lista todas as categorias cadastradas
app.MapGet("/api/categories", ([FromServices] AppDataContext ctx) =>
{
    var categories = ctx.Categories;

    if (ctx.Categories.Any()){
        return Results.Ok(categories.ToList()); // Retorna a lista de categorias
    }

    return Results.NotFound(); 
});

// Cadastra uma nova categoria
app.MapPost("/api/categories", ([FromBody] Category category, [FromServices] AppDataContext ctx) =>
{
    ctx.Categories.Add(category);// Adiciona a nova categoria ao contexto
    ctx.SaveChanges();// Salva no banco de dados
    return Results.Created($"/api/categories/{category.Id}", category); // Retorna a categoria criada 
});


// DELETE /api/categories/{id}
// Exclui uma categoria pelo ID
app.MapDelete("/api/categories/{id}", ([FromRoute] int id, [FromServices] AppDataContext ctx) =>
{
    var category = ctx.Categories.Find(id); // Busca a categoria pelo ID
    if (category == null)
        return Results.NotFound("Categoria não encontrada.");

    ctx.Categories.Remove(category); // Remove a categoria do contexto
    ctx.SaveChanges();               // Salva as alterações
    return Results.Ok("Categoria removida com sucesso.");
});


// TRANSAÇÕES

// GET /api/transactions
// Lista todas as transações com suas categorias
app.MapGet("/api/transactions", ([FromServices] AppDataContext ctx) =>
{
    var transactions = ctx.Transactions
        .Include(t => t.Category) // Inclui a categoria associada
        .Select(t => new
        {
            t.Id,
            t.Description,
            t.Amount,
            t.Date,
            t.Type,
            Category = new { t.Category.Id, t.Category.Name }
        })
        .ToList();

    if (!transactions.Any())
        return Results.NotFound("Nenhuma transação encontrada.");

    return Results.Ok(transactions); // Retorna a lista formatada
});

// GET /api/transactions/{id}
// Busca uma transação específica pelo ID
app.MapGet("/api/transactions/{id}", ([FromRoute] int id, [FromServices] AppDataContext ctx) =>
{
    var transaction = ctx.Transactions
        .Include(t => t.Category) // Inclui os dados da categoria
        .Where(t => t.Id == id)
        .Select(t => new
        {
            t.Id,
            t.Description,
            t.Amount,
            t.Date,
            t.Type,
            Category = new { t.Category.Id, t.Category.Name }
        })
        .FirstOrDefault();

    if (transaction == null)
        return Results.NotFound("Transação não encontrada.");

    return Results.Ok(transaction); // Retorna os dados da transação encontrada
});

// POST /api/transactions
// Cadastra uma nova transação
app.MapPost("/api/transactions", ([FromBody] Transaction transaction, [FromServices] AppDataContext ctx) =>
{
    // Verifica se a categoria existe
    var category = ctx.Categories.Find(transaction.CategoryId);
    if (category == null)
        return Results.BadRequest("Categoria não encontrada.");

    ctx.Transactions.Add(transaction); // Adiciona a transação
    ctx.SaveChanges();                 // Salva no banco
    return Results.Ok("Transação cadastrada com sucesso");
});

// PUT /api/transactions/{id}
// Atualiza uma transação existente
app.MapPut("/api/transactions/{id}", ([FromRoute] int id, [FromBody] Transaction t, [FromServices] AppDataContext ctx) =>
{
    var transaction = ctx.Transactions.Find(id); // Busca a transação
    if (transaction == null)
        return Results.NotFound("Transação não encontrada.");

    // Atualiza os dados da transação
    transaction.Description = t.Description;
    transaction.Amount = t.Amount;
    transaction.Date = t.Date;
    transaction.Type = t.Type;
    transaction.CategoryId = t.CategoryId;

    ctx.SaveChanges(); // Salva no banco
    return Results.Ok(transaction); // Retorna a transação atualizada
});

// DELETE /api/transactions/{id}
// Exclui uma transação pelo ID
app.MapDelete("/api/transactions/{id}", ([FromRoute] int id, [FromServices] AppDataContext ctx) =>
{
    var transaction = ctx.Transactions.Find(id); // Busca a transação
    if (transaction == null)
        return Results.NotFound("Transação não encontrada.");

    ctx.Transactions.Remove(transaction); // Remove a transação
    ctx.SaveChanges();                    // Salva a alteração
    return Results.Ok("Transação removida com sucesso.");
});

app.Run();