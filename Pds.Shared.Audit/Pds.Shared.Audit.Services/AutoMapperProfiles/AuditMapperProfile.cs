using AutoMapper;
using DataModel = Pds.Shared.Audit.Repository.DataModels;
using ServiceModels = Pds.Shared.Audit.Services.Models;

namespace Pds.Shared.Audit.Services.AutoMapperProfiles
{
    /// <summary>
    /// Automapper profile for data models and service models.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class AuditMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditMapperProfile"/> class.
        /// </summary>
        public AuditMapperProfile()
        {
            CreateMap<DataModel.Audit, ServiceModels.Audit>();
            CreateMap<ServiceModels.Audit, DataModel.Audit>();
        }
    }
}