﻿using FluentValidation;
using RH.Core.Messages;
using RH.Pedidos.Domain;
using System;
using System.Collections.Generic;

namespace RH.Pedidos.API.Application.Commands
{
    public class AdicionarItemPedidoCommand : Command
    {
        // Pedido
        public Guid PedidoId { get; private set; }

        public Guid ProdutoId { get; private set; }

        public string Nome { get; private set; }

        public int Quantidade { get; private set; }

        public decimal ValorUnitario { get; private set; }

        public AdicionarItemPedidoCommand(Guid pedidoId, Guid produtoId, string nome, int quantidade, decimal valorUnitario)
        {
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            Nome = nome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarItemPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdicionarItemPedidoValidation : AbstractValidator<AdicionarItemPedidoCommand>
    {
        public static string IdPedidoErroMsg => "Id do pedido inválido";
        public static string IdProdutoErroMsg => "Id do produto inválido";
        public static string NomeErroMsg => "O nome do produto não foi informado";
        public static string QtdItemErroMsg => $"A quantidade precisa ser maior que zero";
        public static string QtdMinErroMsg => "A quantidade miníma de um item é 1";
        public static string ValorErroMsg => "O valor do item precisa ser maior que 0";

        public AdicionarItemPedidoValidation()
        {
            RuleFor(c => c.PedidoId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdPedidoErroMsg);

            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdProdutoErroMsg);

            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage(NomeErroMsg);

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage(QtdItemErroMsg);

            RuleFor(c => c.ValorUnitario)
                .GreaterThan(0)
                .WithMessage(ValorErroMsg);
        }
    }
}