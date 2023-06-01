using RH.Core.DomainObjects;
using System;

namespace RH.ApiGateways.Models
{
    public class ClienteDTO
    {
        public string Nome { get; set; }
        public Email Email { get; set; }
        public Cpf Cpf { get; set; }
        public string Telefone { get; set; }
        public bool Excluido { get; set; }
        public bool EhFornecedor { get; set; }
        public bool EhCliente { get; set; }

    }
}
