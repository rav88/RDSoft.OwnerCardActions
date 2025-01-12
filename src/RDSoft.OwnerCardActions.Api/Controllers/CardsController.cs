using Microsoft.AspNetCore.Mvc;
using RDSoft.OwnerCardActions.Application.DTOs;
using RDSoft.OwnerCardActions.Application.Interfaces;
using RDSoft.OwnerCardActions.Domain.Enums;
using RDSoft.OwnerCardActions.Infrastructure.Exceptions;

namespace RDSoft.OwnerCardActions.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CardsController(ICardService cardService, ICardActionsProvider cardActionsProvider)
    : ControllerBase
{
    [HttpPost]
    [Route("[action]")]
    public async Task<GetAllowedActionsResponseDto> GetAllowedActions(GetAllowedActionsRequestDto request)
    {
        await Task.Delay(1000);
        
        var card = await cardService.GetCardDetails(request.UserId, request.CardNumber);

        if (card == null)
        {
            throw new CardNotFoundException(request.CardNumber, request.UserId);
        }
        
        var allowedActions = await cardActionsProvider.GetAllowedActionsAsync(card);

        return new GetAllowedActionsResponseDto(new List<string?>
        {
            Enum.GetName(AllowedAction.Action1),
            Enum.GetName(AllowedAction.Action6),
            Enum.GetName(AllowedAction.Action10)
        });
    }
}