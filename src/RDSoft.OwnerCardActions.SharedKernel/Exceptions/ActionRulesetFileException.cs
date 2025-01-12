namespace RDSoft.OwnerCardActions.SharedKernel.Exceptions;

public class ActionRulesetFileException(string message)
    : Exception($"Error opening file with allowed actions ruleset for the card. Details: {message}");