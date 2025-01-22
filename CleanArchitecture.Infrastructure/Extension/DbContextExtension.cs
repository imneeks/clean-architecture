using CleanArchitecture.Core.Interface;
using CleanArchitecture.Core.Interface.Core.Interfaces;
using CleanArchitecture.Infrastructure.Persistence.Data;
using CleanArchitecture.Infrastructure.Persistence.Repositories;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;    
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Extension
{
    public static class DbContextExtension
    {        
        public static IServiceCollection AddSqlDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            // Add DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            
            //services.AddScoped<ICountryRepository, CountryRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();


            return services;

        }
    }
}
