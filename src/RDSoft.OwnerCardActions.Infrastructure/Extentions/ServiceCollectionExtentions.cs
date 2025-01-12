using Microsoft.Extensions.DependencyInjection;
using RDSoft.OwnerCardActions.Application.Interfaces;
using RDSoft.OwnerCardActions.Application.Services;
using RDSoft.OwnerCardActions.Domain.Providers;
using RDSoft.OwnerCardActions.Infrastructure.Middleware;

namespace RDSoft.OwnerCardActions.Infrastructure.Extentions;

public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICardService, CardService>(); 
        services.AddScoped<ICardActionsProvider, CardActionsProvider>(); 

        return services;
    }
    
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}