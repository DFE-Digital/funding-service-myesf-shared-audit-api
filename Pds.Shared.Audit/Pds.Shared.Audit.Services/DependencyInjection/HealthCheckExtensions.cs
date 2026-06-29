using Microsoft.Extensions.DependencyInjection;
using Pds.Shared.Audit.Repository.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pds.Shared.Audit.Services.DependencyInjection
{
    /// <summary>
    /// Project specific health checks.
    /// </summary>
    public static class HealthCheckExtensions
    {
        /// <summary>
        /// Adds feature specific health checks.
        /// </summary>
        /// <param name="healthChecks">The health checks builder.</param>
        /// <returns>Health checks builder for further chaining.</returns>
        public static IHealthChecksBuilder AddFeatureHealthChecks(this IHealthChecksBuilder healthChecks)
        {
            healthChecks.AddDbContextCheck<PdsContext>();
            return healthChecks;
        }
    }
}
