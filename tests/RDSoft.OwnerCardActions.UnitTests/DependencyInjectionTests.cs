using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RDSoft.OwnerCardActions.Application.Interfaces;
using RDSoft.OwnerCardActions.Application.Services;
using RDSoft.OwnerCardActions.Domain.Interfaces;
using RDSoft.OwnerCardActions.Domain.Providers;
using RDSoft.OwnerCardActions.Infrastructure.Config;
using RDSoft.OwnerCardActions.Infrastructure.Extentions;
using RDSoft.OwnerCardActions.Infrastructure.Providers;

namespace RDSoft.OwnerCardActions.UnitTests;

public class DependencyInjectionTests
{
    [Fact]
    public void AllServices_AreResolvable()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        
        // Mock IConfiguration
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "CacheSettings:DefaultExpirationMinutes", "60" } // Wartość pola konfiguracji
            }!)
            .Build();

        // Register IConfiguration in DI - workaround to have IOptions<T> working
        serviceCollection.AddSingleton<IConfiguration>(configuration);

        // Register services
        serviceCollection.AddLogging();
        serviceCollection.AddCaching();
        serviceCollection.AddServices();
        serviceCollection.AddControllers();
        serviceCollection.AddSwagger();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Act  
        var cacheService = serviceProvider.GetService<ICardService>();
        var cardActionsBusinessLogicProvider = serviceProvider.GetService<ICardActionsBusinessLogicProvider>(); 
        var jsonActionRulesFileProvider = serviceProvider.GetService<IActionRulesProvider>();
        
        // Assert
        Assert.NotNull(cacheService);
        Assert.NotNull(jsonActionRulesFileProvider);
        Assert.NotNull(cardActionsBusinessLogicProvider);
        
        Assert.IsType<CardService>(cacheService);
        Assert.IsType<JsonActionRulesFileProviderTests>(jsonActionRulesFileProvider);
        Assert.IsType<CardActionsBusinessLogicProvider>(cardActionsBusinessLogicProvider);
    }
}