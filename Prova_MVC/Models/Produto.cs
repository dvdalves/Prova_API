using System.ComponentModel.DataAnnotations;

namespace Prova_MVC.Models
{
    public class Produto : Entity
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public required string Nome { get; set; }

        public string? Descricao { get; set; }

        [Range(1, 1000000, ErrorMessage = "O campo {0} deve estar entre {1} e {2}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public required decimal Valor { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }

        public Produto()
        {
            DataCadastro = DateTime.UtcNow;
        }

        public void Ativar()
        {
            Ativo = true;
        }
    }
}
