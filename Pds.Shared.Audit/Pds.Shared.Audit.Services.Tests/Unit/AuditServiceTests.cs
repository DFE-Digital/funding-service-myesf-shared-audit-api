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

        private Mock<IUnitOfWork> _unitOfWorkMock;

        private Mock<IAuditRepository> _auditRepositoryMock;

        #endregion Variables


        #region Test Initialize

        [TestInitialize]
        public void TestInitialize()
        {
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
            _auditRepositoryMock.Setup(m => m.AddAsync(It.Is<DataModel.Audit>(a => a.Action == 5 && a.Message == "Test" && a.Severity == 1 && a.Ukprn == 12345))).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(m => m.AuditRepository).Returns(_auditRepositoryMock.Object);
            _unitOfWorkMock.Setup(m => m.CommitAsync()).Returns(Task.CompletedTask);
            var smAudit = new ServiceModel.Audit() { Action = 5, Message = "Test", Severity = 1, Ukprn = 12345 };
            IAuditService sut = GetAuditServiceHelper();

            // Act
            await sut.CreateAsync(smAudit);

            //Assert
            _auditRepositoryMock.Verify();
            _auditRepositoryMock.Verify(x => x.AddAsync(It.Is<DataModel.Audit>(a => a.Action == 5 && a.Message == "Test" && a.Severity == 1 && a.Ukprn == 12345)), Times.Once);
            _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
        }

        #endregion Unit Tests


        #region Helpers

        /// <summary>
        /// Create new instance of audit service.
        /// </summary>
        /// <returns>Returns audit service.</returns>
        private IAuditService GetAuditServiceHelper()
        {
            return new AuditService(_unitOfWorkMock.Object);
        }

        #endregion Helpers

    }
}