﻿using WonderFood.Domain.Entities.Enums;

namespace WonderFood.Domain.Dtos.Pedido;

public class StatusPedidoOutputDto
{
    public int NumeroPedido { get; set; }
    public decimal ValorTotal { get; set; }
    public string Status { get; set; }
    public FormaPagamento FormaPagamento { get; set; }
}