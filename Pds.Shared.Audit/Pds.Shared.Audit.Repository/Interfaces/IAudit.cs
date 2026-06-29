using System;
using System.ComponentModel.DataAnnotations;

namespace Pds.Shared.Audit.Repository.Interfaces
{
    /// <summary>
    /// Repository audit model interface.
    /// </summary>
    public interface IAudit
    {
        /// <summary>
        /// Gets or sets the severity.
        /// </summary>
        /// <value>
        /// The severity.
        /// </value>
        [Required]
        int Severity { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        [Required]
        string User { get; set; }

        /// <summary>
        /// Gets or sets the ukprn.
        /// </summary>
        /// <value>
        /// The ukprn.
        /// </value>
        int? Ukprn { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [Required]
        string Message { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        [Required]
        int Action { get; set; }
    }
}