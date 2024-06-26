using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WonderFood.Application.Clientes.Commands.InserirCliente;
using WonderFood.Application.Clientes.Queries.ObterCliente;
using WonderFood.Domain.Dtos.Cliente;
using WonderFood.WebApi.Controllers;
using WonderFood.WebApi.UnitTests.Fixtures;
using Xunit;

namespace WonderFood.WebApi.UnitTests;

[Collection(nameof(ClienteFixtureCollection))]
public class ClienteControllerTests
{
    private readonly ClienteController _sut;
    private readonly ISender _mediator = Substitute.For<ISender>();
    private readonly ClienteFixture _clienteFixture;
    
    public ClienteControllerTests(ClienteFixture clienteFixture)
    {
        _sut = new ClienteController(_mediator);
        _clienteFixture = clienteFixture;
    }

    [Fact]
    [Trait("WebApi", "Cliente")]
    public async Task ObterClientePorId_DeveRetornarClienteEStatusOK_QuandoHouverClienteCadastrado()
    {
        //Arrange
        var cliente = _clienteFixture.GerarClienteOutputDtoValido();
        var clienteId = cliente.Id;
        _mediator.Send(Arg.Any<ObterClienteQuery>()).Returns(cliente);
    
        //Act
        var resultado = (OkObjectResult)await _sut.ObterClientePorId(clienteId);
    
        //Assert
        resultado.StatusCode.Should().Be(200);
        resultado.Value.As<ClienteOutputDto>().Should().BeEquivalentTo(cliente);
    }
    
    [Fact]
    [Trait("WebApi", "Cliente")]
    public async Task ObterClientePorId_DeveRetornarBadRequest_QuandoUmaExceptionForLancada()
    {
        //Arrange
        _mediator.Send(Arg.Any<ObterClienteQuery>()).Throws(new Exception());
    
        //Act
        var resultado = (BadRequestObjectResult)await _sut.ObterClientePorId(Guid.NewGuid());
    
        //Assert
        resultado.StatusCode.Should().Be(400);
    }
    
    [Fact]
    [Trait("WebApi", "Cliente")]
    public async Task InserirCliente_DeveRetornarClienteCadastradoEStatusOk_QuandoClienteCadastrado()
    {
        //Arrange
        var clienteInput = _clienteFixture.GerarInserirClienteInputDtoValido();
        var clienteOutput = _clienteFixture.GerarClienteOutputDtoValido();
        _mediator.Send(Arg.Any<InserirClienteCommand>()).Returns(clienteOutput);
    
        //Act
        var resultado = (OkObjectResult)await _sut.InserirCliente(clienteInput);
    
        //Assert
        resultado.StatusCode.Should().Be(200);
    }
    
    [Fact]
    [Trait("WebApi", "Cliente")]
    public async Task InserirCliente_DeveRetornarBadRequest_QuandoUmaExceptionForLancada()
    {
        //Arrange
        var clienteInput = _clienteFixture.GerarInserirClienteInputDtoValido();
        _mediator.Send(Arg.Any<InserirClienteCommand>()).Throws(new Exception());
    
        //Act
        var resultado = (BadRequestObjectResult)await _sut.InserirCliente(clienteInput);
    
        //Assert
        resultado.StatusCode.Should().Be(400);
    }
}