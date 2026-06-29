using Microsoft.EntityFrameworkCore;
using Pds.Shared.Audit.Repository.Interfaces;
using DataModel = Pds.Shared.Audit.Repository.DataModels;

namespace Pds.Shared.Audit.Repository.Implementations
{
    /// <summary>
    /// Audit repository.
    /// </summary>
    public class AuditRepository : Repository<DataModel.Audit>, IAuditRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditRepository"/> class.
        /// </summary>
        /// <param name="context">Database Context.</param>
        public AuditRepository(DbContext context) : base(context)
        {
        }
    }
}
