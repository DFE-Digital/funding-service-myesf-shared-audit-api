using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pds.Core.Logging;
using Pds.Shared.Audit.Api.Controllers;
using Pds.Shared.Audit.Services.Interfaces;
using System.Net;
using System.Threading.Tasks;
using ServiceModel = Pds.Shared.Audit.Services.Models;

namespace Pds.Shared.Audit.Api.Tests.Unit
{
    [TestClass]
    [TestCategory("Unit")]
    public class AuditControllerTests
    {
        #region Variables

        private Mock<ILoggerAdapter<AuditController>> _mockLogger;

        private Mock<IAuditService> _mockAuditService;

        private Mock<IAuditServiceFireForget> _mockAuditServiceFireForget;

        private ServiceModel.Audit _serviceAudit = new ServiceModel.Audit()
        {
            Action = 0,
            Message = "Test Message",
            Severity = 1,
            Ukprn = 0,
            User = "Audit.Test@education.gov.uk"
        };

        #endregion Variables


        #region Test Initialize

        /// <summary>
        /// Test Initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _mockLogger = new Mock<ILoggerAdapter<AuditController>>(MockBehavior.Strict);
            _mockAuditService = new Mock<IAuditService>(MockBehavior.Strict);
            _mockAuditServiceFireForget = new Mock<IAuditServiceFireForget>(MockBehavior.Strict);
        }

        #endregion Test Initialize


        #region Unit Tests

        /// <summary>
        /// Create Async should return accepted result.
        /// </summary>
        [TestMethod]
        public void Accept_ShouldReturnAccepted()
        {
            // Arrange
            _mockLogger
                .Setup(logger => logger.LogInformation(It.IsAny<string>()))
                .Verifiable();

            _mockAuditService
                .Setup(e => e.CreateAsync(_serviceAudit))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _mockAuditServiceFireForget
                .Setup(e => e.Accept(_serviceAudit))
                .Verifiable();

            AuditController controller = new AuditController(_mockLogger.Object, _mockAuditService.Object, _mockAuditServiceFireForget.Object);

            // Act
            var result = controller.Accept(_serviceAudit);

            // Assert
            result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.Accepted);

            _mockAuditServiceFireForget.Verify(e => e.Accept(_serviceAudit), Times.Once);
        }

        /// <summary>
        /// Create should return created result.
        /// </summary>
        /// <returns>Returns void.</returns>
        [TestMethod]
        public async Task CreateAsync_ShouldReturnCreated()
        {
            // Arrange
            _mockLogger
                .Setup(logger => logger.LogInformation(It.IsAny<string>()))
                .Verifiable();

            _mockAuditService
                .Setup(e => e.CreateAsync(_serviceAudit))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _mockAuditServiceFireForget
                .Setup(e => e.Accept(_serviceAudit))
                .Verifiable();

            AuditController controller = new AuditController(_mockLogger.Object, _mockAuditService.Object, _mockAuditServiceFireForget.Object);

            // Act
            var result = await controller.CreateAsync(_serviceAudit);

            // Assert
            result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.Created);
            _mockAuditService.Verify(x => x.CreateAsync(_serviceAudit), Times.Once);
        }

        #endregion Unit Tests

    }
}