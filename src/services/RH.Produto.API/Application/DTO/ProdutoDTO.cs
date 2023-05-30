using System.ComponentModel.DataAnnotations;

namespace RH.Produtos.API.Application.DTO
{
    public class ProdutoDTO
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool Ativo { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool Entrada { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool Saida { get; set; }
    }
}
