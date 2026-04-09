using Marketplace.Domain.Aggregates.Feed.Repository;
using Marketplace.Domain.Aggregates.JobPost.Repository;
using Marketplace.Domain.Aggregates.Service.Repository;
using Marketplace.Infrastructure.Persistence;
using Marketplace.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMarketPlaceInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<MarketPlaceDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IJobPostRepository, JobPostRepository>();
            services.AddScoped<IFeedRepository, FeedRepository>();
            services.AddScoped<IServiceProjectionRepository, ServiceProjectionRepository>();

            return services;
        }
    }
}
