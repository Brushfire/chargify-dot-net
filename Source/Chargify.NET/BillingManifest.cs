using ChargifyNET.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ChargifyNET
{
    public class BillingManifest : ChargifyBase, IBillingManifest, IComparable<BillingManifest>
    {
        #region Field Keys
        private const string StartDateKey = "start_date";
        private const string EndDateKey = "end_date";
        private const string PeriodTypeKey = "period_type";
        private const string ExistingBalanceInCentsKey = "existing_balance_in_cents";
        private const string SubtotalInCentsKey = "subtotal_in_cents";
        private const string TotalDiscountInCentsKey = "total_discount_in_cents";
        private const string TotalTaxInCentsKey = "total_tax_in_cents";
        private const string TotalInCentsKey = "total_in_cents";
        private const string LineItemsKey = "line_items";
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.  Values set to default
        /// </summary>
        public BillingManifest() : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rootElementName">Name of the root element.</param>
        /// <param name="billingManifestXml">XML containing billing manifest info (in expected format)</param>
        public BillingManifest(string rootElementName, string billingManifestXml) : base()
        {
            // get the XML into an XML document
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(billingManifestXml);
            if (doc.ChildNodes.Count == 0) throw new ArgumentException("XML not valid", "billingManifestXml");
            // loop through the child nodes of this node
            foreach (XmlNode elementNode in doc.ChildNodes)
            {
                if (elementNode.Name == rootElementName)
                {
                    this.LoadFromNode(elementNode);
                    return;
                }
            }
            // if we get here, then no customer info was found
            throw new ArgumentException("XML does not contain billing manifest information", "billingManifestXml");
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="subscriptionNode">XML containing billing manifest info (in expected format)</param>
        internal BillingManifest(string rootElementName, XmlNode billingManifestNode) : base()
        {
            if (billingManifestNode == null) throw new ArgumentNullException("billingManifestNode");
            if (billingManifestNode.Name != rootElementName) throw new ArgumentException("Not a vaild billing manifest node", "billingManifestNode");
            if (billingManifestNode.ChildNodes.Count == 0) throw new ArgumentException("XML not valid", "billingManifestNode");
            this.LoadFromNode(billingManifestNode);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="billingManifestObject">JsonObject containing billing object info (in expected format)</param>
        public BillingManifest(JsonObject billingManifestObject)
            : base()
        {
            if (billingManifestObject == null) throw new ArgumentNullException("billingManifestObject");
            if (billingManifestObject.Keys.Count <= 0) throw new ArgumentException("Not a vaild billing manifest node", "billingManifestObject");
            this.LoadFromJSON(billingManifestObject);
        }

        /// <summary>
        /// Load data from a JsonObject
        /// </summary>
        /// <param name="billingManifestObject">The JsonObject containing subscription preview data</param>
        private void LoadFromJSON(JsonObject billingManifestObject)
        {
            foreach (string key in billingManifestObject.Keys)
            {
                switch (key)
                {
                    case StartDateKey:
                        _startDate = billingManifestObject.GetJSONContentAsDateTime(key);
                        break;
                    case EndDateKey:
                        _endDate = billingManifestObject.GetJSONContentAsDateTime(key);
                        break;
                    case PeriodTypeKey:
                        _periodType = billingManifestObject.GetJSONContentAsEnum<PeriodType>(key);
                        break;
                    case ExistingBalanceInCentsKey:
                        _existingBalanceInCents = billingManifestObject.GetJSONContentAsInt(key);
                        break;
                    case SubtotalInCentsKey:
                        _subtotalInCents = billingManifestObject.GetJSONContentAsInt(key);
                        break;
                    case TotalDiscountInCentsKey:
                        _totalDiscountInCents = billingManifestObject.GetJSONContentAsInt(key);
                        break;
                    case TotalTaxInCentsKey:
                        _totalTaxInCents = billingManifestObject.GetJSONContentAsInt(key);
                        break;
                    case TotalInCentsKey:
                        _totalInCents = billingManifestObject.GetJSONContentAsInt(key);
                        break;
                    case LineItemsKey:
                        _lineItems = billingManifestObject.GetJSONContentAsResults<ILineItem>(key, x => new LineItem(x));
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Load data from a billing manifest node.
        /// </summary>
        /// <param name="billingManifestNode">The billing manifest node</param>
        private void LoadFromNode(XmlNode billingManifestNode)
        {
            foreach (XmlNode dataNode in billingManifestNode.ChildNodes)
            {
                switch (dataNode.Name)
                {
                    case StartDateKey:
                        _startDate = dataNode.GetNodeContentAsDateTime();
                        break;
                    case EndDateKey:
                        _endDate = dataNode.GetNodeContentAsDateTime();
                        break;
                    case PeriodTypeKey:
                        _periodType = dataNode.GetNodeContentAsEnum<PeriodType>();
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
                    case LineItemsKey:
                        _lineItems = dataNode.GetNodeContentAsResults<ILineItem>("line_item", x => new LineItem(x));
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region IBillingManifest Members

        /// <summary>
        /// Gets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
        }

        private DateTime _startDate = DateTime.MinValue;

        /// <summary>
        /// Gets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
        }

        private DateTime _endDate = DateTime.MinValue;

        /// <summary>
        /// Gets the type of the period.
        /// </summary>
        /// <value>
        /// The type of the period.
        /// </value>
        /// <exception cref="NotImplementedException"></exception>
        public PeriodType PeriodType
        {
            get
            {
                return _periodType;
            }
        }

        private PeriodType _periodType = PeriodType.Unknown;

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
        /// Equals operator for two billing manifests.
        /// </summary>
        /// <returns>True if the billing manifests are equal</returns>
        public static bool operator ==(BillingManifest a, BillingManifest b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b)) { return true; }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null)) { return false; }

            return a.StartDate == b.StartDate
                && a.EndDate == b.EndDate
                && a.ExistingBalanceInCents == b.ExistingBalanceInCents
                && a.PeriodType == b.PeriodType
                && a.SubtotalInCents == b.SubtotalInCents
                && a.TotalDiscountInCents == b.TotalDiscountInCents
                && a.TotalInCents == b.TotalInCents
                && a.TotalTaxInCents == b.TotalTaxInCents;
        }

        /// <summary>
        /// Equals operator for two billing manifests.
        /// </summary>
        /// <returns>True if the billing manifests are equal</returns>
        public static bool operator ==(BillingManifest a, IBillingManifest b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b)) { return true; }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null)) { return false; }

            return a.StartDate == b.StartDate
                && a.EndDate == b.EndDate
                && a.ExistingBalanceInCents == b.ExistingBalanceInCents
                && a.PeriodType == b.PeriodType
                && a.SubtotalInCents == b.SubtotalInCents
                && a.TotalDiscountInCents == b.TotalDiscountInCents
                && a.TotalInCents == b.TotalInCents
                && a.TotalTaxInCents == b.TotalTaxInCents;
        }

        /// Equals operator for two billing manifests.
        /// </summary>
        /// <returns>True if the billing manifests are equal</returns>
        public static bool operator ==(IBillingManifest a, BillingManifest b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b)) { return true; }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null)) { return false; }

            return a.StartDate == b.StartDate
                && a.EndDate == b.EndDate
                && a.ExistingBalanceInCents == b.ExistingBalanceInCents
                && a.PeriodType == b.PeriodType
                && a.SubtotalInCents == b.SubtotalInCents
                && a.TotalDiscountInCents == b.TotalDiscountInCents
                && a.TotalInCents == b.TotalInCents
                && a.TotalTaxInCents == b.TotalTaxInCents;
        }

        /// <summary>
        /// Not Equals operator for two billing manifests.
        /// </summary>
        /// <returns>True if the billing manifests are not equal</returns>
        public static bool operator !=(BillingManifest a, BillingManifest b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Not Equals operator for two billing manifests.
        /// </summary>
        /// <returns>True if the billing manifests are not equal</returns>
        public static bool operator !=(BillingManifest a, IBillingManifest b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Not Equals operator for two billing manifests.
        /// </summary>
        /// <returns>True if the billing manifests are not equal</returns>
        public static bool operator !=(IBillingManifest a, BillingManifest b)
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

            if (typeof(IBillingManifest).IsAssignableFrom(obj.GetType()))
            {
                var castObj = obj as IBillingManifest;
                return this.StartDate == castObj.StartDate
                    && this.EndDate == castObj.EndDate
                    && this.ExistingBalanceInCents == castObj.ExistingBalanceInCents
                    && this.PeriodType == castObj.PeriodType
                    && this.SubtotalInCents == castObj.SubtotalInCents
                    && this.TotalDiscountInCents == castObj.TotalDiscountInCents
                    && this.TotalInCents == castObj.TotalInCents
                    && this.TotalTaxInCents == castObj.TotalTaxInCents;
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
            return string.Format("Billing Manifest: {0:C0}", this.Total);
        }

        #endregion

        #region IComparable<IBillingManifest> Members

        /// <summary>
        /// Compare this instance to another.
        /// </summary>
        /// <param name="other">The other instance</param>
        /// <returns>The result of the comparison</returns>
        public int CompareTo(IBillingManifest other)
        {
            return this.TotalInCents.CompareTo(other.TotalInCents);
        }

        #endregion

        #region IComparable<BillingManifest> Members

        /// <summary>
        /// Compare this instance to another.
        /// </summary>
        /// <param name="other">The other instance</param>
        /// <returns>The result of the comparison</returns>
        public int CompareTo(BillingManifest other)
        {
            return this.TotalInCents.CompareTo(other.TotalInCents);
        }

        #endregion
    }
}