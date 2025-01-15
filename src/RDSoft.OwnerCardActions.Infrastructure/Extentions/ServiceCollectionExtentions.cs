using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RDSoft.OwnerCardActions.Application.Interfaces;
using RDSoft.OwnerCardActions.Application.Services;
using RDSoft.OwnerCardActions.Domain.Interfaces;
using RDSoft.OwnerCardActions.Domain.Providers;
using RDSoft.OwnerCardActions.Infrastructure.Config;
using RDSoft.OwnerCardActions.Infrastructure.Providers;

namespace RDSoft.OwnerCardActions.Infrastructure.Extentions;

public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICardService, CardService>(); 
        services.AddScoped<ICardActionsBusinessLogicProvider, CardActionsBusinessLogicProvider>();
        services.AddScoped<ICardActionsBusinessLogicService, CardActionsBusinessLogicService>(); 
        services.AddScoped<IActionRulesProvider, JsonActionRulesFileProvider>();

        return services;
    }
    
    public static IServiceCollection AddCaching(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddScoped<IMemoryCacheProvider, MemoryCacheProvider>();

        return services;
    }
    
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}