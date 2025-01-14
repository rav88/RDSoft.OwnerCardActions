using Microsoft.AspNetCore.Mvc;
using RDSoft.OwnerCardActions.Application.DTOs;
using RDSoft.OwnerCardActions.Application.Interfaces;
using RDSoft.OwnerCardActions.Domain.Enums;
using RDSoft.OwnerCardActions.Domain.Interfaces;
using RDSoft.OwnerCardActions.SharedKernel.Exceptions;

namespace RDSoft.OwnerCardActions.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CardsController(
    ILogger<CardsController> logger,
    ICardService cardService, 
    ICardActionsBusinessLogicProvider cardActionsBusinessLogicProvider)
    : ControllerBase
{
    [HttpPost]
    [Route("[action]")]
    public async Task<GetAllowedActionsResponseDto> GetAllowedActions(GetAllowedActionsRequestDto request)
    {
        var card = await cardService.GetCardDetails(request.UserId, request.CardNumber);

        if (card == null)
        {
            logger.LogError("Card ({request.CardNumber}) or User ({request.UserId}) not found,", request.CardNumber, request.UserId);
            throw new CardNotFoundException(request.CardNumber, request.UserId);
        }
        
        var allowedActions = await cardActionsBusinessLogicProvider.GetAllowedActionsAsync(card);

        return new GetAllowedActionsResponseDto(allowedActions);
    }
}