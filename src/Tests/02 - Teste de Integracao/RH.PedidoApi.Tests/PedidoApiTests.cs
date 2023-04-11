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

        [Fact(DisplayName = "Adicionar item em novo pedido")]
        [Trait("Categoria", "Integração API - Pedido")]
        public async Task AdicionarItem_NovoPedido_DeveRetornarComSucesso()
        {
            var item = new ItemViewModel
            {

                ProdutoId = Guid.NewGuid(),
                PedidoId = Guid.NewGuid(),
                ProdutoNome = "X-tudo",
                Quantidade = 2,
                ValorUnitario = 22
            };

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("api/pedido", item);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }
    }
}
