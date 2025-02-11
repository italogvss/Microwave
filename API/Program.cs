using API.Security.YourNamespace.Services;
using Data.Context;
using Data.Repository;
using Microondas.API.Interfaces;
using Microondas.API.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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
        builder.Services.AddScoped<ProgramConfigRepository>();
        builder.Services.AddScoped<IProgramConfigService, ProgramConfigService>();
        builder.Services.AddScoped<AuthService>();
        builder.Services.AddControllers().AddDataAnnotationsLocalization();
        builder.Services.AddSwaggerGen();
        builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]);
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

        builder.Services.AddCors();
        builder.Services.AddAuthorization();
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
            // Habilita o Swagger na aplicação
            app.UseSwagger();
            app.UseSwaggerUI();
        
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    } 
}
       
