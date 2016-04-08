using ChargifyNET.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ChargifyNET
{
    /// <summary>
    /// Represents a line item.
    /// </summary>
    public class LineItem : ChargifyBase, ILineItem, IComparable<LineItem>
    {
        #region Field Keys
        private const string RootElementName = "line_item";
        private const string AmountInCentsKey = "amount_in_cents";
        private const string DiscountAmountInCentsKey = "discount_amount_in_cents";
        private const string KindKey = "kind";
        private const string MemoKey = "memo";
        private const string TaxableAmountInCentsKey = "taxable_amount_in_cents";
        private const string TaxationsKey = "taxations";
        private const string TransactionTypeKey = "transaction_type";
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.  Values set to default
        /// </summary>
        public LineItem() : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lineItemXml">XML containing line item info (in expected format)</param>
        public LineItem(string lineItemXml) : base()
        {
            // get the XML into an XML document
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(lineItemXml);
            if (doc.ChildNodes.Count == 0) throw new ArgumentException("XML not valid", "lineItemXml");
            // loop through the child nodes of this node
            foreach (XmlNode elementNode in doc.ChildNodes)
            {
                if (elementNode.Name == RootElementName)
                {
                    this.LoadFromNode(elementNode);
                    return;
                }
            }
            // if we get here, then no customer info was found
            throw new ArgumentException("XML does not contain line item information", "lineItemXml");
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lineItemNode">XML containing line item info (in expected format)</param>
        internal LineItem(XmlNode lineItemNode) : base()
        {
            if (lineItemNode == null) throw new ArgumentNullException("lineItemNode");
            if (lineItemNode.Name != RootElementName) throw new ArgumentException("Not a vaild line item node", "lineItemNode");
            if (lineItemNode.ChildNodes.Count == 0) throw new ArgumentException("XML not valid", "lineItemNode");
            this.LoadFromNode(lineItemNode);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lineItemObject">JsonObject containing line item object info (in expected format)</param>
        public LineItem(JsonObject lineItemObject)
            : base()
        {
            if (lineItemObject == null) throw new ArgumentNullException("lineItemObject");
            if (lineItemObject.Keys.Count <= 0) throw new ArgumentException("Not a vaild line item node", "lineItemObject");
            this.LoadFromJSON(lineItemObject);
        }

        /// <summary>
        /// Load data from a JsonObject
        /// </summary>
        /// <param name="lineItemObject">The JsonObject containing subscription preview data</param>
        private void LoadFromJSON(JsonObject lineItemObject)
        {
            foreach (string key in lineItemObject.Keys)
            {
                switch (key)
                {
                    case AmountInCentsKey:
                        _amountInCents = lineItemObject.GetJSONContentAsInt(key);
                        break;
                    case DiscountAmountInCentsKey:
                        _discountAmountInCents = lineItemObject.GetJSONContentAsInt(key);
                        break;
                    case KindKey:
                        _kind = lineItemObject.GetJSONContentAsEnum<TransactionChargeKind>(key);
                        break;
                    case MemoKey:
                        _memo = lineItemObject.GetJSONContentAsString(key);
                        break;
                    case TaxableAmountInCentsKey:
                        _taxableAmountInCents = lineItemObject.GetJSONContentAsInt(key);
                        break;
                    case TaxationsKey:
                        _taxations = lineItemObject.GetJSONContentAsResults<ITaxation>(key, x => new Taxation(x));
                        break;
                    case TransactionTypeKey:
                        _transactionType = lineItemObject.GetJSONContentAsEnum<TransactionType>(key);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Load data from a line item node.
        /// </summary>
        /// <param name="listItemNode">The line item node</param>
        private void LoadFromNode(XmlNode listItemNode)
        {
            foreach (XmlNode dataNode in listItemNode.ChildNodes)
            {
                switch (dataNode.Name)
                {
                    case AmountInCentsKey:
                        _amountInCents = dataNode.GetNodeContentAsInt();
                        break;
                    case DiscountAmountInCentsKey:
                        _discountAmountInCents = dataNode.GetNodeContentAsInt();
                        break;
                    case KindKey:
                        _kind = dataNode.GetNodeContentAsEnum<TransactionChargeKind>();
                        break;
                    case MemoKey:
                        _memo = dataNode.GetNodeContentAsString();
                        break;
                    case TaxableAmountInCentsKey:
                        _taxableAmountInCents = dataNode.GetNodeContentAsInt();
                        break;
                    case TaxationsKey:
                        _taxations = dataNode.GetNodeContentAsResults<ITaxation>("taxation", x => new Taxation("taxation", x));
                        break;
                    case TransactionTypeKey:
                        _transactionType = dataNode.GetNodeContentAsEnum<TransactionType>();
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region ILineItem Members

        /// <summary>
        /// An integer presenting an amount in cents.
        /// </summary>
        public int AmountInCents
        {
            get
            {
                return _amountInCents;
            }
        }

        private int _amountInCents;

        /// <summary>
        /// Gets the decimal amount.
        /// </summary>
        public decimal Amount
        {
            get
            {
                return Convert.ToDecimal(this._amountInCents) / 100;
            }
        }

        /// <summary>
        /// An integer presenting an discount amount in cents.
        /// </summary>
        public int DiscountAmountInCents
        {
            get
            {
                return _discountAmountInCents;
            }
        }

        private int _discountAmountInCents;

        /// <summary>
        /// Gets the decimal discount amount.
        /// </summary>
        public decimal DiscountAmount
        {
            get
            {
                return Convert.ToDecimal(this._discountAmountInCents) / 100;
            }
        }

        /// <summary>
        /// Gets the kind of transaction charge.
        /// </summary>
        public TransactionChargeKind Kind
        {
            get
            {
                return _kind;
            }
        }

        private TransactionChargeKind _kind = TransactionChargeKind.Initial;

        /// <summary>
        /// Gets the memo for this line item.
        /// </summary>
        public string Memo
        {
            get
            {
                return _memo;
            }
        }

        private string _memo = null;

        /// <summary>
        /// Gets the taxable amount in cents.
        /// </summary>
        public int TaxableAmountInCents
        {
            get
            {
                return _taxableAmountInCents;
            }
        }

        private int _taxableAmountInCents;

        /// <summary>
        /// Gets the decimal taxable amount.
        /// </summary>
        public decimal TaxableAmount
        {
            get
            {
                return Convert.ToDecimal(this._taxableAmountInCents) / 100;
            }
        }

        /// <summary>
        /// Gets the list of taxations.
        /// </summary>
        public List<ITaxation> Taxations
        {
            get
            {
                return _taxations;
            }
        }

        private List<ITaxation> _taxations = null;

        /// <summary>
        /// Gets the type of transaction.
        /// </summary>
        public TransactionType TransactionType
        {
            get
            {
                return _transactionType;
            }
        }

        private TransactionType _transactionType = TransactionType.Unknown;

        #endregion

        #region Operators

        /// <summary>
        /// Equals operator for two line items.
        /// </summary>
        /// <returns>True if the line items are equal</returns>
        public static bool operator ==(LineItem a, LineItem b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b)) { return true; }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null)) { return false; }

            return a.AmountInCents == b.AmountInCents
                && a.DiscountAmountInCents == b.DiscountAmountInCents
                && a.Kind == b.Kind
                && a.Memo == b.Memo
                && a.TaxableAmountInCents == b.TaxableAmountInCents
                && a.TransactionType == b.TransactionType;
        }

        /// <summary>
        /// Equals operator for two line items.
        /// </summary>
        /// <returns>True if the line items are equal</returns>
        public static bool operator ==(LineItem a, ILineItem b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b)) { return true; }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null)) { return false; }

            return a.AmountInCents == b.AmountInCents
                && a.DiscountAmountInCents == b.DiscountAmountInCents
                && a.Kind == b.Kind
                && a.Memo == b.Memo
                && a.TaxableAmountInCents == b.TaxableAmountInCents
                && a.TransactionType == b.TransactionType;
        }

        /// Equals operator for two line items.
        /// </summary>
        /// <returns>True if the line items are equal</returns>
        public static bool operator ==(ILineItem a, LineItem b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b)) { return true; }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null)) { return false; }

            return a.AmountInCents == b.AmountInCents
                && a.DiscountAmountInCents == b.DiscountAmountInCents
                && a.Kind == b.Kind
                && a.Memo == b.Memo
                && a.TaxableAmountInCents == b.TaxableAmountInCents
                && a.TransactionType == b.TransactionType;
        }

        /// <summary>
        /// Not Equals operator for two line items.
        /// </summary>
        /// <returns>True if the line items are not equal</returns>
        public static bool operator !=(LineItem a, LineItem b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Not Equals operator for two line items.
        /// </summary>
        /// <returns>True if the line items are not equal</returns>
        public static bool operator !=(LineItem a, ILineItem b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Not Equals operator for two line items.
        /// </summary>
        /// <returns>True if the line items are not equal</returns>
        public static bool operator !=(ILineItem a, LineItem b)
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

            if (typeof(ILineItem).IsAssignableFrom(obj.GetType()))
            {
                var castObj = obj as ILineItem;
                return this.AmountInCents == castObj.AmountInCents
                    && this.DiscountAmountInCents == castObj.DiscountAmountInCents
                    && this.Kind == castObj.Kind
                    && this.Memo == castObj.Memo
                    && this.TaxableAmountInCents == castObj.TaxableAmountInCents
                    && this.TransactionType == castObj.TransactionType;
            }
            else
            {
                return base.Equals(obj);
            }
        }

        /// <summary>
        /// Returns a string representation of the line item object.
        /// </summary>
        public override string ToString()
        {
            return string.Format("line item {0}: {1:C0}", this.TransactionType, this.Amount);
        }

        #endregion

        #region IComparable<ILineItem> Members

        /// <summary>
        /// Compare this instance to another.
        /// </summary>
        /// <param name="other">The other instance</param>
        /// <returns>The result of the comparison</returns>
        public int CompareTo(ILineItem other)
        {
            return this.AmountInCents.CompareTo(other.AmountInCents);
        }

        #endregion

        #region IComparable<LineItem> Members

        /// <summary>
        /// Compare this instance to another.
        /// </summary>
        /// <param name="other">The other instance</param>
        /// <returns>The result of the comparison</returns>
        public int CompareTo(LineItem other)
        {
            return this.AmountInCents.CompareTo(other.AmountInCents);
        }
        

        #endregion
    }
}