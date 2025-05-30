namespace FinanceAPI.Models;

public class Category  // Vai servir para classificar as transações (comida, roupa, eletrônicos etc...)
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<Transaction>? Transactions { get; set; } // NAVIGATION PROPERTY
}
