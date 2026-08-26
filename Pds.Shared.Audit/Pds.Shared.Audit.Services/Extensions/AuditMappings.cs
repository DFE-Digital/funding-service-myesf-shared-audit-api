using AuditRepoModel = Pds.Shared.Audit.Repository.DataModels.Audit;
using AuditServiceModel = Pds.Shared.Audit.Services.Models.Audit;


namespace Pds.Shared.Audit.Services.Extensions
{
    /// <summary>
    /// The Mapping Method.
    /// </summary>
    public static class AuditMappings
    {
        /// <summary>
        /// Mapping method.
        /// </summary>
        /// <param name="audit">The configuration.</param>
        /// <returns>
        /// A mapping between audit datamodels and audit models.
        /// </returns>
        public static AuditRepoModel ToAudit(this AuditServiceModel audit)
        {
            return new AuditRepoModel
            {
                Ukprn = audit.Ukprn,
                Severity = audit.Severity,
                User = audit.User,
                Message = audit.Message,
                Action = audit.Action
            };
        }
    }
}
