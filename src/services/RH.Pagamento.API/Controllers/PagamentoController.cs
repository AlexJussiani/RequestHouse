using MediatR;
using Microsoft.AspNetCore.Mvc;
using RH.Core.Controllers;
using RH.Pagamento.API.Application.Commands;
using RH.Pagamento.API.Application.Queries.ViewModels;
using System.Threading.Tasks;

namespace RH.Pagamento.API.Controllers
{
    public class PagamentoController : MainController
    {
        private readonly IMediator _mediatorHandler;

        public PagamentoController(IMediator mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost("api/pagamento")]
        public async Task<IActionResult> RealizarPagamento([FromBody] PagamentoViewModel item)
        {
            var command = new RegistrarPagamentoCommand(item.ContaId, item.ValorPago, item.DataPagamento);
            return CustomResponse(await _mediatorHandler.Send(command));
        }
    }
}
