using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pds.Core.Logging;
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
    /// Audit service fire and forget test.
    /// </summary>
    [TestClass]
    [TestCategory("Unit")]
    public class AuditServiceFireForgetTest
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

            _auditRepositoryMock = new Mock<IAuditRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        #endregion Test Initialize


        #region Unit Tests

        /// <summary>
        /// Test should create new audit.
        /// </summary>
        [TestMethod]
        public void Accept_ShouldCreateAudit()
        {
            // Arrrange
            var dummyAudit = new ServiceModel.Audit();

            var mockLogger = Mock.Of<ILoggerAdapter<AuditServiceFireForget>>(MockBehavior.Strict);
            Mock.Get(mockLogger)
                .Setup(l => l.LogError(string.Empty))
                .Verifiable();
            var mockFireForgetEventHandler = Mock.Of<IFireForgetEventHandler>(MockBehavior.Strict);
            Mock.Get(mockFireForgetEventHandler)
                .Setup(m => m.Execute(It.IsAny<Func<IAuditService, Task>>()))
                .Verifiable();

            // Act
            var auditService = new AuditServiceFireForget(mockFireForgetEventHandler, mockLogger);
            auditService.Accept(dummyAudit);

            //Assert
            Mock.Get(mockFireForgetEventHandler)
                .Verify(m => m.Execute(It.IsAny<Func<IAuditService, Task>>()), Times.Once);

            Mock.Get(mockLogger).Verify(m => m.LogError(string.Empty), Times.Never);
        }

        #endregion Unit Tests

    }
}