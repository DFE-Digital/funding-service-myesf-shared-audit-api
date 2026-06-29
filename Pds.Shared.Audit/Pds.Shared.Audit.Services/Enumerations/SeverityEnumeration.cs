using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pds.Shared.Audit.Services.Enumerations
{
    /// <summary>
    /// Severity level of Audit record.
    /// </summary>
    public enum SeverityLevel
    {
        /// <summary>
        /// Severity for general information audit.
        /// </summary>
        [Display(Name = "Information")]
        Information = 0,

        /// <summary>
        /// Severity level for warnings.
        /// </summary>
        [Display(Name = "Warning")]
        Warning = 1,

        /// <summary>
        /// Severity level for errors.
        /// </summary>
        [Display(Name = "Error")]
        Error = 2
    }
}
