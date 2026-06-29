using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pds.Shared.Audit.Repository.Interfaces;
using Pds.Shared.Audit.Services.Implementations;
using Pds.Shared.Audit.Services.Interfaces;
using System;
using System.Threading.Tasks;
using DataModel = Pds.Shared.Audit.Repository.DataModels;
using ServiceModel = Pds.Shared.Audit.Services.Models;

namespace Pds.Shared.Audit.Services.Tests.Unit
{
    /// <summary>
    /// Audit service unit tests.
    /// </summary>
    [TestClass]
    [TestCategory("Unit")]
    public class AuditServiceTests
    {
        #region Variables

        private Mock<IMapper> _mockMapper;

        private Mock<IUnitOfWork> _unitOfWorkMock;

        private Mock<IAuditRepository> _auditRepositoryMock;

        private DataModel.Audit _dmAudit;

        #endregion Variables


        #region Test Initialize

        [TestInitialize]
        public void TestInitialize()
        {
            _dmAudit = new DataModel.Audit() { Action = 5, Message = "Test", Severity = 1, Ukprn = 12345 };
            _mockMapper = new Mock<IMapper>(MockBehavior.Strict);
            _mockMapper.Setup(x => x.Map<ServiceModel.Audit, DataModel.Audit>(It.IsAny<ServiceModel.Audit>())).Returns(_dmAudit);

            _auditRepositoryMock = new Mock<IAuditRepository>(MockBehavior.Strict);
            _unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
        }

        #endregion Test Initialize


        #region Unit Tests

        /// <summary>
        /// Create async should throw argument null exception.
        /// </summary>
        [TestMethod]
        public void CreateAsync_WhenArgumentNull_ThrowsArgumentNullException()
        {
            // Arrange
            ServiceModel.Audit smAudit = null;
            IAuditService sut = GetAuditServiceHelper();

            // Act
            Func<Task> func = () => sut.CreateAsync(smAudit);

            // Assert
            func.Should().ThrowAsync<ArgumentNullException>();
        }

        /// <summary>
        /// Test should create new audit.
        /// </summary>
        /// <returns>Task.</returns>
        [TestMethod]
        public async Task CreateAsync_ShouldCreateAudit()
        {
            // Arrrange
            _auditRepositoryMock.Setup(m => m.AddAsync(_dmAudit)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(m => m.AuditRepository).Returns(_auditRepositoryMock.Object);
            _unitOfWorkMock.Setup(m => m.CommitAsync()).Returns(Task.CompletedTask);
            var smAudit = new ServiceModel.Audit() { Action = 5, Message = "Test", Severity = 1, Ukprn = 12345 };
            IAuditService sut = GetAuditServiceHelper();

            // Act
            await sut.CreateAsync(smAudit);

            //Assert
            _auditRepositoryMock.Verify();
            _auditRepositoryMock.Verify(x => x.AddAsync(_dmAudit), Times.Once);
            _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
            Mock.VerifyAll(_mockMapper);
        }

        #endregion Unit Tests


        #region Helpers

        /// <summary>
        /// Create new instance of audit service.
        /// </summary>
        /// <returns>Returns audit service.</returns>
        private IAuditService GetAuditServiceHelper()
        {
            return new AuditService(_unitOfWorkMock.Object, _mockMapper.Object);
        }

        #endregion Helpers

    }
}