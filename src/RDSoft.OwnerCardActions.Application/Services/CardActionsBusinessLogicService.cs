using RDSoft.OwnerCardActions.Application.Interfaces;
using RDSoft.OwnerCardActions.Domain.Entities;
using RDSoft.OwnerCardActions.Domain.Interfaces;
using RDSoft.OwnerCardActions.Domain.Providers;

namespace RDSoft.OwnerCardActions.Application.Services;

public class CardActionsBusinessLogicService(ICardActionsBusinessLogicProvider provider) : ICardActionsBusinessLogicService
{
    public async Task<List<string>> GetAllowedActionsAsync(CardDetails cardDetails)
    {
        return await provider.GetAllowedActionsAsync(cardDetails);
    }
}