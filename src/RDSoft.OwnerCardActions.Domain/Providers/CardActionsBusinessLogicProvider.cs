using Microsoft.Extensions.Logging;
using RDSoft.OwnerCardActions.Domain.Entities;
using RDSoft.OwnerCardActions.Domain.Enums;
using RDSoft.OwnerCardActions.Domain.Interfaces;
using RDSoft.OwnerCardActions.SharedKernel.Exceptions;

namespace RDSoft.OwnerCardActions.Domain.Providers;

public class CardActionsBusinessLogicProvider(
    ILogger<CardActionsBusinessLogicProvider> logger,
    IActionRulesProvider actionRulesProvider) 
    : ICardActionsBusinessLogicProvider
{
    public async Task<List<string>> GetAllowedActionsAsync(CardDetails cardDetails)
    {
        var allowedActions = new List<string>();
        var rules = await actionRulesProvider.GetActionRules();
        
        if (rules == null)
        {
            logger.LogWarning("No rules found in the system.");
            throw new NoRulesFoundException();
        }
        
        foreach (var rule in rules.Where(rule => rule.CardKinds.Contains(cardDetails.CardType)))
        {
            if(
                rule.CardStatuses.Contains(cardDetails.CardStatus) 
                || 
                (cardDetails.IsPinSet && rule.CardStatusesPinRestricted.Contains(cardDetails.CardStatus)))
                
            {
                allowedActions.Add(Enum.GetName(rule.Action)!);
            }
        }
        
        return allowedActions;
    }
}