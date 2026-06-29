using System;
using System.ComponentModel.DataAnnotations;

namespace Pds.Shared.Audit.Services.Enumerations
{
    /// <summary>
    /// Action type for the audit record.
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// Audit record action type for contract approvals.
        /// </summary>
        [Display(Name = "Contract Approved")]
        ContractApproved = 0,

        /// <summary>
        /// Audit record action type for contract feed reads.
        /// </summary>
        [Display(Name = "Contract Feed Read")]
        ContractFeedRead = 1,

        /// <summary>
        /// Audit record action type for contract withdrawals.
        /// </summary>
        [Display(Name = "Contract Withdrawal")]
        ContractWithdrawal = 2,

        /// <summary>
        /// Audit record action type for when a contract has been created.
        /// </summary>
        [Display(Name = "Contract Created")]
        ContractCreated = 3,

        /// <summary>
        /// Audit record action type for when a contract has an email reminder sent.
        /// </summary>
        [Obsolete]
        [Display(Name = "Contract Email Reminder Created")]
        ContractEmailReminderCreated = 4,

        /// <summary>
        /// Audit record action type for when a contract has been replaced.
        /// </summary>
        [Display(Name = "Contract Replaced")]
        ContractReplaced = 5,

        /// <summary>
        /// Audit record action type for approvals for contracts.
        /// </summary>
        [Display(Name = "Contract Confirm Approval")]
        ContractConfirmApproval = 6,

        /// <summary>
        /// Audit record when the funding claim created.
        /// </summary>
        [Display(Name = "Funding Claim Created")]
        FundingClaimCreated = 7,

        /// <summary>
        /// Audit record when the funding claim is approved.
        /// </summary>
        [Display(Name = "Funding Claim Approved")]
        FundingClaimApproved = 8,

        /// <summary>
        /// Audit record when the funding claim is replaced.
        /// </summary>
        [Display(Name = "Funding Claim Replaced")]
        FundingClaimReplaced = 9,

        /// <summary>
        /// Audit record when the funding claim is withdrawn.
        /// </summary>
        [Display(Name = "Funding Claim Withdrawn")]
        FundingClaimWithdrawn = 10,

        /// <summary>
        /// Audit record when the contract is manually signed in FCS.
        /// </summary>
        [Display(Name = "Contract Manual Approval")]
        ContractManualApproval = 11,

        /// <summary>
        /// Audit record action type for reconciliation feed reads.
        /// </summary>
        [Display(Name = "Reconciliation Feed Read")]
        ReconciliationFeedRead = 12,

        /// <summary>
        /// Audit record action type for historic nil declarations.
        /// </summary>
        [Display(Name = "Historic Nil Declaration Submitted")]
        SubcontractorNilDeclarationSubmitted = 13,

        /// <summary>
        /// Audit record action type for new full subcontractor declaration draft created.
        /// </summary>
        [Display(Name = "Full Subcontractor Declaration Created")]
        FullSubcontractorDeclarationCreated = 14,

        /// <summary>
        /// Audit record action type for full subcontractor declaration submitted.
        /// </summary>
        [Display(Name = "Full Subcontractor Declaration Submitted")]
        FullSubcontractorDeclarationSubmitted = 15,

        /// <summary>
        /// Audit record action type for nil subcontractor declaration submitted.
        /// </summary>
        [Display(Name = "Nil Subcontractor Declaration Submitted")]
        NilSubcontractorDeclarationSubmitted = 16,

        /// <summary>
        /// Audit record action type for all allocation statements.
        /// </summary>
        [Display(Name = "Allocation Statements")]
        AllocationStatements = 17,

        /// <summary>
        /// Audit record action type for allocation emails successfully sent.
        /// </summary>
        [Display(Name = "Allocation Email Sent")]
        AllocationEmailSent = 18,

        /// <summary>
        /// Audit record action type for allocation emails not sent.
        /// </summary>
        [Display(Name = "Allocation Email Not Sent")]
        AllocationEmailFailedToSend = 19,

        /// <summary>
        /// Audit record action type for polling of SharePoint.
        /// </summary>
        [Display(Name = "Allocation SharePoint Polling")]
        AllocationSharePointPolling = 20,

        /// <summary>
        /// Audit invalid allocation SharePoint file name.
        /// </summary>
        [Display(Name = "Allocation Invalid SharePoint file name")]
        AllocationInvalidSharePointFileName = 21,

        /// <summary>
        /// Audit record action type for contract ready to sign emails not sent.
        /// </summary>
        [Display(Name = "Contract ready to sign email not sent")]
        ContractReadyToSignEmailNotSent = 22,

        /// <summary>
        /// Audit record action type for contract ready to sign emails sent.
        /// </summary>
        [Display(Name = "Contract ready to sign email sent")]
        ContractReadyToSignEmailSent = 23,

        /// <summary>
        /// Audit record action type for contract ready to review emails not sent.
        /// </summary>
        [Display(Name = "Contract ready to review email not sent")]
        ContractReadyToReviewEmailNotSent = 24,

        /// <summary>
        /// Audit record action type for contract ready to review emails sent.
        /// </summary>
        [Display(Name = "Contract ready to review email sent")]
        ContractReadyToReviewEmailSent = 25,

        /// <summary>
        /// Audit record action type for invalid contract ready to sign emails.
        /// </summary>
        [Display(Name = "Contract ready to sign email not sent and is invalid")]
        ContractReadyToSignEmailInvalid = 26,

        /// <summary>
        /// Audit record action type for invalid contract ready to review emails.
        /// </summary>
        [Display(Name = "Contract ready to review email not sent and is invalid")]
        ContractReadyToReviewEmailInvalid = 27,

        /// <summary>
        /// Audit record action type for when a contract has an email reminder has been queued.
        /// </summary>
        [Display(Name = "Contract Email Reminder Queued")]
        ContractEmailReminderQueued = 28,

        /// <summary>
        /// Audit record action type for when a contract has an email reminder has been sent.
        /// </summary>
        [Display(Name = "Contract Email Reminder sent")]
        ContractEmailReminderSent = 29,

        /// <summary>
        /// Audit record action type for polling contracts that need a reminder
        /// </summary>
        [Display(Name = "Contract Reminder Polling")]
        ContractEmailReminderPolling = 30,

        /// <summary>
        /// Audit record action type for when a contract has an email reminder has been sent.
        /// </summary>
        [Display(Name = "Contract Email Reminder not sent")]
        ContractEmailReminderNotSent = 31,

        /// <summary>
        /// Audit record action type for all LogicApp actions
        /// </summary>
        [Display(Name = "Subcontractor declaration Logic Apps")]
        SubcontractorDeclarationLogicApps = 32,

        /// <summary>
        /// Audit record action type for marking an amended notification contract as read
        /// </summary>
        [Display(Name = "Notification marked as read")]
        NotificationMarkedAsRead = 33,

        /// <summary>
        /// Audit record action type for closing of draft subcontractor declarations.
        /// </summary>
        [Display(Name = "Close draft subcontractor declarations")]
        CloseDraftSubcontractorDeclarations = 34,

        /// <summary>
        /// Audit record action type for emails that have not been sent due to missing role(s).
        /// </summary>
        [Display(Name = "Email not sent as role(s) not found")]
        EmailNotSentAsNoRole = 35,

        /// <summary>
        /// Audit record action type for removal of inactive provisions.
        /// </summary>
        [Display(Name = "Remove inactive provisions from draft declarations")]
        RemoveInactiveProvisionsForDraftDeclarations = 36,

        /// <summary>
        /// Audit record action type for successfully sending a contract Approved Message to the FCS API.
        /// </summary>
        [Display(Name = "Contract Approved Message Sent to FCS API")]
        ContractApprovedMessageSentToFCS = 37,

        /// <summary>
        /// Audit record action type for forwarding a contract notification to a subscriber.
        /// </summary>
        [Display(Name = "Contract notification forwarded")]
        ContractNotificationForwarded = 38,

        /// <summary>
        /// Audit record action type for downloading a Contract PDF from SharePoint.
        /// </summary>
        [Display(Name = "Contract PDF downloaded")]
        ContractPDFDownloaded = 39,

        /// <summary>
        /// Audit record action type for recieving a contract event from the contract feed reader.
        /// </summary>
        [Display(Name = "Contract feed event received")]
        ContractFeedEventReceived = 40,

        /// <summary>
        /// Audit record action type for the feed reader filtering out a Contract event.
        /// </summary>
        [Display(Name = "Contract feed event filtered out")]
        ContractFeedEventFilteredOut = 41,

        /// <summary>
        /// Audit record action type for contract datas from the FCS feed to be saved into blob storage.
        /// </summary>
        [Display(Name = "Contract datas feed extract created in blob storage")]
        ContractDatasBlobCreated = 42
    }
}
