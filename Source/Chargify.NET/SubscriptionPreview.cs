using ChargifyNET.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ChargifyNET
{
    /// <summary>
    /// Represents a preview of a subscription.
    /// </summary>
    public class SubscriptionPreview : ChargifyBase, ISubscriptionPreview, IComparable<SubscriptionPreview>
    {
        #region Field Keys
        private const string RootElementKey = "subscription_preview";
        private const string CurrentBillingManifestKey = "current_billing_manifest";
        private const string NextBillingManifestKey = "next_billing_manifest";
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.  Values set to default
        /// </summary>
        public SubscriptionPreview() : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="subscriptionPreviewXml">XML containing subscription preview info (in expected format)</param>
        public SubscriptionPreview(string subscriptionPreviewXml) : base()
        {
            // get the XML into an XML document
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(subscriptionPreviewXml);
            if (doc.ChildNodes.Count == 0) throw new ArgumentException("XML not valid", "subscriptionPreviewXml");
            // loop through the child nodes of this node
            foreach (XmlNode elementNode in doc.ChildNodes)
            {
                if (elementNode.Name == RootElementKey)
                {
                    this.LoadFromNode(elementNode);
                    return;
                }
            }
            // if we get here, then no customer info was found
            throw new ArgumentException("XML does not contain subscription preview information", "subscriptionPreviewXml");
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="subscriptionNode">XML containing subscription preview info (in expected format)</param>
        internal SubscriptionPreview(XmlNode subscriptionPreviewNode) : base()
        {
            if (subscriptionPreviewNode == null) throw new ArgumentNullException("subscriptionPreviewNode");
            if (subscriptionPreviewNode.Name != RootElementKey) throw new ArgumentException("Not a vaild subscription preview node", "subscriptionPreviewNode");
            if (subscriptionPreviewNode.ChildNodes.Count == 0) throw new ArgumentException("XML not valid", "subscriptionPreviewNode");
            this.LoadFromNode(subscriptionPreviewNode);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="subscriptionPreviewObject">JsonObject containing subscription preview info (in expected format)</param>
        public SubscriptionPreview(JsonObject subscriptionPreviewObject)
            : base()
        {
            if (subscriptionPreviewObject == null) throw new ArgumentNullException("subscriptionPreviewObject");
            if (subscriptionPreviewObject.Keys.Count <= 0) throw new ArgumentException("Not a vaild subscription preview node", "subscriptionPreviewObject");
            this.LoadFromJSON(subscriptionPreviewObject);
        }

        /// <summary>
        /// Load data from a JsonObject
        /// </summary>
        /// <param name="subscriptionPreviewObject">The JsonObject containing subscription preview data</param>
        private void LoadFromJSON(JsonObject subscriptionPreviewObject)
        {
            foreach (string key in subscriptionPreviewObject.Keys)
            {
                switch (key)
                {
                    case CurrentBillingManifestKey:
                        _currentBillingManifest = subscriptionPreviewObject.GetJSONContentAsResult(key, x => new BillingManifest(x));
                        break;
                    case NextBillingManifestKey:
                        _nextBillingManifest = subscriptionPreviewObject.GetJSONContentAsResult(key, x => new BillingManifest(x));
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Load data from a subscription preview node.
        /// </summary>
        /// <param name="subscriptionPreviewNode">The subscription preview node</param>
        private void LoadFromNode(XmlNode subscriptionPreviewNode)
        {
            foreach (XmlNode dataNode in subscriptionPreviewNode.ChildNodes)
            {
                switch (dataNode.Name)
                {
                    case CurrentBillingManifestKey:
                        _currentBillingManifest = dataNode.GetNodeContentAsResult(x => new BillingManifest("current_billing_manifest", x));
                        break;
                    case NextBillingManifestKey:
                        _nextBillingManifest = dataNode.GetNodeContentAsResult(x => new BillingManifest("next_billing_manifest", x));
                        break;
                    default:
                        break;

                }
            }
        }

        #endregion

        #region ISubscriptionPreview Members

        /// <summary>
        /// Gets the current billing manifest.
        /// </summary>
        /// <value>
        /// The current billing manifest.
        /// </value>
        public IBillingManifest CurrentBillingManifest
        {
            get
            {
                return _currentBillingManifest;
            }
        }

        private IBillingManifest _currentBillingManifest = null;

        /// <summary>
        /// Gets the next billing manifest.
        /// </summary>
        /// <value>
        /// The next billing manifest.
        /// </value>
        public IBillingManifest NextBillingManifest
        {
            get
            {
                return _nextBillingManifest;
            }
        }

        private IBillingManifest _nextBillingManifest = null;

        #endregion

        #region Operators

        /// <summary>
        /// Equals operator for two subscription previews.
        /// </summary>
        /// <returns>True if the subscription previews are equal</returns>
        public static bool operator ==(SubscriptionPreview a, SubscriptionPreview b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b)) { return true; }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null)) { return false; }

            return (a.CurrentBillingManifest == b.CurrentBillingManifest
                && a.NextBillingManifest == b.NextBillingManifest);
        }

        /// <summary>
        /// Equals operator for two subscription previews.
        /// </summary>
        /// <returns>True if the subscription previews are equal</returns>
        public static bool operator ==(SubscriptionPreview a, ISubscriptionPreview b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b)) { return true; }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null)) { return false; }

            return (a.CurrentBillingManifest == b.CurrentBillingManifest
                && a.NextBillingManifest == b.NextBillingManifest);
        }

        /// <summary>
        /// Equals operator for two subscription previews.
        /// </summary>
        /// <returns>True if the subscription previews are equal</returns>
        public static bool operator ==(ISubscriptionPreview a, SubscriptionPreview b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b)) { return true; }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null)) { return false; }

            return (a.CurrentBillingManifest == b.CurrentBillingManifest
                && a.NextBillingManifest == b.NextBillingManifest);
        }

        /// <summary>
        /// Not Equals operator for two subscription previews.
        /// </summary>
        /// <returns>True if the subscription previews are not equal</returns>
        public static bool operator !=(SubscriptionPreview a, SubscriptionPreview b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Not Equals operator for two subscription previews.
        /// </summary>
        /// <returns>True if the subscription previews are not equal</returns>
        public static bool operator !=(SubscriptionPreview a, ISubscriptionPreview b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Not Equals operator for two subscription previews.
        /// </summary>
        /// <returns>True if the subscription previews are not equal</returns>
        public static bool operator !=(ISubscriptionPreview a, SubscriptionPreview b)
        {
            return !(a == b);
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Get Hash code
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj"></param>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (typeof(ISubscriptionPreview).IsAssignableFrom(obj.GetType()))
            {
                var castObj = obj as ISubscriptionPreview;
                return (this.CurrentBillingManifest == castObj.CurrentBillingManifest
                    && this.NextBillingManifest == castObj.NextBillingManifest);
            }
            else
            {
                return base.Equals(obj);
            }
        }

        /// <summary>
        /// Returns a string representation of the Subscription Preview object.
        /// </summary>
        public override string ToString()
        {
            // TODO: this needs a better implementation, but the subscription preview object is not very expressive
            return "Subscription Preview";
        }

        #endregion

        #region IComparable<ISubscriptionPreview> Members

        /// <summary>
        /// Compare this instance to another.
        /// </summary>
        /// <param name="other">The other instance</param>
        /// <returns>The result of the comparison</returns>
        public int CompareTo(ISubscriptionPreview other)
        {
            int result = this.CurrentBillingManifest.CompareTo(other.CurrentBillingManifest);
            if (result == 0)
            {
                return this.NextBillingManifest.CompareTo(other.NextBillingManifest);
            }
            else
            {
                return result;
            }
        }

        #endregion

        #region IComparable<SubscriptionPreview> Members

        /// <summary>
        /// Compare this instance to another.
        /// </summary>
        /// <param name="other">The other instance</param>
        /// <returns>The result of the comparison</returns>
        public int CompareTo(SubscriptionPreview other)
        {
            int result = this.CurrentBillingManifest.CompareTo(other.CurrentBillingManifest);
            if (result == 0)
            {
                return this.NextBillingManifest.CompareTo(other.NextBillingManifest);
            }
            else
            {
                return result;
            }
        }

        #endregion
    }
}
