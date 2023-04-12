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

        [Fact(DisplayName = "Adicionar novo pedido"), TestPriority(1)]
        [Trait("Categoria", "Integração API - Pedido")]
        public async Task Adicionar_NovoPedido_DeveRetornarComSucesso()
        {
           
            var pedido = new PedidoViewModel
            {
                ClienteId = new Guid("19dad7f5-ac0d-48c5-9f54-5ad482d691d5")
            };

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("api/pedido", pedido);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

         [Fact(DisplayName = "Adicionar item em novo pedido"), TestPriority(2)]
        [Trait("Categoria", "Integração API - Pedido")]
        public async Task AdicionarItem_NovoPedido_DeveRetornarComSucesso()
        {
            var item = new ItemViewModel
            {

                ProdutoId = new Guid("5AC4CB47-5041-440E-B17D-6A2BA34C605F"),
                PedidoId = new Guid("5AC4CB47-5041-440E-B17D-6A2BA34C605F"), // esse id do pedido precisar estar cadastrado na base de testes
                ProdutoNome = "X-tudo",
                Quantidade = 2,
                ValorUnitario = 22
            };

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("api/pedidoItem", item);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }


        [Fact(DisplayName = "Editar item em novo pedido"), TestPriority(3)]
        [Trait("Categoria", "Integração API - Pedido")]
        public async Task EditarItem_NovoPedido_DeveRetornarComSucesso()
        {
            var item = new ItemViewModel
            {

                ProdutoId = new Guid("5AC4CB47-5041-440E-B17D-6A2BA34C605F"),
                PedidoId = new Guid("5AC4CB47-5041-440E-B17D-6A2BA34C605F"), // esse id do pedido precisar estar cadastrado na base de testes                
                Quantidade = 3
            };

            // Act
            var postResponse = await _testsFixture.Client.PutAsJsonAsync("api/pedidoItem", item);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Remover item em um pedido"), TestPriority(3)]
        [Trait("Categoria", "Integração API - Pedido")]
        public async Task RemoverItem_NovoPedido_DeveRetornarComSucesso()
        {
            Guid produtoId = new Guid("5AC4CB47-5041-440E-B17D-6A2BA34C605F");
            Guid pedidoId = new Guid("5AC4CB47-5041-440E-B17D-6A2BA34C605F"); // esse id do pedido precisar estar cadastrado na base de testes   
            

            // Act
            var postResponse = await _testsFixture.Client.DeleteAsync($"api/pedidoItem?pedidoId={pedidoId}&produtoId={produtoId}");

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }        
    }
}
