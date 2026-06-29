using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pds.Core.Logging;
using Pds.Shared.Audit.Services.Interfaces;
using System.Threading.Tasks;
using ServiceModels = Pds.Shared.Audit.Services.Models;

namespace Pds.Shared.Audit.Api.Controllers
{
    /// <summary>
    /// The audit controller.
    /// </summary>
    public class AuditController : BaseApiController
    {
        private readonly ILoggerAdapter<AuditController> _logger;

        private readonly IAuditService _auditService;

        private readonly IAuditServiceFireForget _auditServiceFireForget;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="auditService">The audit service.</param>
        /// <param name="auditServiceFireForget">The audit Service Fire Forget.</param>
        public AuditController(
            ILoggerAdapter<AuditController> logger,
            IAuditService auditService,
            IAuditServiceFireForget auditServiceFireForget)
        {
            _logger = logger;
            _auditService = auditService;
            _auditServiceFireForget = auditServiceFireForget;
        }

        /// <summary>
        /// Creates a new audit, Fire and forget manner.
        /// </summary>
        /// <param name="request">Audit model.</param>
        /// <returns>Returns an IActionResult.</returns>
        [HttpPost("/api/auditasync")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public IActionResult Accept(ServiceModels.Audit request)
        {
            _auditServiceFireForget.Accept(request);
            return StatusCode(StatusCodes.Status202Accepted);
        }

        /// <summary>
        /// Creates a new audit Asynchronously.
        /// </summary>
        /// <param name="request">Audit model.</param>
        /// <returns>Returns an IActionResult.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> CreateAsync(ServiceModels.Audit request)
        {
            await _auditService.CreateAsync(request);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}