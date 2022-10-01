using CookEat.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace CookEat.Api.Configuration
{
    public static class Startup
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IDatabaseManager>((_) => CreateDatabaseManager(builder.Environment.ContentRootPath));

            // Add services to the container.
            builder.Services.ConfigureCors();

            builder.Services.AddControllers(options =>
            {
                options.Conventions.Insert(0, new RouteConvention(new RouteAttribute("/api")));
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            return builder;
        }

        public static WebApplication ConfigureMiddlewares(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }

        private static IDatabaseManager CreateDatabaseManager(string rootPath)
        {
            var path = Path.Combine(rootPath, "SQL");
            var fileProvider = new PhysicalFileProvider(path);

            return new DatabaseManager(fileProvider);
        }
    }
}
