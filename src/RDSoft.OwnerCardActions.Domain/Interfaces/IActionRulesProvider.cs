using RDSoft.OwnerCardActions.Domain.Entities;

namespace RDSoft.OwnerCardActions.Domain.Interfaces;

public interface IActionRulesProvider
{
    public Task<IEnumerable<AllowedActionRuleset>> GetActionRules(CardDetails cardDetails);
}