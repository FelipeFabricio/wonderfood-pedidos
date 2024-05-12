﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using WonderFood.Application.Pedidos.Commands.Inserir;
using WonderFood.Application.Pedidos.Queries.ObterPedido;
using WonderFood.Domain.Dtos.Pedido;

namespace WonderFood.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidoController  : ControllerBase
{
    private readonly ISender _mediator;

    public PedidoController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obter Pedido por Número do Pedido
    /// </summary>
    /// <response code="200">Dados obtidos com sucesso</response>
    /// <response code="400">Falha ao obter Pedido</response>
    [HttpGet("{numeroPedido:int}")]
    public async Task<IActionResult> ObterPedido(int numeroPedido)
    {
        try
        {
            var command = new ObterPedidoQuery(numeroPedido);
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
    
    /// <summary>
    /// Cadastrar um novo Pedido
    /// </summary>
    /// <response code="200">Cadastrado com sucesso</response>
    /// <response code="400">Falha ao cadastrar Pedido</response>
    [HttpPost]
    public async Task<IActionResult> InserirPedido([FromBody] InserirPedidoInputDto pedidoInput)
    {
        try
        {
            var command = new InserirPedidoCommand(pedidoInput);
            var pedidoCadastrado = await _mediator.Send(command);
            return Ok(pedidoCadastrado);
        }
        catch (Exception e)
        {
            return BadRequest(new { e.Message });
        }
    }
}