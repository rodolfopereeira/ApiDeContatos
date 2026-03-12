namespace ApiContatos.Models
{   

    public class Contato
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? Endereco { get; set; }
        public string? Categoria { get; set; }
    }
}
