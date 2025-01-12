using System.Text.Json;
using Microsoft.Extensions.Logging;
using RDSoft.OwnerCardActions.Domain.Entities;
using RDSoft.OwnerCardActions.Domain.Enums;
using RDSoft.OwnerCardActions.Domain.Interfaces;
using RDSoft.OwnerCardActions.SharedKernel.Exceptions;

namespace RDSoft.OwnerCardActions.Domain.Providers;

public class CardActionsBusinessLogicBusinessLogicProvider(
    ILogger<CardActionsBusinessLogicBusinessLogicProvider> logger,
    IActionRulesProvider actionRulesProvider,
    IMemoryCacheProvider memoryCacheProvider) 
    : ICardActionsBusinessLogicProvider
{
    public async Task<List<string>> GetAllowedActionsAsync(CardDetails cardDetails)
    {
        var allowedActions = new List<AllowedAction>();
        IEnumerable<AllowedActionRuleset>? rules = await actionRulesProvider.GetActionRules(cardDetails);

        if (rules == null)
        {
            logger.LogWarning("No rules found in the system.");
            throw new NoRulesFoundException();
        }
        
        foreach (var rule in rules.Where(rule => rule.CardKinds.Contains(cardDetails.CardType)))
        {
            if(rule.CardStatuses.Contains(cardDetails.CardStatus))
            {
                allowedActions.Add(rule.Action);
                continue;
            }
                
            if(cardDetails.IsPinSet && rule.CardStatusesPinRestricted.Contains(cardDetails.CardStatus))
            {
                allowedActions.Add(rule.Action);
            }
        }
        
        return allowedActions.Select(q => Enum.GetName(typeof(AllowedAction), q)).ToList()!;
    }
}