using Dapper;
using RH.Pedidos.API.Application.DTO;
using RH.Pedidos.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RH.Pedidos.API.Application.Queries
{
    public interface IPedidoQueries
    {
        Task<PedidoDTO> ObterPedidoPorId(Guid pedidoId);

        Task<IEnumerable<PedidoDTO>> ObterListaPedidos();
        Task<IEnumerable<PedidoDTO>> ObterListaPedidosNaoConcluido();
        Task<IEnumerable<PedidoDTO>> ObterListaPedidosConcluido();
        Task<IEnumerable<PedidoFilaDTO>> ObterListaPedidosAutorizados();
        public class PedidoQueries : IPedidoQueries
        {
            private readonly IPedidoRepository _pedidoRepository;

            public PedidoQueries(IPedidoRepository pedidoRepository)
            {
                _pedidoRepository = pedidoRepository;
            }

            public async Task<IEnumerable<PedidoFilaDTO>> ObterListaPedidosAutorizados()
            {
                const string sql = @"SELECT 
                    p.codigo AS 'Codigo'
                    FROM pedidos p 
                    WHERE p.PedidoStatus = 3
                    order by data_autorizacao";

                var pedidos = await _pedidoRepository.ObterConexao()
                   .QueryAsync<dynamic>(sql);

                return pedidos == null ? null : MapearListaPedidosAutorizados(pedidos);
            }

            public async Task<IEnumerable<PedidoDTO>> ObterListaPedidosNaoConcluido()
            {
                const string sql = @"SELECT 
                 p.id AS 'IdPedido'
                 ,p.codigo AS 'Codigo'
                 ,p.clienteId AS 'ClienteId'
                 ,p.valor_total AS 'ValorTotal'
                 ,p.data_cadastro AS 'DataCadastro'
                 ,p.data_autorizacao AS 'DataAutorizacao'
                 ,p.data_conclusao AS 'DataConclusao'
                 ,p.PedidoStatus AS 'PedidoStatus'                 
                FROM pedidos p 
                where p.PedidoStatus in (1,2,3,4) order by codigo desc";
                var pedidos = await _pedidoRepository.ObterConexao()
                    .QueryAsync<dynamic>(sql);

                return pedidos == null ? null : MapearListaPedidos(pedidos);
            }

            public async Task<IEnumerable<PedidoDTO>> ObterListaPedidosConcluido()
            {
                const string sql = @"SELECT 
                 p.id AS 'IdPedido'
                 ,p.codigo AS 'Codigo'
                 ,p.clienteId AS 'ClienteId'
                 ,p.valor_total AS 'ValorTotal'
                 ,p.data_cadastro AS 'DataCadastro'
                 ,p.data_autorizacao AS 'DataAutorizacao'
                 ,p.data_conclusao AS 'DataConclusao'
                 ,p.PedidoStatus AS 'PedidoStatus'                 
                FROM pedidos p 
                where p.PedidoStatus in (5) order by codigo desc";
                var pedidos = await _pedidoRepository.ObterConexao()
                    .QueryAsync<dynamic>(sql);

                return pedidos == null ? null : MapearListaPedidos(pedidos);
            }

            public async Task<IEnumerable<PedidoDTO>> ObterListaPedidos()
            {
                const string sql = @"SELECT 
                 p.id AS 'IdPedido'
                 ,p.codigo AS 'Codigo'
                 ,p.clienteId AS 'ClienteId'
                 ,p.valor_total AS 'ValorTotal'
                 ,p.data_cadastro AS 'DataCadastro'
                 ,p.data_autorizacao AS 'DataAutorizacao'
                 ,p.data_conclusao AS 'DataConclusao'
                 ,p.PedidoStatus AS 'PedidoStatus'
                 
                FROM pedidos p order by codigo desc";
                var pedidos = await _pedidoRepository.ObterConexao()
                    .QueryAsync<dynamic>(sql);

                return pedidos == null ? null : MapearListaPedidos(pedidos);
            }

            public async Task<PedidoDTO> ObterPedidoPorId(Guid pedidoId)
            {
                const string sql = @"SELECT 
                 p.id AS 'IdPedido'
                 ,p.codigo AS 'Codigo'
                 ,p.clienteId AS 'ClienteId'
                 ,p.valor_total AS 'ValorTotal'
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
                LEFT JOIN pedidoItems i ON p.id=i.pedido_id
                WHERE p.id = @pedidoId";

                var pedido = await _pedidoRepository.ObterConexao()
                    .QueryAsync<dynamic>(sql, new { pedidoId });

                return pedido.Count() == 0 ? null : MapearPedido(pedido);
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
                if (result[0].IdItem != null)
                {
                    foreach (var item in result)
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
                }

                return pedido;
            }
            private List<PedidoDTO> MapearListaPedidos(dynamic results)
            {
                var pedidos = new List<PedidoDTO>();
                foreach (var item in results)
                {
                    pedidos.Add(
                        new PedidoDTO
                        {
                            IdPedido = item.IdPedido,
                            Codigo = item.Codigo,
                            ClienteId = item.ClienteId,
                            DataAutorizacao = item.DataAutorizacao,
                            DataCadastro = item.DataCadastro,
                            DataConclusao = item.DataConclusao,
                            PedidoStatus = item.PedidoStatus,
                            ValorTotal = item.ValorTotal,
                        }
                    );
                }
                return pedidos;
            }

            private List<PedidoFilaDTO> MapearListaPedidosAutorizados(dynamic results)
            {
                var pedidos = new List<PedidoFilaDTO>();
                foreach (var item in results)
                {
                    pedidos.Add(
                        new PedidoFilaDTO
                        {
                            Codigo = item.Codigo,
                            NumeroFila = (pedidos.Count + 1),
                        }
                    );
                }
                return pedidos;
            }           
        }
    }
}
