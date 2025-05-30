namespace FinanceAPI.Models;

public class Transaction
{
    public int Id { get; set; }
    public string? Description { get; set; } // Descrição da transação
    public decimal Amount { get; set; } // Valor da transação
    public DateTime Date { get; set; } = DateTime.Now; // Data 
    public string? Type { get; set; }  // Tipo: Entrada ou saida

    public int CategoryId { get; set; } //  Id da categoria
    public Category Category { get; set; } 
    
}
