using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pds.Shared.Audit.Repository.Implementations;
using Pds.Shared.Audit.Repository.Interfaces;
using Pds.Shared.Audit.Services.AutoMapperProfiles;
using Pds.Shared.Audit.Services.Implementations;
using Pds.Shared.Audit.Services.Interfaces;
using Pds.Shared.Audit.Services.Tests.Helper;
using System;
using System.Linq;
using System.Threading.Tasks;
using ServiceModel = Pds.Shared.Audit.Services.Models;

namespace Pds.Shared.Audit.Services.Tests.Integration
{
    [TestClass]
    [TestCategory("Integration")]
    public class AuditServiceFireForgetTest
    {
        /// <summary>
        /// Create a fire and forget audit test.
        /// </summary>
        /// <returns>Returns void.</returns>
        [TestMethod]
        public async Task Accept_ShouldCreateAudit()
        {
            //Arrange
            var provider = SetUpHelper.GetServiceProvider();
            var scopeFactory = provider.GetService<IServiceScopeFactory>();
            IFireForgetEventHandler faf = new FireForgetEventHandler(scopeFactory);
            IAuditServiceFireForget auditServiceFireForget = new AuditServiceFireForget(faf, null);

            var repository = provider.GetService<IAuditRepository>();
            int? ukPrn = 12345670;

            //Act
            auditServiceFireForget.Accept(new ServiceModel.Audit()
            {
                Action = 1,
                Message = "test",
                Severity = 0,
                Ukprn = ukPrn,
                User = "audit@education.gov.uk"
            });

            // Delaying for 2000 milliseconds to allow fire and forget to complete in time before we assert.
            await Task.Delay(2000);

            // Assert
            var result = repository.GetMany(m => m.Ukprn == ukPrn);
            result.FirstOrDefault().Ukprn.Should().Be(ukPrn);
        }
    }
}