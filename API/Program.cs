using Data.Context;
using Data.Repository;
using Microondas.API.Interfaces;
using Microondas.API.Service;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<MicrowaveContext>((options) => {
            options
                    .UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"])
                    .UseLazyLoadingProxies();
        });
        builder.Services.AddScoped<MicrowaveConfigRepository>();
        builder.Services.AddScoped<ProgramConfigRepository>();
        builder.Services.AddScoped<IProgramConfigService, ProgramConfigService>();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
        builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        builder.Services.AddCors();

        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<MicrowaveContext>();
            context.InitializeDatabase();
        }
        app.UseCors(options =>
        {
            options.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();

        });
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();
        app.MapControllers();
        app.Run();
    } 
}
       
