namespace FinanceAPI.Models;

public class User
{

    public int Id { get; set;}
    public string ?Name { get; set; }

    public string? Password { get; set; } // Essa vai vir do usuário, em texto puro, pois dai o codigo que irei colocar no program, ira proteger ela com caracteres diferernte
}