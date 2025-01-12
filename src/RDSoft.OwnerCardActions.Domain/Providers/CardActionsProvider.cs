using System.Text.Json;
using RDSoft.OwnerCardActions.Application.Interfaces;
using RDSoft.OwnerCardActions.Domain.Entities;
using RDSoft.OwnerCardActions.Domain.Enums;

namespace RDSoft.OwnerCardActions.Domain.Providers;

public class CardActionsProvider : ICardActionsProvider
{
    public async Task<List<AllowedAction>> GetAllowedActionsAsync(CardDetails cardDetails)
    {
        //throw new NotImplementedException();
        
        var allowedActions = new List<AllowedAction>();
        var fPath = Path.Combine(AppContext.BaseDirectory, "Files", "allowed_action_rules_simplified.json");
        
        // TODO: Handle file operations externally
        var jFile = await File.ReadAllTextAsync(fPath);
        var rules = JsonSerializer.Deserialize<List<AllowedActionRuleset>>(jFile);
        
        foreach (var rule in rules.Where(rule => rule.CardKinds.Contains(cardDetails.CardType)))
        {
            if(rule.CardStatuses.Contains(cardDetails.CardStatus))
            {
                allowedActions.Add(rule.Action);
                continue;
            }
                
            if(rule.CardStatusesPinRestricted.Contains(cardDetails.CardStatus))
            {
                if(cardDetails.IsPinSet)
                {
                    allowedActions.Add(rule.Action);
                }
            }
        }
        
        return allowedActions;
    }
}