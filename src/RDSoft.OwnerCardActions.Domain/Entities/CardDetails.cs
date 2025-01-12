using RDSoft.OwnerCardActions.Domain.Enums;

namespace RDSoft.OwnerCardActions.Domain.Entities;

public record CardDetails( 
    string CardNumber,
    CardType CardType,
    CardStatus CardStatus,
    bool IsPinSet
);