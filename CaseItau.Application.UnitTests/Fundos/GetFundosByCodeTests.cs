using CaseItau.Application.Fundos.GetFundos;
using CaseItau.Domain.Fundos;
using CaseItau.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace CaseItau.Application.UnitTests.Fundos;

public class GetFundosByCodeTests
{
    [Fact]
    public async Task Handle_Should_ReturnFundos_WhenCodeExists()
    {
        // Arrange
        var query = new GetFundosByCodeQuery("123");

        var fundosRepositoryMock = new Mock<IFundosRepository>();
        fundosRepositoryMock
            .Setup(f => f.GetByCodeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Fundo("123", "Fundo Test", "00000000000100", 1, 2000000));

        var handler = new GetFundosByCodeQueryHandler(fundosRepositoryMock.Object);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Codigo.Should().Be("123");
        result.Value.Nome.Should().Be("Fundo Test");
        result.Value.Cnpj.Should().Be("00000000000100");
        result.Value.Patrimonio.Should().Be(2000000);
    }
}
