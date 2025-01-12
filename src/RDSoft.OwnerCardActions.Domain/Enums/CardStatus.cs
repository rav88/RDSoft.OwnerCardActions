using System.Text.Json.Serialization;

namespace RDSoft.OwnerCardActions.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CardStatus
{
    Ordered,
    Inactive,
    Active,
    Restricted,
    Blocked,
    Expired,
    Closed
}