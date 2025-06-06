using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StonksAPI.DTO.User;
using StonksAPI.Entities;
using StonksAPI.Services;
using StonksAPI.Utility.Parsers;
using StonksAPI.Validators;
using System.Globalization;
using System.Text;

namespace StonksAPI
{
    public class Program
    {
        /*
         * Main class for the ASP.NET 8.0 CORE Server
         * Here we are creating our server by adding controllers and services and defining the HTTP workflow.
         * We can also register classes to be automatically supplied into methods using DependencyInjection container
         * This class is mainly for setting things up, all the logic will be kept in Controllers and Services
         */
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var authenticationSettings = new AuthenticationSettings();

            //fill up authenticationSettings object with settings from appsettings.json
            builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);


            // Add services
            builder.Services.AddHttpClient();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(config => {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
                };
            });


            // DI container
            builder.Services.AddScoped<IStonksApiService, StonksApiService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IHoldingsService, HoldingsService>();
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            builder.Services.AddScoped<IGeneralInfoParser, GeneralInformationParser>();
            builder.Services.AddScoped<IQuotationParser, QuotationParser>();
            builder.Services.AddScoped<IDividendParser, DividendParser>();
            builder.Services.AddScoped<IOverviewParser, OverviewParser>();
            builder.Services.AddScoped<IParserFacade, ParserFacade>();
            builder.Services.AddSingleton<UserDbContext>();
            builder.Services.AddSingleton(authenticationSettings);

            builder.Services.AddControllers();
            
            builder.Services.AddFluentValidationAutoValidation();

            //Build an app
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization(); 

            app.MapControllers();

            app.Run();
        }
    }
}
