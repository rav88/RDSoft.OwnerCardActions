namespace RDSoft.OwnerCardActions.Infrastructure.Exceptions;

public class CardNotFoundException(string cardId, string userId)
    : Exception($"Card with ID: {cardId} for user ID: {userId} not found");