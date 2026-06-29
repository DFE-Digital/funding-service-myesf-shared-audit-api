using System.Threading.Tasks;
using DataModel = Pds.Shared.Audit.Repository.DataModels;

namespace Pds.Shared.Audit.Repository.Interfaces
{
    /// <summary>
    /// Audit repository interface.
    /// </summary>
    public interface IAuditRepository : IRepository<DataModel.Audit>
    {
    }
}
