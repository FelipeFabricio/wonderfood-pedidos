using WonderFood.Models.Enums;

namespace WonderFood.Models.Events;

public class PagamentoProcessadoEvent
{
    public Guid IdPedido { get; set; }
    public StatusPagamento StatusPagamento { get; set; }
}
