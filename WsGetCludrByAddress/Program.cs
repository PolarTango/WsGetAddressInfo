using Microsoft.AspNetCore.Diagnostics;
using NLog.Web;
using System.Net.Http.Headers;
using WsGetCludrByAddress.WebService;
using WsGetCludrByAddress.WebService.Entities.Config;

internal class Program {
    private static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        var services = builder.Services;
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.Configure<HttpSettings>(builder.Configuration.GetSection("HttpSettings"));
        services.Configure<ContentSettings>(builder.Configuration.GetSection("ContentSettings"));
        
        services.AddTransient<Service>();

        services.AddAutoMapper(typeof(ResponseAddressMapper));

        services.AddExceptionHandler<ExceptionHandler>();

        builder.Host.UseNLog();

        string? httpClientDaData = builder.Configuration["HttpClientDaData"];
        ArgumentException.ThrowIfNullOrEmpty(httpClientDaData);
        builder.Services.AddHttpClient(httpClientDaData, (client) => {
            var settings = builder.Configuration.GetSection("HttpSettings").Get<HttpSettings>();
            client.BaseAddress = new Uri(settings.Url);
            client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue(settings.Accept));
            client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue(
                    settings.AuthScheme, settings.AuthKey);
            client.DefaultRequestHeaders.Add(
                settings.SecretKeyName, settings.SecretKey);
        });

        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseExceptionHandler((o) => { });
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}