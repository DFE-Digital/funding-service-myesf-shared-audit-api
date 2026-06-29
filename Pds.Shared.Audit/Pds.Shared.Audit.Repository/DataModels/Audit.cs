using Pds.Shared.Audit.Repository.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Pds.Shared.Audit.Repository.DataModels
{
    /// <summary>
    /// Reverse engineered Audit data model.
    /// </summary>
    public partial class Audit : IAudit
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the severity.
        /// </summary>
        /// <value>
        /// The severity.
        /// </value>
        [Required]
        public int Severity { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        [Required]
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the ukprn.
        /// </summary>
        /// <value>
        /// The ukprn.
        /// </value>
        public int? Ukprn { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [Required]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        [Required]
        public int Action { get; set; }
    }
}