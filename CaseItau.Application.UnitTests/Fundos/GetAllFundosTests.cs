using CaseItau.Application.Fundos.GetAllFundos;
using CaseItau.Application.Fundos.Shared;
using CaseItau.Domain.Fundos;
using CaseItau.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace CaseItau.Application.UnitTests.Fundos;

public class GetAllFundosTests
{
    [Fact]
    public async Task Handle_Should_ReturnAllFundos()
    {
        // Arrange
        var fundos = new List<Fundo>
        {
            new Fundo("123", "Fundo Test", "00000000000100", 1, 2000000),
            new Fundo("456", "Fundo Test", "00000000000100", 1, 1000000)
        };

        var fundosRepositoryMock = new Mock<IFundosRepository>();
        fundosRepositoryMock
            .Setup(f => f.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(fundos);

        var handler = new GetAllFundosQueryHandler(fundosRepositoryMock.Object);

        // Act
        var result = await handler.Handle(new GetAllFundosQuery(), default);

        // Assert
        result.Value.Should().BeEquivalentTo(fundos.Select(fundo => new FundosResponse
        {
            Codigo = fundo.Codigo,
            Nome = fundo.Nome,
            Cnpj = fundo.Cnpj,
            TipoFundo = fundo.TipoFundo,
            Patrimonio = fundo.Patrimonio
        }));
    }
}
