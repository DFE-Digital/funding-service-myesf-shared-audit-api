using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Pds.Core.Logging;
using Pds.Shared.Audit.Api.Controllers;
using Pds.Shared.Audit.Services.Implementations;
using Pds.Shared.Audit.Services.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ServiceModels = Pds.Shared.Audit.Services.Models;

namespace Pds.Shared.Audit.Api.Tests.Integration
{
    [TestClass]
    [TestCategory("Integration")]
    public class AuditControllerTests : IntegrationTest
    {
        private ServiceModels.Audit _serviceAudit = new ServiceModels.Audit()
        {
            Action = 0,
            Message = "Test Message",
            Severity = 1,
            Ukprn = 123456,
            User = "Audit.Test@education.gov.uk"
        };

        #region Tests
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
        public void Accept_Endpoints_MultiActionTest_ReturnAccepted(int actionType)
        {
            //Arrange
            AcceptHelper(GetGenericAuditHelper(actionType), HttpStatusCode.Accepted);
        }

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
        public async Task CreateAsync_Endpoints_MultiActionTest_ReturnCreated(int actionType)
        {
            //Arrange
            await EndpointHelper(GetGenericAuditHelper(actionType), HttpStatusCode.Created);
        }

        [TestMethod]
        public async Task Accept_ShouldReturnAccepted()
        {
            // Arrange
            var obj = new
            {
                Action = 1,
                Message = "Test Message",
                Severity = 2,
                Ukprn = 123456,
                User = "simon.osborn@education.gov.uk"
            };

            // Act
            var result = await CreatePostAsync("/api/auditasync", GetStringContent(obj));

            // Assert
            result.Should().BeOfType<HttpResponseMessage>().Which.StatusCode.Should().Be(HttpStatusCode.Accepted);
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnCreated()
        {
            // Arrange
            var audit = GetGenericAuditHelper(1);

            // Act
            var result = await CreatePostAsync("/api/Audit", GetStringContent(audit));

            // Assert
            result.Should().BeOfType<HttpResponseMessage>().Which.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [TestMethod]
        public async Task Accept_ShouldReturnBadRequest()
        {
            // Arrange
            var obj = new
            {
                Action = 1,
                Message = "Test Message",
                Severity = 2,
                Ukprn = 123456789123,
                User = "simon.osborn@education.gov.uk"
            };

            // Act
            var result = await CreatePostAsync("/api/auditasync", GetStringContent(obj));

            // Assert
            result.Should().BeOfType<HttpResponseMessage>().Which.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnBadRequest()
        {
            // Arrange
            var obj = new
            {
                Action = 1,
                Message = "Test Message",
                Severity = 2,
                Ukprn = 123456789123,
                User = "simon.osborn@education.gov.uk"
            };

            // Act
            var result = await CreatePostAsync("/api/Audit", GetStringContent(obj));

            // Assert
            result.Should().BeOfType<HttpResponseMessage>().Which.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        #endregion


        #region Helpers

        /// <summary>
        /// Accept helper.
        /// </summary>
        /// <param name="expectedStatusCode">Status code to mock test.</param>
        private void AcceptHelper(ServiceModels.Audit audit, HttpStatusCode expectedStatusCode)
        {
            // Arrange
            // Act
            var result = CreatePostAsync("/api/auditasync", GetStringContent(audit)).Result;

            // Assert
            result.Should().BeOfType<HttpResponseMessage>().Which.StatusCode.Should().Be(expectedStatusCode);
        }

        /// <summary>
        /// Create async helper.
        /// </summary>
        /// <param name="expectedStatusCode">Status code to mock test.</param>
        private async Task EndpointHelper(ServiceModels.Audit audit, HttpStatusCode expectedStatusCode)
        {
            // Arrange
            // Act
            var result = await CreatePostAsync("/api/audit", GetStringContent(audit));

            // Assert
            result.Should().BeOfType<HttpResponseMessage>().Which.StatusCode.Should().Be(expectedStatusCode);
        }

        /// <summary>
        /// Get a generic audit object.
        /// </summary>
        /// <param name="actionTypeId">action type.</param>
        /// <returns>Returns a generic audit object.</returns>
        private ServiceModels.Audit GetGenericAuditHelper(int actionTypeId, int severityId = 0)
        {
            return new ServiceModels.Audit()
            {
                Action = actionTypeId,
                Message = "Test Message",
                Severity = severityId,
                Ukprn = 123456,
                User = "simon.osborn@education.gov.uk"
            };
        }

        private StringContent GetStringContent(object obj)
            => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");

        #endregion Helpers

    }
}