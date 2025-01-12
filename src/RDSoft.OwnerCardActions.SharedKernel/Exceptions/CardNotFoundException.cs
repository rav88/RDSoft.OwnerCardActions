namespace RDSoft.OwnerCardActions.SharedKernel.Exceptions
{
    public class CardNotFoundException(string cardId, string userId)
        : Exception($"Card with ID: {cardId} or user with ID: {userId} not found");
}