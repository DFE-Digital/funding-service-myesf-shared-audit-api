using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pds.Shared.Audit.Repository.Context;
using Pds.Shared.Audit.Repository.DependencyInjection;
using Pds.Shared.Audit.Services.AutoMapperProfiles;
using Pds.Shared.Audit.Services.Implementations;
using Pds.Shared.Audit.Services.Interfaces;

namespace Pds.Shared.Audit.Services.DependencyInjection
{
    /// <summary>
    /// Extensions class for <see cref="IServiceCollection"/> for registering the feature's services.
    /// </summary>
    public static class FeatureServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services for the current feature to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add the feature's services to.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>
        /// A reference to this instance after the operation has completed.
        /// </returns>
        public static IServiceCollection AddFeatureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PdsContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("audit"));
            });

            services.AddRepositoriesServices(configuration);
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AuditMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IAuditService, AuditService>();
            services.AddScoped<IAuditServiceFireForget, AuditServiceFireForget>();
            services.AddScoped<IFireForgetEventHandler, FireForgetEventHandler>();

            return services;
        }
    }
}