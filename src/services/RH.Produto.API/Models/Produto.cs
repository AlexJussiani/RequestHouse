using RH.Core.DomainObjects;
using System;

namespace RH.Produtos.API.Models
{
    public class Produto : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public bool Saida { get; private set; }
        public bool Entrada { get; private set; }

        public Produto() { }

        public Produto(string nome, string descricao, bool ativo, decimal valor, bool saida, bool entrada)
        {
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            Valor = valor;
            Saida = saida;
            Entrada = entrada;
        }

    }
}
