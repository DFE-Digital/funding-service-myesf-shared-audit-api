using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pds.Shared.Audit.Services.Implementations;
using Pds.Shared.Audit.Services.Interfaces;
using System;
using System.Threading.Tasks;
using ServiceModel = Pds.Shared.Audit.Services.Models;

namespace Pds.Shared.Audit.Services.Tests.Unit
{
    /// <summary>
    /// Fire and forget event handler test.
    /// </summary>
    [TestClass]
    [TestCategory("Unit")]
    public class FireForgetEventHandlerTests
    {
        /// <summary>
        /// Execute should create new audit in a fire and forget manner.
        /// </summary>
        /// <returns>Returns void.</returns>
        [TestMethod]
        public async Task Execute_ShouldCreateAuditAsync()
        {
            // Arrrange
            var dummyAudit = new ServiceModel.Audit();
            var mockAuditService = Mock.Of<IAuditService>(MockBehavior.Strict);
            var serviceProvider = Mock.Of<IServiceProvider>(MockBehavior.Strict);
            var serviceScope = Mock.Of<IServiceScope>(MockBehavior.Strict);
            var serviceScopeFactory = Mock.Of<IServiceScopeFactory>(MockBehavior.Strict);

            Mock.Get(mockAuditService)
                .Setup(x => x.CreateAsync(dummyAudit))
                .Returns(Task.CompletedTask);

            Mock.Get(serviceScope).SetupGet(x => x.ServiceProvider).Returns(serviceProvider);

            Mock.Get(serviceScopeFactory)
                .Setup(x => x.CreateScope())
                .Returns(serviceScope);

            Mock.Get(serviceProvider)
                .Setup(x => x.GetService(typeof(IAuditService)))
                .Returns(mockAuditService);

            Mock.Get(serviceProvider)
                .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactory);

            // Act
            var fireForgetEventHandler = new FireForgetEventHandler(serviceScopeFactory);
            fireForgetEventHandler.Execute(async auditService => await auditService.CreateAsync(dummyAudit));

            // We execute run in a separate thread then the current one so we need to delay to assert.
            await Task.Delay(100);

            //Assert
            Mock.Get(serviceScopeFactory).Verify(x => x.CreateScope(), Times.Once);
            Mock.Get(serviceScope).VerifyGet(x => x.ServiceProvider, Times.Once);
            Mock.Get(serviceProvider).Verify(x => x.GetService(typeof(IAuditService)), Times.Once);
            Mock.Get(mockAuditService).Verify(x => x.CreateAsync(dummyAudit), Times.Once);
        }
    }
}
