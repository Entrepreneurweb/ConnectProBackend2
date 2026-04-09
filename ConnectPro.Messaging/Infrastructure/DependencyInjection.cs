using Messaging.Application.Abstractions;
using Messaging.Domain.Repositories;
using Messaging.Infrastructure.Persistence;
using Messaging.Infrastructure.Persistence.Repositories;
using Messaging.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMessagingInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<MessagingDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IConversationRepository, ConversationRepository>();
            services.AddScoped<IUserExistenceChecker, UserExistenceChecker>();

            services.AddHttpClient("IdentityBC", client =>
            {
                client.BaseAddress = new Uri(configuration["Services:IdentityBC"]!);
            });

            return services;
        }
    }
}
