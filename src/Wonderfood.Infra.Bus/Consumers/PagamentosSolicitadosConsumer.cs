using MassTransit;
using MediatR;
using WonderFood.Application.Pedidos.Commands.IniciarProducaoPedido;
using Wonderfood.Models.Enums;
using Wonderfood.Models.Events;

namespace Wonderfood.Infra.Bus.Consumers;

public class PagamentosSolicitadosConsumer : IConsumer<PagamentoSolicitadoEvent>
{
    private readonly ISender _mediator;

    public PagamentosSolicitadosConsumer(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<PagamentoSolicitadoEvent> context)
    {
        //TODO: Implementar a lógica de iniciar a produção do pedido
        var message = new PagamentoProcessadoEvent()
        {
            IdPedido = context.Message.IdPedido,
            StatusPagamento = SituacaoPagamento.PagamentoAprovado
        };

        var command = new ProcessarProducaoPedidoCommand(message.IdPedido, message.StatusPagamento);
        await _mediator.Send(command);
    }
}