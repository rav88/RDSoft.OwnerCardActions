using System.Text.Json;
using Microsoft.Extensions.Logging;
using RDSoft.OwnerCardActions.Domain.Entities;
using RDSoft.OwnerCardActions.Domain.Interfaces;
using RDSoft.OwnerCardActions.SharedKernel.Exceptions;

namespace RDSoft.OwnerCardActions.Infrastructure.Providers;

public class JsonActionRulesFileProvider(ILogger<JsonActionRulesFileProvider> logger, IMemoryCacheProvider cache) 
    : IActionRulesProvider
{
    private const string FilePath = "Files\\allowed_action_rules.json";
    private const string CacheKey = "allowedActionRulesets";

    public async Task<IEnumerable<AllowedActionRuleset>> GetActionRules(CardDetails cardDetails)
    {
        if(cache.TryGetValue<IEnumerable<AllowedActionRuleset>>(CacheKey, out var cachedData))
        {
            return cachedData;
        }
        
        var fPath = Path.Combine(AppContext.BaseDirectory, FilePath);
        string fileContentString = await GetFileContnet(fPath);
        var allowedActionRulesets = ParseJson(fileContentString).ToList();
        
        cache.Set(CacheKey, allowedActionRulesets);
        
        return allowedActionRulesets;
    }

    private async Task<string> GetFileContnet(string jsonContent)
    {
        var fPath = Path.Combine(AppContext.BaseDirectory, FilePath);
        string fileContentString;
        
        try
        {
            fileContentString = await File.ReadAllTextAsync(fPath);
        }
        catch (IOException ex)
        {
            logger.LogError($"Error reading file content: {fPath}. Error details: {ex.Message}");
            throw new ActionRulesetFileException(ex.Message);
        }

        return fileContentString;
    }
    
    private IEnumerable<AllowedActionRuleset> ParseJson(string jsonContent)
    {
        List<AllowedActionRuleset>? rules;
        
        try
        {
            rules = JsonSerializer.Deserialize<List<AllowedActionRuleset>>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (Exception ex)
        {
            logger.LogError($"Error reading file content. Error details: {ex.Message}");
            throw new ActionRulesFileContentException(ex.Message);
        }

        return rules ?? [];
    }
}