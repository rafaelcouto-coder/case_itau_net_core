using CaseItau.Application.Fundos.CreateFundos;
using CaseItau.Domain.Abstractions;
using CaseItau.Domain.Fundos;
using CaseItau.Domain.Fundos.Enums;
using CaseItau.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace CaseItau.Application.UnitTests.Fundos;

public class CreateFundosTests
{
    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenCodeAlreadyExists()
    {
        // Arrange
        var command = new CreateFundosCommand(
            "123",
            "Fundo Test",
            "00000000000100",
            TipoFundoEnum.RendaFixa,
            1000000);

        var fundosRepositoryMock = new Mock<IFundosRepository>();
        fundosRepositoryMock
            .Setup(f => f.GetByCodeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Fundo("123", "Fundo Test", "00000000000100", 1, 2000000));

        var handler = new CreateFundosCommandHandler(
            fundosRepositoryMock.Object,
            new Mock<IUnitOfWork>().Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(FundoErrors.CodeAlreadyExists);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenCnpjAlreadyExists()
    {
        // Arrange
        var command = new CreateFundosCommand(
            "456",
            "Fundo Test",
            "00000000000100",
            TipoFundoEnum.RendaFixa,
            1000000);

        var fundosRepositoryMock = new Mock<IFundosRepository>();
        fundosRepositoryMock
            .Setup(f => f.GetByCnpjAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Fundo("456", "Fundo Test", "00000000000100", 1, 1000000));

        var handler = new CreateFundosCommandHandler(
            fundosRepositoryMock.Object,
            new Mock<IUnitOfWork>().Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(FundoErrors.CnpjAlreadyExists);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenFundoIsSuccessfullyCreated()
    {
        // Arrange
        var command = new CreateFundosCommand(
            "456",
            "Fundo Test",
            "00000000000100",
            TipoFundoEnum.RendaFixa,
            1000000);

        var fundosRepositoryMock = new Mock<IFundosRepository>();

        fundosRepositoryMock
            .Setup(f => f.GetByCodeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Fundo?)null);
        fundosRepositoryMock
            .Setup(f => f.GetByCnpjAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Fundo?)null);

        fundosRepositoryMock
            .Setup(f => f.Add(It.IsAny<Fundo>()))
            .Verifiable();

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(1));

        var handler = new CreateFundosCommandHandler(
            fundosRepositoryMock.Object,
            unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(command.Codigo);
        fundosRepositoryMock.Verify(f => f.Add(It.IsAny<Fundo>()), Times.Once);
        unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
