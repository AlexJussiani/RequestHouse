﻿using Dapper;
using RH.Pedidos.API.Application.DTO;
using RH.Pedidos.Data.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RH.Pedidos.API.Application.Queries
{
    public interface IPedidoQueries
    {
        Task<PedidoDTO> ObterPedidoPorId(Guid pedidoId);

        Task<IEnumerable<PedidoDTO>> ObterListaPedidos();
    }
    public class PedidoQueries : IPedidoQueries
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoQueries(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public Task<IEnumerable<PedidoDTO>> ObterListaPedidos()
        {
            throw new NotImplementedException();
        }

        public async Task<PedidoDTO> ObterPedidoPorId(Guid pedidoId)
        {
            const string sql = @"SELECT 
                 p.id AS 'IdPedido'
                 ,p.codigo AS 'Codigo'
                 ,p.clienteId AS 'ClienteId'
                 ,p.ValorTotal AS 'ValorTotal'
                 ,p.data_cadastro AS 'DataCadastro'
                 ,p.data_autorizacao AS 'DataAutorizacao'
                 ,p.data_conclusao AS 'DataConclusao'
                 ,p.PedidoStatus AS 'PedidoStatus'
                 ,i.id AS 'IdItem'
                 ,i.pedido_id AS 'PedidoId'
                 ,i.produto_id AS 'ProdutoId'
                 ,i.produto_nome AS 'ProdutoNome'
                 ,i.quantidade AS 'Quantidade'
                 ,i.valor_unitario AS 'ValorUnitario'
                FROM pedidos p
                INNER JOIN pedidoItems i ON p.id=i.pedido_id
                WHERE p.id = @pedidoId";

            var pedido = await _pedidoRepository.ObterConexao()
                .QueryAsync<dynamic>(sql, new { pedidoId });
            
            return pedido == null ? null : MapearPedido(pedido);
        }

        private PedidoDTO MapearPedido(dynamic result)
        {
            var pedido = new PedidoDTO
            {
                IdPedido = result[0].IdPedido,
                Codigo = result[0].Codigo,
                ClienteId = result[0].ClienteId,
                DataAutorizacao = result[0].DataAutorizacao,
                DataCadastro = result[0].DataCadastro,
                DataConclusao = result[0].DataConclusao,
                PedidoStatus = result[0].PedidoStatus,
                ValorTotal = result[0].ValorTotal,

               PedidoItems = new List<PedidoItemDTO>()
            };

            foreach(var item in result)
            {
                var pedidoItem = new PedidoItemDTO
                {
                    idItem = item.IdItem,
                    PedidoId = item.PedidoId,
                    ProdutoId = item.ProdutoId,
                    ProdutoNome = item.ProdutoNome,
                    Quantidade = item.Quantidade,
                    ValorUnitario = item.ValorUnitario
                };
                pedido.PedidoItems.Add(pedidoItem);
            }

            return pedido;
        }
    }
}