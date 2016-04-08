using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChargifyNET
{
    /// <summary>
    /// Represents an interface for a tax.
    /// </summary>
    public interface ITaxation : IComparable<ITaxation>
    {
        /// <summary>
        /// The tax rate.
        /// </summary>
        string Rate { get; }

        /// <summary>
        /// An integer representing the tax amount.
        /// </summary>
        int TaxAmountInCents { get; }

        /// <summary>
        /// A decimal representing the taxed amount.
        /// </value>
        decimal TaxAmount { get; }

        /// <summary>
        /// The tax ID.
        /// </summary>
        string TaxId { get; }

        /// <summary>
        /// The name of the tax.
        /// </summary>
        string TaxName { get; }

        /// <summary>
        /// A list of tax rules.
        /// </summary>
        List<string> TaxRules { get; }
    }
}
