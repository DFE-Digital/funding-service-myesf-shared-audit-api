using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pds.Shared.Audit.Repository.Implementations;
using Pds.Shared.Audit.Repository.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Pds.Shared.Audit.Repository.Tests.Unit
{
    /// <summary>
    /// Unit test unit of work.
    /// </summary>
    [TestClass]
    [TestCategory("Unit")]
    public class UnitOfWorkTests
    {
        /// <summary>
        /// Commit should save the context.
        /// </summary>
        /// <returns>Returns Void.</returns>
        [TestMethod]
        public async Task CommitAsync_ShouldSaveContext()
        {
            //Arrange
            var mockDbContext = Mock.Of<DbContext>(MockBehavior.Strict);
            var mockAuditRepository = Mock.Of<IAuditRepository>(MockBehavior.Strict);
            Mock.Get(mockDbContext).Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            var uow = new UnitOfWork(mockDbContext, mockAuditRepository);

            //Act
            await uow.CommitAsync();

            //Assert
            Mock.Get(mockDbContext).Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
