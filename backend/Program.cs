using Serilog;
using Microsoft.OpenApi.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();


try
{
    var builder = WebApplication.CreateBuilder(args);

    //serilog
    builder.Host.UseSerilog((context, servvices, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
    // swagger 
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(o =>
    {
        o.SwaggerDoc("v1", new OpenApiInfo { Title = "Chat API", Version = "v1" });
    });


    //health checks 
    var pgConn = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    var redisConn = builder.Configuration.GetConnectionString("Redis")
        ?? throw new InvalidOperationException("Connection string 'Redis' not found.");


    // health checks
    builder.Services.AddHealthChecks()
        .AddNpgSql(pgConn, name: "PostgreSQL")
        .AddRedis(redisConn, name: "Redis");

    // db context
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(pgConn));

    //CORS
    builder.Services.AddCors(o =>
    {
        o.AddPolicy("AllowFrontend", policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    });

    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    app.UseSerilogRequestLogging();
    app.UseCors("AllowFrontend");
    app.UseHttpsRedirection();



    app.MapHealthChecks("/health");
    app.Run();

}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "App failed to start");
}
finally
{
    Log.CloseAndFlush();
}



