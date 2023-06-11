using RH.Core.DomainObjects;
using System;
using System.Text.Json.Serialization;

namespace RH.Clientes.API.Models
{
    public class ContatoCliente : Entity
    {       

        public string Nome { get; set; }
        public string Telefone { get; set; }
        public Guid ClienteId { get; set; }

        // EF Relation
        [JsonIgnore]
        public Cliente Cliente { get; protected set; }

        public ContatoCliente(string nome, string telefone, Guid clienteId)
        {
            Nome = nome;
            Telefone = telefone;
            ClienteId = clienteId;
        }
    }
}
