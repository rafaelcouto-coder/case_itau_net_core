using CaseItau.Application.Fundos.DeleteFundos;
using CaseItau.Domain.Abstractions;
using CaseItau.Domain.Fundos;
using CaseItau.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace CaseItau.Application.UnitTests.Fundos;

public class DeleteFundosCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenFundoDoesNotExist()
    {
        // Arrange
        var command = new DeleteFundosCommand("123");

        var fundosRepositoryMock = new Mock<IFundosRepository>();
        fundosRepositoryMock
            .Setup(f => f.GetByCodeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Fundo?)null);

        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var handler = new DeleteFundosCommandHandler(fundosRepositoryMock.Object, unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(FundoErrors.CodeDontExists);
        fundosRepositoryMock.Verify(f => f.Delete(It.IsAny<Fundo>()), Times.Never);
        unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenFundoExists()
    {
        // Arrange
        var command = new DeleteFundosCommand("456");

        var fundosRepositoryMock = new Mock<IFundosRepository>();
        fundosRepositoryMock
            .Setup(f => f.GetByCodeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Fundo("456", "Fundo Test", "00000000000100", 1, 1000000));

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(1));

        var handler = new DeleteFundosCommandHandler(
            fundosRepositoryMock.Object,
            unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(command.Codigo);
        fundosRepositoryMock.Verify(f => f.Delete(It.IsAny<Fundo>()), Times.Once);
        unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}