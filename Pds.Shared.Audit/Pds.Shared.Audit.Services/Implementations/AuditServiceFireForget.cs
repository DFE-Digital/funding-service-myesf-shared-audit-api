using Pds.Core.Logging;
using Pds.Shared.Audit.Services.Interfaces;
using System;
using ServiceModel = Pds.Shared.Audit.Services.Models;


namespace Pds.Shared.Audit.Services.Implementations
{
    /// <summary>
    /// Audit service fire and forget.
    /// </summary>
    public class AuditServiceFireForget : IAuditServiceFireForget
    {
        private readonly IFireForgetEventHandler _fireForgetEventHandler;
        private readonly ILoggerAdapter<AuditServiceFireForget> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditServiceFireForget"/> class.
        /// </summary>
        /// <param name="fireForgetEventHandler">IFireForgetEventHandler.</param>
        /// <param name="logger">The logger.</param>
        public AuditServiceFireForget(IFireForgetEventHandler fireForgetEventHandler, ILoggerAdapter<AuditServiceFireForget> logger)
        {
            _fireForgetEventHandler = fireForgetEventHandler;
            _logger = logger;
        }

        /// <summary>
        /// Accept a fire and forget audit message.
        /// </summary>
        /// <param name="message">Audit.</param>
        public void Accept(ServiceModel.Audit message)
        {
            _fireForgetEventHandler.Execute(async a =>
            {
                try
                {
                    await a.CreateAsync(message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Unexpected error during fire and forget audit creation of audit message : {message.Message}.");
                }
            });
        }
    }
}