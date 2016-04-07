using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChargifyNET
{
    /// <summary>
    /// Represents an interface for a subscription preview.
    /// </summary>
    public interface ISubscriptionPreview : IComparable<ISubscriptionPreview>
    {
        /// <summary>
        /// The current billing manifest.
        /// </value>
        IBillingManifest CurrentBillingManifest { get; }

        /// <summary>
        /// The next billing manifest.
        /// </value>
        IBillingManifest NextBillingManifest { get; }
    }
}
