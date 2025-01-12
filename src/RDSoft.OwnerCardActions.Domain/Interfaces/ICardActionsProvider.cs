using RDSoft.OwnerCardActions.Domain.Entities;
using RDSoft.OwnerCardActions.Domain.Enums;

namespace RDSoft.OwnerCardActions.Application.Interfaces;

public interface ICardActionsProvider
{
    Task<List<AllowedAction>> GetAllowedActionsAsync(CardDetails cardDetails);
}