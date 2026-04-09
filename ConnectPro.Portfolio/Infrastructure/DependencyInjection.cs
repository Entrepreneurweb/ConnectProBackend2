using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Domain.Aggregates.Portfolio.Repository;
using Portfolio.Domain.Aggregates.Review;
using Portfolio.Domain.Aggregates.Service.Repository;
using Portfolio.Infrastructure.Persistence;
using Portfolio.Infrastructure.Persistence.Repositories;
using Portfolio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPortfolioInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<PortfolioDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(PortfolioDbContext).Assembly.FullName)));

            services.AddScoped<IUnitOfWork>(sp => (IUnitOfWork)sp.GetRequiredService<PortfolioDbContext>());

            services.AddScoped<IPortfolioRepository, PortfolioRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();

            return services;
        }
    }

}
