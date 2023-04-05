using RH.Core.DomainObjects;
using System;
using System.Linq;
using Xunit;

namespace RH.Pedido.Domain.Tests
{
    
    public class PedidoTests
    {
        [Fact(DisplayName = "Adicionar Item Novo Pedido")]
        [Trait("Categoria", "Pedido")]
        public void AdicionarItemPedido_NovoPedido_DeveAtualizarValor()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem = new PedidoItem(Guid.NewGuid(), "X-tudo", 2, 22);

            //Act
             pedido.AdicionarItem(pedidoItem);

            //Assert
            Assert.Equal(44, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Adicionar Item Existente Pedido")]
        [Trait("Categoria", "Pedido")]
        public void AdicionarItemPedido_ItemExistente_DeveIncrementarUnidadesAtualizarValor()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "X-tudo", 2, 22);
            pedido.AdicionarItem(pedidoItem);

            var pedidoItem2 = new PedidoItem(produtoId, "X-tudo", 3, 22);
            var pedidoItem3 = new PedidoItem(produtoId, "X-tudo", 1, 22);

            //Act
            pedido.AdicionarItem(pedidoItem2);
            pedido.AdicionarItem(pedidoItem3);

            //Assert
            Assert.Equal(132, pedido.ValorTotal);
            Assert.Equal(1, pedido.PedidoItems.Count);
            Assert.Equal(6, pedido.PedidoItems.FirstOrDefault(i => i.ProdutoId == produtoId).Quantidade);
        }

        [Fact(DisplayName = "Adicionar 2 Itens diferentes ao Pedido")]
        [Trait("Categoria", "Pedido")]
        public void AdicionarItemPedido_ItensDiferentes_DeveAdicionarItemAtualizarValor()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId1 = Guid.NewGuid();
            var produtoId2 = Guid.NewGuid();
            var produtoId3 = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId1, "X-tudo", 2, 22);
            pedido.AdicionarItem(pedidoItem);

            var pedidoItem2 = new PedidoItem(produtoId2, "X-bagunça", 3, 20);
            var pedidoItem3 = new PedidoItem(produtoId3, "cachorro quente", 1, 10);

            //Act
            pedido.AdicionarItem(pedidoItem2);
            pedido.AdicionarItem(pedidoItem3);

            //Assert
            Assert.Equal(114, pedido.ValorTotal);
            Assert.Equal(3, pedido.PedidoItems.Count);
            Assert.Equal(6, pedido.PedidoItems.Sum(i => i.Quantidade));
        }

        [Fact(DisplayName = "Atualizar Item Pedido Valido")]
        [Trait("Categoria", "Pedido")]
        public void AtualizarItemPedido_ItemValido_DeveAtualizarQuantidadeValor()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Produto Teste", 2, 100);
            pedido.AdicionarItem(pedidoItem);
            var pedidoItemAtualizado = new PedidoItem(produtoId, "Produto Teste", 5, 100);
            var novaQuantidade = pedidoItemAtualizado.Quantidade;

            // Act
            pedido.AtualizarItem(pedidoItemAtualizado);

            // Assert
            Assert.Equal(novaQuantidade, pedido.PedidoItems.FirstOrDefault(p => p.ProdutoId == produtoId).Quantidade);
            Assert.Equal(500, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Atualizar Item Pedido Inexistente")]
        [Trait("Categoria", "Pedido")]
        public void AtualizarItemPedido_ItemInexistente_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Produto Teste", 2, 100);
            // Act Assert
            Assert.Throws<DomainException>(() => pedido.AtualizarItem(pedidoItem));            
        }

        [Fact(DisplayName = "Remover Item Pedido Deve Calcular Valor Total")]
        [Trait("Categoria", "Pedido")]
        public void AdicionarItemPedido_RemoverItem_DeveRemoverItemAtualizarValor()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId1 = Guid.NewGuid();
            var produtoId2 = Guid.NewGuid();
            var produtoId3 = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId1, "X-tudo", 2, 22);
            pedido.AdicionarItem(pedidoItem);

            var pedidoItem2 = new PedidoItem(produtoId2, "X-bagunça", 3, 20);
            var pedidoItem3 = new PedidoItem(produtoId3, "cachorro quente", 1, 10);
            pedido.AdicionarItem(pedidoItem2);
            pedido.AdicionarItem(pedidoItem3);

            //Act
            pedido.RemoverItem(pedidoItem2);

            //Assert
            Assert.Equal(54, pedido.ValorTotal);
            Assert.Equal(2, pedido.PedidoItems.Count);
            Assert.Equal(3, pedido.PedidoItems.Sum(i => i.Quantidade));
        }

        [Fact(DisplayName = "Remover Item Inexistente")]
        [Trait("Categoria", "Pedido")]
        public void AdicionarItemPedido_RemoverItemInexistente_DeveRetornarException()
        {
            //Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId1 = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId1, "X-tudo", 2, 22);

            //Act Assert
            Assert.Throws<DomainException>(() => pedido.RemoverItem(pedidoItem));
        }
    }
}
