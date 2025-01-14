using Microsoft.Extensions.Logging;
using Moq;
using RDSoft.OwnerCardActions.Domain.Entities;
using RDSoft.OwnerCardActions.Domain.Enums;
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
        actionRulesProviderMock.Setup(q => q.GetActionRules())
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
        actionRulesProviderMock.Setup(q => q.GetActionRules())
            .ReturnsAsync(new List<AllowedActionRuleset>());

        var sut = new CardActionsBusinessLogicProvider(loggerMock.Object, actionRulesProviderMock.Object);

        // Act
        var result = await sut.GetAllowedActionsAsync(It.IsAny<CardDetails>());

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task CardActionsBusinessLogicProvider_NotEmptyCollection()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<CardActionsBusinessLogicProvider>>();
        var actionRulesProviderMock = new Mock<IActionRulesProvider>();
        actionRulesProviderMock.Setup(q => q.GetActionRules())
            .ReturnsAsync(new List<AllowedActionRuleset> { new AllowedActionRuleset
            {
                CardKinds = new List<CardType> { CardType.Debit },
                CardStatuses = new List<CardStatus> { CardStatus.Active },
                CardStatusesPinRestricted = new List<CardStatus>(),
                Action = AllowedAction.Action1
            }});

        var sut = new CardActionsBusinessLogicProvider(loggerMock.Object, actionRulesProviderMock.Object);

        // Act
        var result = await sut.GetAllowedActionsAsync(
            new CardDetails("Card", CardType.Debit, CardStatus.Active, false));

        // Assert
        Assert.NotNull(result);
        Assert.Equal(AllowedAction.Action1.ToString(), result[0]);
    }
    
    [Fact]
    public async Task CardActionsBusinessLogicProvider_NotEmptyCollection_PinSet()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<CardActionsBusinessLogicProvider>>();
        var actionRulesProviderMock = new Mock<IActionRulesProvider>();
        actionRulesProviderMock.Setup(q => q.GetActionRules())
            .ReturnsAsync(new List<AllowedActionRuleset> { new AllowedActionRuleset
            {
                CardKinds = new List<CardType> { CardType.Debit },
                CardStatuses = new List<CardStatus>(),
                CardStatusesPinRestricted = new List<CardStatus>{ CardStatus.Active },
                Action = AllowedAction.Action1
            }});

        var sut = new CardActionsBusinessLogicProvider(loggerMock.Object, actionRulesProviderMock.Object);

        // Act
        var result = await sut.GetAllowedActionsAsync(
            new CardDetails("Card", CardType.Debit, CardStatus.Active, true));

        // Assert
        Assert.NotNull(result);
        Assert.Equal(AllowedAction.Action1.ToString(), result[0]);
    }
}