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
        /// </summary>
        DateTime StartDate { get; }

        /// <summary>
        /// The timestamp for the end of the period covered by the manifest.
        /// </summary>
        DateTime EndDate { get; }

        /// <summary>
        /// The type of billing period: recurring for previews.
        /// </summary>
        PeriodType PeriodType { get; }

        /// <summary>
        /// An integer representing the amount of the subscription’s current balance. Will be zero since the subscription does not yet exist.
        /// </summary>
        int ExistingBalanceInCents { get; }

        /// <summary>
        /// A decimal representing the subscription's existing balance.
        /// </summary>
        decimal ExistingBalance { get; }

        /// <summary>
        /// An integer representing the amount of the total pre-tax, pre-discount charges that would be assessed.
        /// </summary>
        int SubtotalInCents { get; }

        /// <summary>
        /// A decimal representing the subscription's subtotal.
        /// </summary>
        decimal Subtotal { get; }

        /// <summary>
        /// An integer representing the amount of the coupon discounts that would be applied.
        /// </summary>
        int TotalDiscountInCents { get; }

        /// <summary>
        /// A decimal representing the subscription's total discount.
        /// </summary>
        decimal TotalDiscount { get; }

        /// <summary>
        /// An integer representing the total tax charges that would be assessed.
        /// </summary>
        int TotalTaxInCents { get; }

        /// <summary>
        /// A decimal representing the subscription's total tax.
        /// </summary>
        decimal TotalTax { get; }

        /// <summary>
        /// An integer representing the total amount owed, less any discounts, that would be assessed.
        /// </summary>
        int TotalInCents { get; }

        /// <summary>
        /// A decimal representing the subscription's total.
        /// </summary>
        decimal Total { get; }

        /// <summary>
        /// An array of objects representing the individual transactions that would be created for this subscription.
        /// </summary>
        List<ILineItem> LineItems { get; }
    }
}
