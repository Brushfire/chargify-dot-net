using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChargifyNET
{
    /// <summary>
    /// Represents an enumeration of period types.
    /// </summary>
    public enum PeriodType
    {
        /// <summary>
        /// The 'recurring' period type
        /// </summary>
        Recurring,
        /// <summary>
        /// The 'unknown' period type, only internal to this wrapper
        /// </summary>
        Unknown = -1
    }

    /// <summary>
    /// Interface represents a billing manifest.
    /// </summary>
    public interface IBillingManifest : IComparable<IBillingManifest>
    {
        /// <summary>
        /// The timestamp for the beginning of the period covered by the manifest.
        /// </value>
        DateTime StartDate { get; }

        /// <summary>
        /// The timestamp for the end of the period covered by the manifest.
        /// </value>
        DateTime EndDate { get; }

        /// <summary>
        /// The type of billing period: recurring for previews.
        /// </value>
        PeriodType PeriodType { get; }

        /// <summary>
        /// An integer representing the amount of the subscription’s current balance. Will be zero since the subscription does not yet exist.
        /// </value>
        int ExistingBalanceInCents { get; }

        /// <summary>
        /// An integer representing the amount of the total pre-tax, pre-discount charges that would be assessed.
        /// </value>
        int SubtotalInCents { get; }

        /// <summary>
        /// An integer representing the amount of the coupon discounts that would be applied.
        /// </value>
        int TotalDiscountInCents { get; }

        /// <summary>
        /// An integer representing the total tax charges that would be assessed.
        /// </value>
        int TotalTaxInCents { get; }

        /// <summary>
        /// An integer representing the total amount owed, less any discounts, that would be assessed.
        /// </value>
        int TotalInCents { get; }

        /// <summary>
        /// An array of objects representing the individual transactions that would be created for this subscription.
        /// </value>
        List<ILineItem> LineItems { get; }
    }
}
