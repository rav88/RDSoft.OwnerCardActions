using RDSoft.OwnerCardActions.Domain.Entities;
using RDSoft.OwnerCardActions.Domain.Enums;

namespace RDSoft.OwnerCardActions.Application.Interfaces;

public interface ICardService
{
   Task<CardDetails?> GetCardDetails(string userId, string cardNumber);
}