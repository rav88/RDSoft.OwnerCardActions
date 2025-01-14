using Moq;
using RDSoft.OwnerCardActions.Application.Interfaces;
using RDSoft.OwnerCardActions.Application.Services;
using RDSoft.OwnerCardActions.Domain.Entities;

namespace RDSoft.OwnerCardActions.UnitTests;

public class CardServiceTests
{
    [Fact]
    public async Task GetCardDetais_ReturnsCardDetails()
    {
        // Arrange
        
        // Act
        var sut = new CardService();
        var result = await sut.GetCardDetails("User1", "Card111");
        
        // Assert
        Assert.NotNull(result);
        Assert.IsType<CardDetails>(result);
    }
    
    [Fact]
    public async Task GetCardDetais_ReturnsEmptyCard()
    {
        // Arrange
        
        // Act
        var sut = new CardService();
        var result = await sut.GetCardDetails(string.Empty, string.Empty);
        
        // Assert
        Assert.Null(result);
    }
}