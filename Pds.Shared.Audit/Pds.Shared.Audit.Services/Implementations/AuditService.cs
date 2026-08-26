using Pds.Core.Utils.Helpers;
using Pds.Shared.Audit.Repository.Interfaces;
using Pds.Shared.Audit.Services.Extensions;
using Pds.Shared.Audit.Services.Interfaces;
using System;
using System.Threading.Tasks;
using DataModel = Pds.Shared.Audit.Repository.DataModels;
using ServiceModel = Pds.Shared.Audit.Services.Models;

namespace Pds.Shared.Audit.Services.Implementations
{
    /// <summary>
    /// Audit service.
    /// </summary>
    public class AuditService : IAuditService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditService"/> class.
        /// </summary>
        /// <param name="unitOfWork">Unit of work.</param>
        public AuditService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Create new Audit repository model and pass on to the repository.
        /// </summary>
        /// <param name="request">Audit entity.</param>
        /// <returns>Returns void.</returns>
        public async Task CreateAsync(ServiceModel.Audit request)
        {
            It.IsNull(request)
                .AsGuard<ArgumentNullException>();

            var dmAudit = AuditMappings.ToAudit(request);
            dmAudit.CreatedAt = DateTime.UtcNow;
            dmAudit.User = dmAudit.User ?? "AuditUser";
            await _unitOfWork.AuditRepository.AddAsync(dmAudit);
            await _unitOfWork.CommitAsync();
        }
    }
}