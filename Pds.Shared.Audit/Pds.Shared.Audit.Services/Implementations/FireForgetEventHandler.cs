using Microsoft.Extensions.DependencyInjection;
using Pds.Core.Logging;
using Pds.Shared.Audit.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Pds.Shared.Audit.Services.Implementations
{
    /// <summary>
    /// Fire and forget event handler.
    /// </summary>
    public class FireForgetEventHandler : IFireForgetEventHandler
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FireForgetEventHandler"/> class.
        /// </summary>
        /// <param name="serviceScopeFactory">IServiceScopeFactory.</param>
        /// <param name="logger">Logger instance.</param>
        public FireForgetEventHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="auditServiceCreate">IAuditService.</param>
        public void Execute(Func<IAuditService, Task> auditServiceCreate)
        {
            Task.Run(async () =>
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var auditService = scope.ServiceProvider.GetRequiredService<IAuditService>();
                await auditServiceCreate(auditService);
            });
        }
    }
}
