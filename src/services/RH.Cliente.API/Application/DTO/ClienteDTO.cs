using RH.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;

namespace RH.Clientes.API.Application.DTO
{
    public class ClienteDTO
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool EhFornecedor { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool EhCliente { get; set; }

    }
}
