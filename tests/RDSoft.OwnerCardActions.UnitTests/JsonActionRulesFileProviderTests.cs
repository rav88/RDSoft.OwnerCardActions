using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Moq;
using RDSoft.OwnerCardActions.Domain.Entities;
using RDSoft.OwnerCardActions.Domain.Interfaces;
using RDSoft.OwnerCardActions.Infrastructure.Providers;

namespace RDSoft.OwnerCardActions.UnitTests;

public class JsonActionRulesFileProviderTests
{
    [Fact]
    public void JsonActionRulesFileProvider_GetActionRules()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<JsonActionRulesFileProvider>>();
        var cacheMock = new Mock<IMemoryCacheProvider>();
        object o;
        cacheMock.Setup(q => q.TryGetValue(It.IsAny<string>(), out o))
            .Returns(false);
        
        var jsonProviderMock = new Mock<JsonActionRulesFileProvider>(loggerMock.Object, cacheMock.Object);
        jsonProviderMock.Setup(q => q.GetActionRules())
            .ReturnsAsync(new List<AllowedActionRuleset>());
        
        var sut = new JsonActionRulesFileProvider(loggerMock.Object, cacheMock.Object);

        // Act
        var result = sut.GetActionRules();

        // Assert
        Assert.NotNull(result);
    }
}