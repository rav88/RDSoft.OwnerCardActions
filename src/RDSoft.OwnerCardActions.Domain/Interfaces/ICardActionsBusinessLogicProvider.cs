using RDSoft.OwnerCardActions.Domain.Entities;

namespace RDSoft.OwnerCardActions.Domain.Interfaces;

public interface ICardActionsBusinessLogicProvider
{
    Task<List<string>> GetAllowedActionsAsync(CardDetails cardDetails);
}