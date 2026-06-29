using Microsoft.EntityFrameworkCore;
using Pds.Shared.Audit.Repository.Interfaces;
using System.Threading.Tasks;

namespace Pds.Shared.Audit.Repository.Implementations
{
    /// <summary>
    /// Unit of work.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        /// <summary>
        /// Gets audit repository.
        /// </summary>
        public IAuditRepository AuditRepository { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context"> db context.</param>
        /// <param name="auditRepository">Audit repository.</param>
        public UnitOfWork(DbContext context, IAuditRepository auditRepository)
        {
            _context = context;
            AuditRepository = auditRepository;
        }

        /// <summary>
        /// Asynchronously commits and save changes to the context.
        /// </summary>
        /// <returns> Number of commit to the database.</returns>
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
