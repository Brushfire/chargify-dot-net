using ChargifyNET.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ChargifyNET
{
    public class RenewalPreview : ChargifyBase, IRenewalPreview, IComparable<RenewalPreview>
    {
        #region Field Keys
        private const string RootElementKey = "renewal_preview";
        private const string NextAssessmentAtKey = "next_assessment_at";
        private const string ExistingBalanceInCentsKey = "existing_balance_in_cents";
        private const string SubtotalInCentsKey = "subtotal_in_cents";
        private const string TotalDiscountInCentsKey = "total_discount_in_cents";
        private const string TotalTaxInCentsKey = "total_tax_in_cents";
        private const string TotalInCentsKey = "total_in_cents";
        private const string TotalAmountDueInCentsKey = "total_amount_due_in_cents";
        private const string UncalculatedTaxesKey = "uncalculated_taxes";
        private const string LineItemsKey = "line_items";
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.  Values set to default
        /// </summary>
        public RenewalPreview() : base()
        {
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="renewalPreviewXml">XML containing renewal preview info (in expected format)</param>
        public RenewalPreview(string renewalPreviewXml) : base()
        {
            // get the XML into an XML document
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(renewalPreviewXml);
            if (doc.ChildNodes.Count == 0) throw new ArgumentException("XML not valid", "renewalPreviewXml");
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
            throw new ArgumentException("XML does not contain renewal preview information", "renewalPreviewXml");
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="subscriptionNode">XML containing billing manifest info (in expected format)</param>
        internal RenewalPreview(string rootElementName, XmlNode renewalPreviewNode) : base()
        {
            if (renewalPreviewNode == null) throw new ArgumentNullException("renewalPreviewNode");
            if (renewalPreviewNode.Name != rootElementName) throw new ArgumentException("Not a vaild renewal preview node", "renewalPreviewNode");
            if (renewalPreviewNode.ChildNodes.Count == 0) throw new ArgumentException("XML not valid", "renewalPreviewNode");
            this.LoadFromNode(renewalPreviewNode);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="renewlPreviewObject">JsonObject containing renewal preview info (in expected format)</param>
        public RenewalPreview(JsonObject renewlPreviewObject)
            : base()
        {
            if (renewlPreviewObject == null) throw new ArgumentNullException("renewlPreviewObject");
            if (renewlPreviewObject.Keys.Count <= 0) throw new ArgumentException("Not a vaild renewal preview node", "renewlPreviewObject");
            this.LoadFromJSON(renewlPreviewObject);
        }

        /// <summary>
        /// Load data from a JsonObject
        /// </summary>
        /// <param name="renewlPreviewObject">The JsonObject containing renewal preview data</param>
        private void LoadFromJSON(JsonObject renewlPreviewObject)
        {
            foreach (string key in renewlPreviewObject.Keys)
            {
                switch (key)
                {
                    case NextAssessmentAtKey:
                        _nextAssessmentAt = renewlPreviewObject.GetJSONContentAsDateTime(key);
                        break;
                    case ExistingBalanceInCentsKey:
                        _existingBalanceInCents = renewlPreviewObject.GetJSONContentAsInt(key);
                        break;
                    case SubtotalInCentsKey:
                        _subtotalInCents = renewlPreviewObject.GetJSONContentAsInt(key);
                        break;
                    case TotalDiscountInCentsKey:
                        _totalDiscountInCents = renewlPreviewObject.GetJSONContentAsInt(key);
                        break;
                    case TotalTaxInCentsKey:
                        _totalTaxInCents = renewlPreviewObject.GetJSONContentAsInt(key);
                        break;
                    case TotalInCentsKey:
                        _totalInCents = renewlPreviewObject.GetJSONContentAsInt(key);
                        break;
                    case TotalAmountDueInCentsKey:
                        _totalAmountDueInCents = renewlPreviewObject.GetJSONContentAsInt(key);
                        break;
                    case UncalculatedTaxesKey:
                        _uncalculatedTaxes = renewlPreviewObject.GetJSONContentAsBoolean(key);
                        break;
                    case LineItemsKey:
                        _lineItems = renewlPreviewObject.GetJSONContentAsResults<ILineItem>(key, x => new LineItem(x));
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Load data from a rewnewal preview node.
        /// </summary>
        /// <param name="renewalPreviewNode">The rewnewal preview node</param>
        private void LoadFromNode(XmlNode renewalPreviewNode)
        {
            foreach (XmlNode dataNode in renewalPreviewNode.ChildNodes)
            {
                switch (dataNode.Name)
                {
                    case NextAssessmentAtKey:
                        _nextAssessmentAt = dataNode.GetNodeContentAsDateTime();
                        break;
                    case ExistingBalanceInCentsKey:
                        _existingBalanceInCents = dataNode.GetNodeContentAsInt();
                        break;
                    case SubtotalInCentsKey:
                        _subtotalInCents = dataNode.GetNodeContentAsInt();
                        break;
                    case TotalDiscountInCentsKey:
                        _totalDiscountInCents = dataNode.GetNodeContentAsInt();
                        break;
                    case TotalTaxInCentsKey:
                        _totalTaxInCents = dataNode.GetNodeContentAsInt();
                        break;
                    case TotalInCentsKey:
                        _totalInCents = dataNode.GetNodeContentAsInt();
                        break;
                    case TotalAmountDueInCentsKey:
                        _totalAmountDueInCents = dataNode.GetNodeContentAsInt();
                        break;
                    case UncalculatedTaxesKey:
                        _uncalculatedTaxes = dataNode.GetNodeContentAsBoolean();
                        break;
                    case LineItemsKey:
                        _lineItems = dataNode.GetNodeContentAsResults<ILineItem>("line_item", x => new LineItem(x));
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region IRenewalPreview Members

        /// <summary>
        /// The timestamp for the next renewal billing date.
        /// </summary>
        public DateTime NextAssessmentAt
        {
            get
            {
                return _nextAssessmentAt;
            }
        }

        private DateTime _nextAssessmentAt = DateTime.MinValue;
        
        /// <summary>
        /// Gets the existing balance in cents.
        /// </summary>
        /// <value>
        /// The existing balance in cents.
        /// </value>
        public int ExistingBalanceInCents
        {
            get
            {
                return _existingBalanceInCents;
            }
        }

        private int _existingBalanceInCents;

        /// <summary>
        /// Gets the existing balance.
        /// </summary>
        /// <value>
        /// The existing balance.
        /// </value>
        public decimal ExistingBalance
        {
            get
            {
                return Convert.ToDecimal(this._existingBalanceInCents) / 100;
            }
        }

        /// <summary>
        /// Gets the subtotal in cents.
        /// </summary>
        /// <value>
        /// The subtotal in cents.
        /// </value>
        public int SubtotalInCents
        {
            get
            {
                return _subtotalInCents;
            }
        }

        private int _subtotalInCents;

        /// <summary>
        /// Gets the subtotal.
        /// </summary>
        /// <value>
        /// The subtotal.
        /// </value>
        public decimal Subtotal
        {
            get
            {
                return Convert.ToDecimal(this._subtotalInCents) / 100;
            }
        }

        /// <summary>
        /// Gets the total discount in cents.
        /// </summary>
        /// <value>
        /// The total discount in cents.
        /// </value>
        public int TotalDiscountInCents
        {
            get
            {
                return _totalDiscountInCents;
            }
        }

        private int _totalDiscountInCents;

        /// <summary>
        /// Gets the total discount.
        /// </summary>
        /// <value>
        /// The total discount.
        /// </value>
        public decimal TotalDiscount
        {
            get
            {
                return Convert.ToDecimal(this._totalDiscountInCents) / 100;
            }
        }

        /// <summary>
        /// Gets the total tax in cents.
        /// </summary>
        /// <value>
        /// The total tax in cents.
        /// </value>
        public int TotalTaxInCents
        {
            get
            {
                return _totalTaxInCents;
            }
        }

        private int _totalTaxInCents;

        /// <summary>
        /// Gets the total tax.
        /// </summary>
        /// <value>
        /// The total tax.
        /// </value>
        public decimal TotalTax
        {
            get
            {
                return Convert.ToDecimal(this._totalTaxInCents) / 100;
            }
        }

        /// <summary>
        /// Gets the total in cents.
        /// </summary>
        /// <value>
        /// The total in cents.
        /// </value>
        public int TotalInCents
        {
            get
            {
                return _totalInCents;
            }
        }

        private int _totalInCents;

        /// <summary>
        /// Gets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public decimal Total
        {
            get
            {
                return Convert.ToDecimal(this._totalInCents) / 100;
            }
        }

        /// <summary>
        /// An integer representing the total amount due at the time of billing.
        /// </summary>
        public int TotalAmountDueInCents
        {
            get
            {
                return _totalAmountDueInCents;
            }
        }

        private int _totalAmountDueInCents;

        /// <summary>
        /// A decimal representing the total amount due at the time of billing.
        /// </summary>
        public decimal TotalAmountDue
        {
            get
            {
                return Convert.ToDecimal(this._totalAmountDueInCents) / 100;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [uncalculated taxes].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [uncalculated taxes]; otherwise, <c>false</c>.
        /// </value>
        public bool UncalculatedTaxes
        {
            get
            {
                return _uncalculatedTaxes;
            }
        }

        private bool _uncalculatedTaxes;

        /// <summary>
        /// Gets the line items.
        /// </summary>
        /// <value>
        /// The line items.
        /// </value>
        public List<ILineItem> LineItems
        {
            get
            {
                return _lineItems;
            }
        }

        private List<ILineItem> _lineItems = null;

        #endregion

        #region Operators

        /// <summary>
        /// Equals operator for two renewal previews.
        /// </summary>
        /// <returns>True if the renewal previews are equal</returns>
        public static bool operator ==(RenewalPreview a, RenewalPreview b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b)) { return true; }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null)) { return false; }

            return a.NextAssessmentAt == b.NextAssessmentAt
                && a.ExistingBalanceInCents == b.ExistingBalanceInCents
                && a.TotalAmountDueInCents == b.TotalAmountDueInCents
                && a.SubtotalInCents == b.SubtotalInCents
                && a.TotalDiscountInCents == b.TotalDiscountInCents
                && a.TotalInCents == b.TotalInCents
                && a.TotalTaxInCents == b.TotalTaxInCents
                && a.UncalculatedTaxes == b.UncalculatedTaxes;
        }

        /// <summary>
        /// Equals operator for two renewal previews.
        /// </summary>
        /// <returns>True if the renewal previews are equal</returns>
        public static bool operator ==(RenewalPreview a, IRenewalPreview b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b)) { return true; }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null)) { return false; }

            return a.NextAssessmentAt == b.NextAssessmentAt
                && a.ExistingBalanceInCents == b.ExistingBalanceInCents
                && a.TotalAmountDueInCents == b.TotalAmountDueInCents
                && a.SubtotalInCents == b.SubtotalInCents
                && a.TotalDiscountInCents == b.TotalDiscountInCents
                && a.TotalInCents == b.TotalInCents
                && a.TotalTaxInCents == b.TotalTaxInCents
                && a.UncalculatedTaxes == b.UncalculatedTaxes;
        }

        /// Equals operator for two renewal previews.
        /// </summary>
        /// <returns>True if the renewal previews are equal</returns>
        public static bool operator ==(IRenewalPreview a, RenewalPreview b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b)) { return true; }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null)) { return false; }

            return a.NextAssessmentAt == b.NextAssessmentAt
                && a.ExistingBalanceInCents == b.ExistingBalanceInCents
                && a.TotalAmountDueInCents == b.TotalAmountDueInCents
                && a.SubtotalInCents == b.SubtotalInCents
                && a.TotalDiscountInCents == b.TotalDiscountInCents
                && a.TotalInCents == b.TotalInCents
                && a.TotalTaxInCents == b.TotalTaxInCents
                && a.UncalculatedTaxes == b.UncalculatedTaxes;
        }

        /// <summary>
        /// Not Equals operator for two renewal previews.
        /// </summary>
        /// <returns>True if the renewal previews are not equal</returns>
        public static bool operator !=(RenewalPreview a, RenewalPreview b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Not Equals operator for two renewal previews.
        /// </summary>
        /// <returns>True if the renewal previews are not equal</returns>
        public static bool operator !=(RenewalPreview a, IRenewalPreview b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Not Equals operator for two renewal previews.
        /// </summary>
        /// <returns>True if the renewal previews are not equal</returns>
        public static bool operator !=(IRenewalPreview a, RenewalPreview b)
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

            if (typeof(IRenewalPreview).IsAssignableFrom(obj.GetType()))
            {
                var castObj = obj as IRenewalPreview;
                return this.NextAssessmentAt == castObj.NextAssessmentAt
                    && this.ExistingBalanceInCents == castObj.ExistingBalanceInCents
                    && this.TotalAmountDueInCents == castObj.TotalAmountDueInCents
                    && this.SubtotalInCents == castObj.SubtotalInCents
                    && this.TotalDiscountInCents == castObj.TotalDiscountInCents
                    && this.TotalInCents == castObj.TotalInCents
                    && this.TotalTaxInCents == castObj.TotalTaxInCents
                    && this.UncalculatedTaxes == castObj.UncalculatedTaxes;
            }
            else
            {
                return base.Equals(obj);
            }
        }

        /// <summary>
        /// Returns a string representation of the Billing Manifest object.
        /// </summary>
        public override string ToString()
        {
            return string.Format("Renewal Preview: {0:C0}", this.Total);
        }

        #endregion

        #region IComparable<IRenewalPreview> Members

        /// <summary>
        /// Compare this instance to another.
        /// </summary>
        /// <param name="other">The other instance</param>
        /// <returns>The result of the comparison</returns>
        public int CompareTo(IRenewalPreview other)
        {
            return this.TotalInCents.CompareTo(other.TotalInCents);
        }

        #endregion

        #region IComparable<IRenewalPreview> Members

        /// <summary>
        /// Compare this instance to another.
        /// </summary>
        /// <param name="other">The other instance</param>
        /// <returns>The result of the comparison</returns>
        public int CompareTo(RenewalPreview other)
        {
            return this.TotalInCents.CompareTo(other.TotalInCents);
        }

        #endregion
    }
}