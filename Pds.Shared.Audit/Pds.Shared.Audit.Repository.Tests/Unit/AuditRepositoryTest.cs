using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pds.Shared.Audit.Repository.Implementations;
using Pds.Shared.Audit.Repository.Tests.Helper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pds.Shared.Audit.Repository.Tests.Unit
{
    /// <summary>
    /// Unit audit repository test.
    /// </summary>
    [TestClass]
    [TestCategory("Unit")]
    public class AuditRepositoryTest
    {
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

        #region AddAsync per Action Type

        /// <summary>
        /// Add multi action test.
        /// </summary>
        /// <param name="actionType">Action Type.</param>
        /// <returns>Returns void.</returns>
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
            await AddAsyncMockHelper(actionType);
        }

        #endregion AddAsync per Action Type

        /// <summary>
        /// Get all test.
        /// </summary>
        [TestMethod]
        public void GetAllTest()
        {
            //Set up
            var dummyCollection = new List<DataModels.Audit>()
            {
                new DataModels.Audit() { Action = 0, CreatedAt = DateTime.UtcNow, Id = 1, Message = "test message 1", Severity = 1, Ukprn = 0, User = "simon.osborn@education.gov.uk" },
                new DataModels.Audit() { Action = 0, CreatedAt = DateTime.UtcNow, Id = 2, Message = "test message 2", Severity = 1, Ukprn = 0, User = "simon.osborn@education.gov.uk" }
            };

            var mockDbSet = dummyCollection.GetMockDbSet();
            var mockDbContext = Mock.Of<DbContext>(MockBehavior.Strict);
            Mock.Get(mockDbContext)
                .Setup(db => db.Set<DataModels.Audit>())
                .Returns(mockDbSet.Object);

            //Act
            var repo = new Repository<DataModels.Audit>(mockDbContext);
            var audits = repo.GetAll();

            //Verify
            audits.Should().BeEquivalentTo(dummyCollection);
            Mock.Get(mockDbContext)
                .Verify(db => db.Set<DataModels.Audit>(), Times.Once);
        }

        /// <summary>
        /// Get by id async test.
        /// </summary>
        /// <returns>Returns void.</returns>
        [TestMethod]
        public async Task GetByIdAsyncTestAsync()
        {
            var mockDbContext = Mock.Of<DbContext>(MockBehavior.Strict);
            Mock.Get(mockDbContext)
                .Setup(db => db.FindAsync<DataModels.Audit>(1))
                .ReturnsAsync(default(DataModels.Audit));

            //Act
            var repo = new Repository<DataModels.Audit>(mockDbContext);
            var audits = await repo.GetByIdAsync(1);

            //Verify
            Mock.Get(mockDbContext)
                .Verify(db => db.FindAsync<DataModels.Audit>(1), Times.Once);
        }

        /// <summary>
        /// Get by predicate test.
        /// </summary>
        [TestMethod]
        public void GetByPredicateTest()
        {
            //Set up
            var expectation = new DataModels.Audit()
            {
                Action = 0,
                CreatedAt = DateTime.UtcNow,
                Id = 1,
                Message = "expected",
                Severity = 1,
                Ukprn = 0,
                User = "simon.osborn@education.gov.uk"
            };
            var unexpectation = new DataModels.Audit()
            {
                Action = 0,
                CreatedAt = DateTime.UtcNow,
                Id = 2,
                Message = "unexpected",
                Severity = 1,
                Ukprn = 0,
                User = "simon.osborn@education.gov.uk"
            };
            var dummyCollection = new List<DataModels.Audit> { expectation, unexpectation };

            var mockDbSet = dummyCollection.GetMockDbSet();
            var mockDbContext = Mock.Of<DbContext>(MockBehavior.Strict);
            Mock.Get(mockDbContext)
                .Setup(db => db.Set<DataModels.Audit>())
                .Returns(mockDbSet.Object);

            //Act
            var repo = new Repository<DataModels.Audit>(mockDbContext);
            var actual = repo.GetByPredicate(dummy => dummy.Id == 1);

            //Verify
            actual.Should().Be(expectation);
            Mock.Get(mockDbContext).Verify();
        }

        /// <summary>
        /// Get many test.
        /// </summary>
        [TestMethod]
        public void GetManyTest()
        {
            //Set up
            var expectation = new List<DataModels.Audit>
            {
                new DataModels.Audit { Action = 1, Message = "exactly-expected 1" },
                new DataModels.Audit { Action = 2, Message = "exactly-expected 2" }
            };

            var dummyCollection = new List<DataModels.Audit>
            {
                new DataModels.Audit { Action = 1, Message = "exactly-expected 1" },
                new DataModels.Audit { Action = 2, Message = "exactly-expected 2" },
                new DataModels.Audit { Action = 3, Message = "un-expected 3" }
            };

            var mockDbSet = dummyCollection.GetMockDbSet();
            var mockDbContext = Mock.Of<DbContext>(MockBehavior.Strict);
            Mock.Get(mockDbContext)
                .Setup(db => db.Set<DataModels.Audit>())
                .Returns(mockDbSet.Object);

            //Act
            var repo = new Repository<DataModels.Audit>(mockDbContext);
            var actual = repo.GetMany(dummy => dummy.Message.Contains("exactly-expected"));

            //Verify
            actual.Should().BeEquivalentTo(expectation);
            Mock.Get(mockDbContext).Verify();
        }

        /// <summary>
        /// Update test.
        /// </summary>
        [TestMethod]
        public void UpdateTest()
        {
            //Set up
            var dummyModel = new DataModels.Audit();
            var mockDbSet = Mock.Of<DbSet<DataModels.Audit>>();
            var mockDbContext = Mock.Of<DbContext>(MockBehavior.Strict);
            Mock.Get(mockDbContext)
                .Setup(db => db.Set<DataModels.Audit>())
                .Returns(mockDbSet);

            //Act
            var repo = new Repository<DataModels.Audit>(mockDbContext);
            repo.Update(dummyModel);

            //Verify
            Mock.Get(mockDbContext).Verify();
            Mock.Get(mockDbSet)
                .Verify(e => e.Update(dummyModel), Times.Once);
        }

        /// <summary>
        /// Delete test.
        /// </summary>
        [TestMethod]
        public void DeleteTest()
        {
            //Set up
            var dummyModel = new DataModels.Audit();
            var mockDbSet = Mock.Of<DbSet<DataModels.Audit>>();
            var mockDbContext = Mock.Of<DbContext>(MockBehavior.Strict);
            Mock.Get(mockDbContext)
                .Setup(db => db.Set<DataModels.Audit>())
                .Returns(mockDbSet);

            //Act
            var repo = new Repository<DataModels.Audit>(mockDbContext);
            repo.Delete(dummyModel);

            //Verify
            Mock.Get(mockDbContext).Verify();
            Mock.Get(mockDbSet)
                .Verify(e => e.Remove(dummyModel), Times.Once);
        }

        /// <summary>
        /// Set generic audit helper.
        /// </summary>
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

        /// <summary>
        /// Add async mock helper.
        /// </summary>
        /// <param name="actionTypeId">Action type to mock test.</param>
        private async Task AddAsyncMockHelper(int actionTypeId)
        {
            //Arrange
            SetGenericAuditHelper();

            //Set up
            genericAudit.Action = actionTypeId;
            var mockContext = Mock.Of<DbContext>(MockBehavior.Strict);
            Mock.Get(mockContext)
                .Setup(db => db.AddAsync(genericAudit, It.IsAny<CancellationToken>()))
                .ReturnsAsync(default(EntityEntry<DataModels.Audit>));

            //Act
            var repo = new Repository<DataModels.Audit>(mockContext);
            await repo.AddAsync(genericAudit);

            //Assert
            Mock.Get(mockContext)
                .Verify(db => db.AddAsync(genericAudit, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}