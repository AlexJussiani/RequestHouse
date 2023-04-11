using RH.Integration.Tests.Config;
using RH.Pedidos.API;
using RH.Pedidos.API.Application.Queries.ViewModels;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace RH.Integration.Tests
{
    [TestCaseOrderer("Features.Tests.PriorityOrderer", "Features.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class PedidoApiTests
    {

        private readonly IntegrationTestsFixture<Startup> _testsFixture;

        public PedidoApiTests(IntegrationTestsFixture<Startup> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Adicionar item em novo pedido"), TestPriority(1)]
        [Trait("Categoria", "Integração API - Pedido")]
        public async Task AdicionarItem_NovoPedido_DeveRetornarComSucesso()
        {
            var item = new ItemViewModel
            {

                ProdutoId = new Guid("576bc18a-6617-4d99-8bd6-f00fa35c0ee0"),
                PedidoId = new Guid("bd32ca50-7023-4760-9630-dfa7d07f9657"),
                ProdutoNome = "X-tudo",
                Quantidade = 2,
                ValorUnitario = 22
            };

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("api/pedido", item);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Editar item em novo pedido"), TestPriority(2)]
        [Trait("Categoria", "Integração API - Pedido")]
        public async Task EditarItem_NovoPedido_DeveRetornarComSucesso()
        {
            var item = new ItemViewModel
            {

                ProdutoId = new Guid("576BC18A-6617-4D99-8BD6-F00FA35C0EE0"),
                PedidoId = new Guid("F12B7756-4485-4E28-98BF-F4B26C6CF53B"),
                ProdutoNome = "X-tudo",
                Quantidade = 3
            };

            // Act
            var postResponse = await _testsFixture.Client.PutAsJsonAsync("api/pedido", item);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        //[Fact(DisplayName = "Remover item em novo pedido")]
        //[Trait("Categoria", "Integração API - Pedido")]
        //public async Task RemoverItem_NovoPedido_DeveRetornarComSucesso()
        //{
        //    var item = new ItemViewModel
        //    {

        //        ProdutoId = Guid.NewGuid(),
        //        PedidoId = Guid.NewGuid(),
        //        ProdutoNome = "X-tudo",
        //        Quantidade = 2,
        //        ValorUnitario = 22
        //    };

        //    // Act
        //    var postResponse = await _testsFixture.Client.DeleteAsync("api/pedido", item);

        //    // Assert
        //    postResponse.EnsureSuccessStatusCode();
        //}
    }
}
