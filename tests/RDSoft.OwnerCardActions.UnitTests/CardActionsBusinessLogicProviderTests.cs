using Microsoft.Extensions.Logging;
using Moq;
using RDSoft.OwnerCardActions.Domain.Entities;
using RDSoft.OwnerCardActions.Domain.Interfaces;
using RDSoft.OwnerCardActions.Domain.Providers;
using RDSoft.OwnerCardActions.SharedKernel.Exceptions;

namespace RDSoft.OwnerCardActions.UnitTests;

public class CardActionsBusinessLogicProviderTests
{
    [Fact]
    public async Task CardActionsBusinessLogicProvider_HasNullCollection()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<CardActionsBusinessLogicProvider>>();
        var actionRulesProviderMock = new Mock<IActionRulesProvider>();
        actionRulesProviderMock.Setup(q => q.GetActionRules(It.IsAny<CardDetails>()))
            .ReturnsAsync((IEnumerable<AllowedActionRuleset>) null!);

        var sut = new CardActionsBusinessLogicProvider(loggerMock.Object, actionRulesProviderMock.Object);

        // Act
        _ = sut.GetAllowedActionsAsync(It.IsAny<CardDetails>());

        // Assert
        loggerMock.Verify(x => x.Log(
            LogLevel.Warning,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("No rules found in the system.")),
            null,
            It.IsAny<Func<It.IsAnyType, Exception, string>>()!), Times.Once);
        await Assert.ThrowsAsync<NoRulesFoundException>(() => sut.GetAllowedActionsAsync(It.IsAny<CardDetails>()));
    }
    
    [Fact]
    public async Task CardActionsBusinessLogicProvider_EmptyCollection()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<CardActionsBusinessLogicProvider>>();
        var actionRulesProviderMock = new Mock<IActionRulesProvider>();
        actionRulesProviderMock.Setup(q => q.GetActionRules(It.IsAny<CardDetails>()))
            .ReturnsAsync(new List<AllowedActionRuleset>());

        var sut = new CardActionsBusinessLogicProvider(loggerMock.Object, actionRulesProviderMock.Object);

        // Act
        var result = await sut.GetAllowedActionsAsync(It.IsAny<CardDetails>());

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}