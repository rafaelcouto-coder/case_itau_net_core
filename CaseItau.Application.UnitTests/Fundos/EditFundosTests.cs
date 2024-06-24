using CaseItau.Application.Fundos.EditFundos;
using CaseItau.Domain.Abstractions;
using CaseItau.Domain.Fundos;
using CaseItau.Domain.Fundos.Enums;
using CaseItau.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace CaseItau.Application.UnitTests.Fundos;

public class EditFundosTests
{
    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenFundoDoesNotExist()
    {
        // Arrange
        var command = new EditFundosCommand(
            "123",
            "Fundo Test",
            "00000000000100",
            TipoFundoEnum.RendaFixa);

        var fundosRepositoryMock = new Mock<IFundosRepository>();
        fundosRepositoryMock
            .Setup(f => f.GetByCodeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Fundo?)null);

        var handler = new EditFundosCommandHandler(
            fundosRepositoryMock.Object,
            new Mock<IUnitOfWork>().Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(FundoErrors.CodeDontExists);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenCnpjAlreadyExistsInAnotherFundo()
    {
        // Arrange
        var command = new EditFundosCommand(
            "123",
            "Fundo Test",
            "00000000000100",
            TipoFundoEnum.RendaFixa);

        var fundosRepositoryMock = new Mock<IFundosRepository>();
        fundosRepositoryMock
            .Setup(f => f.GetByCodeAsync("123", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Fundo("123", "Fundo Test", "00000000000100", 1, 2000000));

        fundosRepositoryMock
            .Setup(f => f.GetByCnpjAsync("00000000000100", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Fundo("456", "Other Fundo", "00000000000100", 1, 2000000));

        var handler = new EditFundosCommandHandler(
            fundosRepositoryMock.Object,
            new Mock<IUnitOfWork>().Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(FundoErrors.CnpjAlreadyExists);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenCnpjAlreadyExistsInSomeFundo()
    {
        // Arrange
        var command = new EditFundosCommand(
            "123",
            "Rename Fundo Test",
            "00000000000100",
            TipoFundoEnum.RendaFixa);

        var fundosRepositoryMock = new Mock<IFundosRepository>();
        fundosRepositoryMock
            .Setup(f => f.GetByCodeAsync("123", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Fundo("123", "Fundo Test", "00000000000100", 1, 2000000));

        fundosRepositoryMock
            .Setup(f => f.GetByCnpjAsync("00000000000100", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Fundo("123", "Fundo Test", "00000000000100", 1, 2000000));

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(1));

        var handler = new EditFundosCommandHandler(
            fundosRepositoryMock.Object,
            unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be("123");
        fundosRepositoryMock.Verify(f => f.Update(It.IsAny<Fundo>()), Times.Once);
        unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenFundoIsSuccessfullyEdited()
    {
        // Arrange
        var command = new EditFundosCommand(
            "123",
            "Fundo Test",
            "00000000000100",
            TipoFundoEnum.RendaFixa);

        var fundosRepositoryMock = new Mock<IFundosRepository>();
        fundosRepositoryMock
            .Setup(f => f.GetByCodeAsync("123", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Fundo("123", "Fundo Test", "00000000000100", 1, 2000000));

        fundosRepositoryMock
            .Setup(f => f.GetByCnpjAsync("00000000000100", It.IsAny<CancellationToken>()))
            .ReturnsAsync((Fundo?)null);

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(1));

        var handler = new EditFundosCommandHandler(
            fundosRepositoryMock.Object,
            unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be("123");
        fundosRepositoryMock.Verify(f => f.Update(It.IsAny<Fundo>()), Times.Once);
        unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}