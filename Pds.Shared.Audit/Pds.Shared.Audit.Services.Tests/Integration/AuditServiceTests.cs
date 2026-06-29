using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pds.Shared.Audit.Repository.Implementations;
using Pds.Shared.Audit.Services.AutoMapperProfiles;
using Pds.Shared.Audit.Services.Implementations;
using System.Linq;
using System.Threading.Tasks;
using ServiceModel = Pds.Shared.Audit.Services.Models;

namespace Pds.Shared.Audit.Services.Tests.Integration
{
    [TestClass]
    [TestCategory("Integration")]
    public class AuditServiceTests
    {
        #region Variables

        private UnitOfWork _uow = null;

        private IMapper _mapper = null;

        private ServiceModel.Audit _serviceAudit = new ServiceModel.Audit()
        {
            Action = 0,
            Message = "Test Message",
            Severity = 1,
            Ukprn = 0,
            User = "Audit.Test@education.gov.uk"
        };

        #endregion


        #region Tests

        [TestMethod]
        public async Task CreateAsync_ShouldCreateAudit()
        {
            // Arrrange
            string textMessage = "Log from audit service create async interation test";
            SetMapperHelper();
            using (var pdsContext = Pds.Shared.Audit.Services.Tests.Helper.SetUpHelper.GetInMemoryPdsDbContext())
            {
                var auditRepository = new AuditRepository(pdsContext);
                _uow = new UnitOfWork(pdsContext, auditRepository);
                var auditService = new AuditService(_uow, _mapper);
                SetServiceAuditHelper(textMessage);
                _serviceAudit.Ukprn = 01234567;

                // Act
                await auditService.CreateAsync(_serviceAudit);
                var result = auditRepository.GetMany(m => m.Ukprn == 01234567).FirstOrDefault();

                //Assert
                result.Ukprn.Should().Be(_serviceAudit.Ukprn);
            }
        }

        #endregion


        #region Helpers

        /// <summary>
        /// Set the mapper config.
        /// </summary>
        private void SetMapperHelper()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AuditMapperProfile());
            });

            _mapper = mapperConfig.CreateMapper();
        }

        /// <summary>
        /// Set service Audit model.
        /// </summary>
        private void SetServiceAuditHelper(string textMessage)
        {
            _serviceAudit = new ServiceModel.Audit()
            {
                Action = 0,
                Message = textMessage,
                Severity = 1,
                Ukprn = 12345,
                User = "Audit.Test@education.gov.uk"
            };
        }

        #endregion

    }
}