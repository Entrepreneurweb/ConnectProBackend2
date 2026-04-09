using ConnectPro.Identity.Application;
using ConnectPro.Identity.Domain;
using ConnectPro.Identity.Infrastructure.Auth;
using ConnectPro.Identity.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Identity.Infrastructure;

namespace ConnectPro.Identity.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // --- EF Core ---
            services.AddDbContext<IdentityAppDbContext>(opt =>
                opt.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName)
                ));

            // --- ASP.NET Identity ---
            services
                .AddIdentityCore<ApplicationUser>(opt =>
                {
                    opt.Password.RequireDigit = true;
                    opt.Password.RequiredLength = 8;
                    opt.Password.RequireUppercase = true;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.User.RequireUniqueEmail = true;
                })
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<IdentityAppDbContext>()
                .AddDefaultTokenProviders();

            // --- JWT ---
            var jwtSettings = configuration
                .GetSection(JwtSettings.SectionName)
                .Get<JwtSettings>()!;

            services.Configure<JwtSettings>(
                configuration.GetSection(JwtSettings.SectionName));

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                    };
                });

            // --- Services ---
            services.AddScoped < IUnitOfWork, UnitOfWork > ();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenService, JwtTokenService>();
           // services.AddScoped<Application.Interfaces.IPasswordHasher, PasswordHasherAdapter>();
            services.AddSingleton<IPasswordHasher<object>, PasswordHasher<object>>();


            // --- SMTP ---
            //services.Configure<SmtpSettings>(configuration.GetSection(SmtpSettings.SectionName));
            //services.AddScoped<IEmailService, SmtpEmailService>();


            return services;
        }
    }
}
