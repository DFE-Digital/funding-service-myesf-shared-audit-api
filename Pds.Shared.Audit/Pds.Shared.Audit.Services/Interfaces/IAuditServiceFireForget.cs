namespace Pds.Shared.Audit.Services.Interfaces
{
    /// <summary>
    /// Audit service fire and forget interface.
    /// </summary>
    public interface IAuditServiceFireForget
    {
        /// <summary>
        /// Accept.
        /// </summary>
        /// <param name="message">Audit.</param>
        void Accept(Models.Audit message);
    }
}