namespace Models;
public class Transacao {
    public int Id { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public string Tipo { get; set; } // "Entrada" ou "Saida"
    public int CategoriaId { get; set; }
    public int UsuarioId { get; set; }
}