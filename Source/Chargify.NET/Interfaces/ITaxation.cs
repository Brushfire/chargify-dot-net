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
        /// </value>
        string Rate { get; }

        /// <summary>
        /// An integer representing the tax amount.
        /// </value>
        int TaxAmountInCents { get; }

        /// <summary>
        /// The tax ID.
        /// </value>
        string TaxId { get; }

        /// <summary>
        /// The name of the tax.
        /// </value>
        string TaxName { get; }

        /// <summary>
        /// A list of tax rules.
        /// </value>
        List<string> TaxRules { get; }
    }
}
