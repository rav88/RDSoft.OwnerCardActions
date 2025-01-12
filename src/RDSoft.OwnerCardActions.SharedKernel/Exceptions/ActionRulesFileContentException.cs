namespace RDSoft.OwnerCardActions.SharedKernel.Exceptions;

public class ActionRulesFileContentException(string message)
    : Exception($"Error parsing file with allowed actions ruleset for the card. Details: {message}");