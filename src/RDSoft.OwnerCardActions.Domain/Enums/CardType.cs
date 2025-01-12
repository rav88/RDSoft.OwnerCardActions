using System.Text.Json.Serialization;

namespace RDSoft.OwnerCardActions.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CardType
{
    Prepaid,
    Debit,
    Credit
}