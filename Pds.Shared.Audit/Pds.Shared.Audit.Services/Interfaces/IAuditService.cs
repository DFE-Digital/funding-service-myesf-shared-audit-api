using System.Threading.Tasks;
using ServiceModels = Pds.Shared.Audit.Services.Models;

namespace Pds.Shared.Audit.Services.Interfaces
{
    /// <summary>
    /// Audit service interface.
    /// </summary>
    public interface IAuditService
    {
        /// <summary>
        /// Create new audit.
        /// </summary>
        /// <param name="entity">Audit Model.</param>
        /// <returns>Task.</returns>
        Task CreateAsync(ServiceModels.Audit entity);
    }
}