using RDSoft.OwnerCardActions.Infrastructure.Extentions;
using RDSoft.OwnerCardActions.Infrastructure.Middleware;

namespace RDSoft.OwnerCardActions.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register application services
            builder.Services.AddServices();
            
            // Register controllers
            builder.Services.AddControllers();
            
            // Register swagger services
            builder.Services.AddSwagger();

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