namespace API.Domain.Models
{
    public class Produto : Entity
    {
        public required string Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal? Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }

        public Produto()
        {
            DataCadastro = DateTime.UtcNow;
        }

        public void Ativar()
        {
            Ativo = !Ativo;
        }
    }
}
