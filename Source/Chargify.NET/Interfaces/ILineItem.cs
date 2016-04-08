using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChargifyNET
{
    /// <summary>
    /// Represents an interface for a billing manifest line item.
    /// </summary>
    public interface ILineItem : IComparable<ILineItem>
    {
        /// <summary>
        /// An integer representing the line amount.
        /// </summary>
        int AmountInCents { get; }

        /// <summary>
        /// A decimal representing the line item's amount.
        /// </value>
        decimal Amount { get; }

        /// <summary>
        /// An integer representing the discount aount.
        /// </summary>
        int DiscountAmountInCents { get; }

        /// <summary>
        /// A decimal representing the line item's discount amount.
        /// </summary>
        decimal DiscountAmount { get; }

        /// <summary>
        /// The kind of transaction charge.
        /// </summary>
        TransactionChargeKind Kind { get; }

        /// <summary>
        /// A string representing the purpose of the line item.
        /// </summary>
        string Memo { get; }

        /// <summary>
        /// An integer representing the taxable amount of the line item.
        /// </summary>
        int TaxableAmountInCents { get; }

        /// <summary>
        /// A decimal representing the line item's taxable amount.
        /// </summary>
        decimal TaxableAmount { get; }

        /// <summary>
        /// A list of taxations.
        /// </summary>
        List<ITaxation> Taxations { get; }

        /// <summary>
        /// The type of transaction described by the line item.
        /// </summary>
        TransactionType TransactionType { get; }
    }
}
