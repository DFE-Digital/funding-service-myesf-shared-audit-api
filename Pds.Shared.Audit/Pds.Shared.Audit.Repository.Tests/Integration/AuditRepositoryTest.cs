using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pds.Shared.Audit.Repository.Context;
using System;
using System.Threading.Tasks;

namespace Pds.Shared.Audit.Repository.Tests.Integration
{
    /// <summary>
    /// Audit Repository unit tests.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class AuditRepositoryTest
    {
        private static IConfiguration _configuration = null;

        internal static IConfiguration Configuration
        {
            get
            {
                return _configuration ?? new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appSettings.development.json").Build();
            }
        }

        /// <summary>
        /// Unit of work instance.
        /// </summary>
        private Implementations.UnitOfWork uow = null;

        private DataModels.Audit genericAudit = new DataModels.Audit()
        {
            Action = 0,
            CreatedAt = DateTime.UtcNow,
            Id = 0,
            Message = "Test Message",
            Severity = 1,
            Ukprn = 0,
            User = "simon.osborn@education.gov.uk"
        };

        private PdsContext _context = null;

        /// <summary>
        /// Initalizes test class.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _context = Pds.Shared.Audit.Repository.Tests.Helper.SetUpHelper.GetInMemoryPdsDbContext();
        }

        /// <summary>
        /// Get all audits test.
        /// </summary>
        /// <returns>Returns void.</returns>
        [TestMethod]
        public async Task GetAllAuditsTest()
        {
            //Arrange
            uow = new Implementations.UnitOfWork(_context, new Implementations.AuditRepository(_context));
            var repository = new Repository.Implementations.AuditRepository(_context);
            SetGenericAuditHelper();
            genericAudit.Action = 1;

            //Act
            await uow.AuditRepository.AddAsync(genericAudit);
            await uow.CommitAsync();
            var audits = uow.AuditRepository.GetAll();

            //Assert
            audits.Should().HaveCountGreaterThan(0);
        }

        #region AddAsync per Action Type

        /// <summary>
        /// Add multi action test.
        /// </summary>
        /// <param name="actionType">Action Type.</param>
        /// <returns>Returns Void.</returns>
        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        [DataRow(7)]
        [DataRow(8)]
        [DataRow(9)]
        [DataRow(10)]
        [DataRow(11)]
        [DataRow(12)]
        [DataRow(13)]
        [DataRow(14)]
        [DataRow(15)]
        [DataRow(16)]
        [DataRow(17)]
        [DataRow(18)]
        [DataRow(19)]
        [DataRow(20)]
        [DataRow(21)]
        [DataRow(22)]
        [DataRow(23)]
        [DataRow(24)]
        [DataRow(25)]
        [DataRow(26)]
        [DataRow(27)]
        [DataRow(28)]
        [DataRow(29)]
        [DataRow(30)]
        [DataRow(31)]
        [DataRow(32)]
        [DataRow(33)]
        [DataRow(34)]
        [DataRow(35)]
        [DataRow(36)]
        public async Task AddMultiActionTest(int actionType)
        {
            await AddAsyncHelper(actionType);
        }

        #endregion AddAsync per Action Type

        /// <summary>
        /// Get by id async test.
        /// </summary>
        /// <returns>returns nothing.</returns>
        [TestMethod]
        public async Task GetByIdAsyncTest()
        {
            //Arrange
            uow = new Implementations.UnitOfWork(_context, new Implementations.AuditRepository(_context));
            SetGenericAuditHelper();
            genericAudit.Action = 1;

            //Act
            await uow.AuditRepository.AddAsync(genericAudit);
            await uow.CommitAsync();
            var actualAudit = await uow.AuditRepository.GetByIdAsync(genericAudit.Id);

            //Assert
            actualAudit.Should().NotBeNull();
        }

        /// <summary>
        /// Get by predicate async test.
        /// </summary>
        /// <returns>Returns void.</returns>
        [TestMethod]
        public async Task GetByPredicateTest()
        {
            //Arrange
            uow = new Implementations.UnitOfWork(_context, new Implementations.AuditRepository(_context));
            SetGenericAuditHelper();
            genericAudit.Action = 1;

            //Act
            await uow.AuditRepository.AddAsync(genericAudit);
            await uow.CommitAsync();
            var actualAudit = uow.AuditRepository.GetByPredicate(x => x.Id == genericAudit.Id);

            //Assert
            actualAudit.Should().NotBeNull();
        }

        /// <summary>
        /// Get many async test.
        /// </summary>
        /// <returns>Returns void.</returns>
        [TestMethod]
        public async Task GetManyTest()
        {
            //Arrange
            SetGenericAuditHelper();
            genericAudit.Action = 1;
            var pdsContext = Pds.Shared.Audit.Repository.Tests.Helper.SetUpHelper.GetInMemoryPdsDbContext();
            uow = new Implementations.UnitOfWork(_context, new Implementations.AuditRepository(_context));

            //Act
            await uow.AuditRepository.AddAsync(genericAudit);
            await uow.CommitAsync();
            var actualAudit = uow.AuditRepository.GetMany(w => w.Action == 0);

            //Assert
            actualAudit.Should().NotBeNull();
        }

        /// <summary>
        /// Update async test.
        /// </summary>
        /// <returns>Returns void.</returns>
        [TestMethod]
        public async Task UpdateTest()
        {
            //Arrange
            uow = new Implementations.UnitOfWork(_context, new Implementations.AuditRepository(_context));
            SetGenericAuditHelper();
            genericAudit.Action = 1;

            //Act
            await uow.AuditRepository.AddAsync(genericAudit);
            await uow.CommitAsync();
            var existingAudit = uow.AuditRepository.GetByIdAsync(1).Result;
            existingAudit.Message = $"[UPDATE TEST] " + existingAudit.Message;
            existingAudit.CreatedAt = DateTime.UtcNow;
            uow.AuditRepository.Update(existingAudit);
            await uow.CommitAsync();
            var actualAudit = uow.AuditRepository.GetByIdAsync(1).Result;

            //Assert
            actualAudit.Message.Should().StartWith("[UPDATE TEST]");
            actualAudit.CreatedAt.Should().Be(existingAudit.CreatedAt);
        }

        /// <summary>
        /// Delete async test.
        /// </summary>
        /// <returns>Returns void.</returns>
        [TestMethod]
        public async Task DeleteTest()
        {
            //Arrange
            uow = new Implementations.UnitOfWork(_context, new Implementations.AuditRepository(_context));
            SetGenericAuditHelper();
            genericAudit.Action = 1;

            //Act
            await uow.AuditRepository.AddAsync(genericAudit);
            await uow.CommitAsync();
            uow.AuditRepository.Delete(genericAudit);
            await uow.CommitAsync();
            var actualAudit = uow.AuditRepository.GetByIdAsync(genericAudit.Id);

            //Assert
            actualAudit.Result.Should().BeNull();
        }

        /// <summary>
        /// Add async helper.
        /// </summary>
        /// <param name="actionTypeId">action type id to use.</param>
        private async Task AddAsyncHelper(int actionTypeId)
        {
            //Arrange
            uow = new Implementations.UnitOfWork(_context, new Implementations.AuditRepository(_context));
            SetGenericAuditHelper();
            genericAudit.Action = actionTypeId;

            //Act
            await uow.AuditRepository.AddAsync(genericAudit);
            await uow.CommitAsync();
            var actualAudit = await uow.AuditRepository.GetByIdAsync(genericAudit.Id);

            //Assert
            actualAudit.Should().NotBeNull();
            actualAudit.Message.Should().Be(genericAudit.Message);
        }

        private void SetGenericAuditHelper()
        {
            genericAudit = new DataModels.Audit()
            {
                Action = 0,
                CreatedAt = DateTime.UtcNow,
                Id = 0,
                Message = "Test Message",
                Severity = 1,
                Ukprn = 0,
                User = "simon.osborn@education.gov.uk"
            };
        }
    }
}