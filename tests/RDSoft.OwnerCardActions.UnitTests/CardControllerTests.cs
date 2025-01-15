using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RDSoft.OwnerCardActions.Api.Controllers;
using RDSoft.OwnerCardActions.Application.DTOs;
using RDSoft.OwnerCardActions.Application.Interfaces;
using RDSoft.OwnerCardActions.Domain.Entities;
using RDSoft.OwnerCardActions.Domain.Enums;
using RDSoft.OwnerCardActions.Domain.Interfaces;
using RDSoft.OwnerCardActions.SharedKernel.Exceptions;

namespace RDSoft.OwnerCardActions.UnitTests;

public class CardControllerTests
{
    [Fact]
    public async Task GetCardsDetails_ReturnsCardDetails()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<CardsController>>();
        
        var cardServiceMock = new Mock<ICardService>();
        cardServiceMock.Setup(x => x.GetCardDetails(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new CardDetails
            (
                CardNumber: "Card111",
                CardType: CardType.Debit,
                IsPinSet: true,
                CardStatus: CardStatus.Active
            ));
        var cardActionsBusinessLogicServiceMock = new Mock<ICardActionsBusinessLogicService>();
        cardActionsBusinessLogicServiceMock.Setup(x => x.GetAllowedActionsAsync(It.IsAny<CardDetails>()))
            .ReturnsAsync([AllowedAction.Action1.ToString()]);

        var sut = new CardsController(loggerMock.Object,
            cardServiceMock.Object,
            cardActionsBusinessLogicServiceMock.Object);
        
        // Act
        var result = await sut.GetAllowedActions(
            new GetAllowedActionsRequestDto(UserId: It.IsAny<string>(), CardNumber: It.IsAny<string>()));

        // Assert
        Assert.NotNull(result);
        Assert.IsType<GetAllowedActionsResponseDto>(result);
        Assert.Single(result.AllowedActions);
        Assert.Equal(AllowedAction.Action1.ToString(), result.AllowedActions[0]);
    }
    
    [Fact]
    public async Task GetCardsDetails_ThrowsCardNotFoundException()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<CardsController>>();
        
        var cardServiceMock = new Mock<ICardService>();
        cardServiceMock.Setup(x => x.GetCardDetails(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((CardDetails?)null);
        var cardActionsBusinessLogicServiceMock = new Mock<ICardActionsBusinessLogicService>();

        var sut = new CardsController(loggerMock.Object,
            cardServiceMock.Object,
            cardActionsBusinessLogicServiceMock.Object);
        
        // Act and Assert
        await Assert.ThrowsAsync<CardNotFoundException>(() => 
            sut.GetAllowedActions(new GetAllowedActionsRequestDto(It.IsAny<string>(),It.IsAny<string>())));
    }
}