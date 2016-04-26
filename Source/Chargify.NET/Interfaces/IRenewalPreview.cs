using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChargifyNET
{
    /// <summary>
    /// Represents an interface for a renewal preview.
    /// </summary>
    public interface IRenewalPreview : IComparable<IRenewalPreview>
    {
        /// <summary>
        /// The timestamp for the next renewal billing date.
        /// </summary>
        DateTime NextAssessmentAt { get; }
        
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
        /// An integer representing the total amount due at the time of billing.
        /// </summary>
        int TotalAmountDueInCents { get; }

        /// <summary>
        /// A decimal representing the total amount due at the time of billing.
        /// </summary>
        decimal TotalAmountDue { get; }

        /// <summary>
        /// Gets a value indicating whether [uncalculated taxes].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [uncalculated taxes]; otherwise, <c>false</c>.
        /// </value>
        bool UncalculatedTaxes { get; }

        /// <summary>
        /// An array of objects representing the individual transactions that would be created for this subscription.
        /// </summary>
        List<ILineItem> LineItems { get; }
    }
}
