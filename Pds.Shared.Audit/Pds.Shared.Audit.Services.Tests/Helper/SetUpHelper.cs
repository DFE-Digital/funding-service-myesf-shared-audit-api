using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pds.Shared.Audit.Repository.Context;
using Pds.Shared.Audit.Repository.DependencyInjection;
using Pds.Shared.Audit.Repository.Implementations;
using Pds.Shared.Audit.Repository.Interfaces;
using Pds.Shared.Audit.Services.AutoMapperProfiles;
using Pds.Shared.Audit.Services.Implementations;
using Pds.Shared.Audit.Services.Interfaces;
using System;

namespace Pds.Shared.Audit.Services.Tests.Helper
{
    /// <summary>
    /// SetUpHelper class.
    /// </summary>
    public static class SetUpHelper
    {
        /// <summary>
        /// Gets a ServiceProvider.
        /// </summary>
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Builds a service provider.
        /// </summary>
        /// <returns>An instance of <see cref="PdsContext"/> that uses in-memory database.</returns>
        internal static IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AuditMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IAuditRepository, AuditRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuditService, AuditService>();
            services.AddScoped<IAuditServiceFireForget, AuditServiceFireForget>();
            services.AddScoped<IFireForgetEventHandler, FireForgetEventHandler>();
            services.AddScoped<DbContext, PdsContext>();

            services.AddDbContext<PdsContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: "InMemoryPdsDatabase").UseInternalServiceProvider(ServiceProvider);
            });

            return services.BuildServiceProvider();
        }


        /// <summary>
        /// Provides a new instance of in memory PDS database context.
        /// </summary>
        /// <returns>An instance of <see cref="PdsContext"/> that uses in-memory database.</returns>
        internal static PdsContext GetInMemoryPdsDbContext()
        {
            ServiceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<PdsContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryPdsDatabase")
                .UseInternalServiceProvider(ServiceProvider)
                .Options;

            return new PdsContext(options);
        }
    }
}
