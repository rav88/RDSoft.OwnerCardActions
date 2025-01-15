using RDSoft.OwnerCardActions.Domain.Entities;

namespace RDSoft.OwnerCardActions.Application.Interfaces;

public interface ICardActionsBusinessLogicService
{
    Task<List<string>> GetAllowedActionsAsync(CardDetails cardDetails);
}