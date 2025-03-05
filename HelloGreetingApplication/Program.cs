using BusinessLayer.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using RepositoryLayer;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    logger.Info("Application is starting...");

    var builder = WebApplication.CreateBuilder(args);


    // Add services to the container.
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add swagger
    builder.Services.AddControllers();
    builder.Services.AddScoped<IGreetingBL, GreetingBL>();
    builder.Services.AddScoped<IGreetingRL, GreetingRL>();
    var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    
    var app = builder.Build();

    
        app.UseSwagger();
        app.UseSwaggerUI();
    

    // Configure the HTTP request pipeline.
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Application encountered an error and stopped.");
    throw;
}
finally
{
    LogManager.Shutdown();
}
