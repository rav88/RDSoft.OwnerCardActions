using RDSoft.OwnerCardActions.Infrastructure.Config;
using RDSoft.OwnerCardActions.Infrastructure.Extentions;
using RDSoft.OwnerCardActions.Infrastructure.Middleware;

namespace RDSoft.OwnerCardActions.Api
{
    public class Program
    {
        private const string CacheSettingsSectionName = "CacheSettings";
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection(CacheSettingsSectionName));
            
            // Add memory cache
            builder.Services.AddCaching();

            // Register application services
            builder.Services.AddServices();
            
            // Register controllers
            builder.Services.AddControllers();
            
            // Register swagger services
            builder.Services.AddSwagger();
            
            // Register logging
            builder.Services.AddLogging();

            var app = builder.Build();
            
            // Configure the HTTP request pipeline for Swagger.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            
            app.MapControllers();

            app.Run();
        }
    }
}