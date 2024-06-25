using CaseItau.Application.Fundos.UpdatePatrimonio;
using CaseItau.Application.Fundos.UpdatePatrimonio.Actions.Interface;
using CaseItau.Domain.Abstractions;
using CaseItau.Domain.Fundos;
using CaseItau.Domain.Fundos.Enums;
using CaseItau.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace CaseItau.Application.UnitTests.Fundos;

public class UpdatePatrimonioTests
{
    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenFundoDoesNotExist()
    {
        // Arrange
        var command = new UpdatePatrimonioCommand(
            "123",
            100,
            PatrimonyOperation.Add);

        var fundosRepositoryMock = new Mock<IFundosRepository>();
        fundosRepositoryMock
            .Setup(f => f.GetByCodeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Fundo?)null);

        var handler = new UpdatePatrimonioCommandHandler(
            fundosRepositoryMock.Object,
            new Mock<IUnitOfWork>().Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(FundoErrors.CodeDontExists);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenOperationIsSubtractAndPatrimonyIsNegative()
    {
        // Arrange
        var command = new UpdatePatrimonioCommand(
            "123",
            200,
            PatrimonyOperation.Subtract);

        var fundosRepositoryMock = new Mock<IFundosRepository>();
        fundosRepositoryMock
            .Setup(f => f.GetByCodeAsync("123", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Fundo("123", "Fundo Test", "00000000000100", 1, 100));

        var actionMock = new Mock<IPatrimonioAction>();
        actionMock
            .Setup(a => a.Execute(It.IsAny<Fundo>(), It.IsAny<decimal>()))
            .Returns(Result.Failure<string>(FundoErrors.NegativePatrimonyNotAllowed));

        var handler = new UpdatePatrimonioCommandHandler(
            fundosRepositoryMock.Object,
            new Mock<IUnitOfWork>().Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(FundoErrors.NegativePatrimonyNotAllowed);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenOperationIsSubtract()
    {
        // Arrange
        var command = new UpdatePatrimonioCommand(
            "123",
            200,
            PatrimonyOperation.Subtract);

        var fundosRepositoryMock = new Mock<IFundosRepository>();
        fundosRepositoryMock
            .Setup(f => f.GetByCodeAsync("123", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Fundo("123", "Fundo Test", "00000000000100", 1, 300));

        var actionMock = new Mock<IPatrimonioAction>();
        actionMock
            .Setup(a => a.Execute(It.IsAny<Fundo>(), It.IsAny<decimal>()))
            .Returns(Result.Failure<string>(FundoErrors.NegativePatrimonyNotAllowed));

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(1));

        var handler = new UpdatePatrimonioCommandHandler(
            fundosRepositoryMock.Object,
            new Mock<IUnitOfWork>().Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be("123");
        fundosRepositoryMock.Verify(f => f.Update(It.IsAny<Fundo>()), Times.Once);
        unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenOperationIsAdd()
    {
        // Arrange
        var command = new UpdatePatrimonioCommand(
            "123",
            200,
            PatrimonyOperation.Add);

        var fundosRepositoryMock = new Mock<IFundosRepository>();
        fundosRepositoryMock
            .Setup(f => f.GetByCodeAsync("123", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Fundo("123", "Fundo Test", "00000000000100", 1, 300));

        var actionMock = new Mock<IPatrimonioAction>();
        actionMock
            .Setup(a => a.Execute(It.IsAny<Fundo>(), It.IsAny<decimal>()))
            .Returns(Result.Failure<string>(FundoErrors.NegativePatrimonyNotAllowed));

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(1));

        var handler = new UpdatePatrimonioCommandHandler(
            fundosRepositoryMock.Object,
            new Mock<IUnitOfWork>().Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be("123");
        fundosRepositoryMock.Verify(f => f.Update(It.IsAny<Fundo>()), Times.Once);
        unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
