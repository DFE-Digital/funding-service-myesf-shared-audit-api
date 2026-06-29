using Pds.Shared.Audit.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Pds.Shared.Audit.Services.Interfaces
{
    /// <summary>
    /// Fire and forget event handler interface .
    /// </summary>
    public interface IFireForgetEventHandler
    {
        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="auditServiceCreate">IAuditService.</param>
        void Execute(Func<IAuditService, Task> auditServiceCreate);
    }
}