using MediatR;
using Moq;
using Moq.AutoMock;
using RH.Pedidos.API.Application.Commands;
using RH.Pedidos.Data.Repository;
using RH.Pedidos.Domain;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RH.Pedidos.Application.Tests
{


    public class PedidoCommandHandlerTests
    {
        private readonly Guid _pedidoId;
        private readonly Guid _produtoId;
        private readonly Pedido _pedido;
        private readonly AutoMocker _mocker;
        private readonly PedidoCommandHandler _pedidoHandler;

        public PedidoCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _pedidoHandler = _mocker.CreateInstance<PedidoCommandHandler>();

            _pedidoId = Guid.NewGuid();
            _produtoId = Guid.NewGuid();

            _pedido = Pedido.PedidoFactory.NovoPedidoRascunho(_pedidoId);
        }

        [Fact(DisplayName = "Adicionar Item Novo Pedido com Sucesso")]
        [Trait("Categoria", "Pedidos - Pedido Command Handler")]
        public async Task AdicionarItem_NovoPedido_DeveExecutarComSucesso()
        {
            //Arrange
            var pedidoComand = new AdicionarItemPedidoCommand(_pedidoId,
                _produtoId, "X-tudo", 2, 22);

            _mocker.GetMock<IPedidoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            //Act

            var result = await _pedidoHandler.Handle(pedidoComand, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<IPedidoRepository>().Verify(r => r.Adicionar(It.IsAny<Pedido>()), Times.Once);
            _mocker.GetMock<IPedidoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
            //_mocker.GetMock<IMediator>().Verify(r => r.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Novo Item Pedido Rascunho com Sucesso")]
        [Trait("Categoria", "Pedidos - Pedido Command Handler")]
        public async Task AtualizarItem_NovoItemAoPedidoRascunho_DeveExecutarComSucesso()
        {
            //Arrange
            var pedidoItemExistente = new PedidoItem(_produtoId, "x-salada", 2, 12);
            _pedido.AdicionarItem(pedidoItemExistente);

            _mocker.GetMock<IPedidoRepository>().Setup(r => r.ObterPedidoRascunhoPorPedidoId(_pedidoId)).Returns(Task.FromResult(_pedido));
            _mocker.GetMock<IPedidoRepository>().Setup(r => r.ObterItemPorPedido(_pedidoId, _produtoId)).Returns(Task.FromResult(pedidoItemExistente));
            _mocker.GetMock<IPedidoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));


            var pedidoCommand = new AtualizarItemPedidoCommand(_pedidoId, _produtoId, 3);

            // Act
            var result = await _pedidoHandler.Handle(pedidoCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<IPedidoRepository>().Verify(r => r.AtualizarItem(It.IsAny<PedidoItem>()), Times.Once);
            _mocker.GetMock<IPedidoRepository>().Verify(r => r.Atualizar(It.IsAny<Pedido>()), Times.Once);
            _mocker.GetMock<IPedidoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Remover Novo Item Pedido Rascunho com Sucesso")]
        [Trait("Categoria", "Pedidos - Pedido Command Handler")]
        public async Task RemoverItem_NovoItemAoPedidoRascunho_DeveExecutarComSucesso()
        {
            //Arrange
            var pedidoItemExistente = new PedidoItem(_produtoId, "x-salada", 2, 12);
            _pedido.AdicionarItem(pedidoItemExistente);

            _mocker.GetMock<IPedidoRepository>().Setup(r => r.ObterPedidoRascunhoPorPedidoId(_pedidoId)).Returns(Task.FromResult(_pedido));
            _mocker.GetMock<IPedidoRepository>().Setup(r => r.ObterItemPorPedido(_pedidoId, _produtoId)).Returns(Task.FromResult(pedidoItemExistente));
            _mocker.GetMock<IPedidoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            var pedidoCommand = new RemoverItemPedidoCommand(_pedidoId, _produtoId);

            // Act
            var result = await _pedidoHandler.Handle(pedidoCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<IPedidoRepository>().Verify(r => r.RemoverItem(It.IsAny<PedidoItem>()), Times.Once);
            _mocker.GetMock<IPedidoRepository>().Verify(r => r.Atualizar(It.IsAny<Pedido>()), Times.Once);
            _mocker.GetMock<IPedidoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

            [Fact(DisplayName = "Adicionar Novo Item Pedido Rascunho com Sucesso")]
        [Trait("Categoria", "Pedidos - Pedido Command Handler")]
        public async Task AdicionarItem_NovoItemAoPedidoRascunho_DeveExecutarComSucesso()
        {
            var pedidoItemExistente = new PedidoItem(Guid.NewGuid(), "x-salada", 2, 12);
            _pedido.AdicionarItem(pedidoItemExistente);

            var pedidoCommand = new AdicionarItemPedidoCommand(_pedidoId, Guid.NewGuid(), "x-salada", 2, 12);

            _mocker.GetMock<IPedidoRepository>().Setup(r => r.ObterPedidoRascunhoPorPedidoId(_pedidoId)).Returns(Task.FromResult(_pedido));
            _mocker.GetMock<IPedidoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _pedidoHandler.Handle(pedidoCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<IPedidoRepository>().Verify(r => r.AdicionarItem(It.IsAny<PedidoItem>()), Times.Once);
            _mocker.GetMock<IPedidoRepository>().Verify(r => r.Atualizar(It.IsAny<Pedido>()), Times.Once);
            _mocker.GetMock<IPedidoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Item Existente ao Pedido Rascunho com Sucesso")]
        [Trait("Categoria", "Pedidos - Pedido Command Handler")]
        public async Task AdicionarItem_ItemExistenteAoPedidoRascunho_DeveExecutarComSucesso()
        {
            // Arrange
            var pedidoItemExistente = new PedidoItem(_produtoId, "x-tudo", 2, 22);
            _pedido.AdicionarItem(pedidoItemExistente);

            var pedidoCommand = new AdicionarItemPedidoCommand(_pedidoId, _produtoId, "x-tudo", 2, 22);

            _mocker.GetMock<IPedidoRepository>().Setup(r => r.ObterPedidoRascunhoPorPedidoId(_pedidoId)).Returns(Task.FromResult(_pedido));
            _mocker.GetMock<IPedidoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _pedidoHandler.Handle(pedidoCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<IPedidoRepository>().Verify(r => r.AtualizarItem(It.IsAny<PedidoItem>()), Times.Once);
            _mocker.GetMock<IPedidoRepository>().Verify(r => r.Atualizar(It.IsAny<Pedido>()), Times.Once);
            _mocker.GetMock<IPedidoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Item Command Inválido")]
        [Trait("Categoria", "Pedidos - Pedido Command Handler")]
        public async Task AdicionarItem_CommandInvalido_DeveRetornarFalsoELancarEventosDeNotificacao()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(Guid.Empty, Guid.Empty, "", 0, 0);

            // Act
            var result = await _pedidoHandler.Handle(pedidoCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            _mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Exactly(5));
        }
    }
}
