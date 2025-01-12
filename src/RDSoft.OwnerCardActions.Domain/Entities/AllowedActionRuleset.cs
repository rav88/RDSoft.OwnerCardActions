using System.Text.Json.Serialization;
using RDSoft.OwnerCardActions.Domain.Enums;

namespace RDSoft.OwnerCardActions.Domain.Entities;

public class AllowedActionRuleset
{
    [JsonPropertyName("Action")]
    public required AllowedAction Action { get; set; }

    public required IList<CardType> CardKinds { get; set; }
    
    public required IList<CardStatus> CardStatuses { get; set; }
    
    public required IList<CardStatus> CardStatusesPinRestricted { get; set; }
}