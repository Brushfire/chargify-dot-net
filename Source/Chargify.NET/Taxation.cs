using ChargifyNET.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ChargifyNET
{
    /// <summary>
    /// Represents a taxation.
    /// </summary>
    /// <seealso cref="ChargifyNET.ChargifyBase" />
    /// <seealso cref="ChargifyNET.ITaxation" />
    /// <seealso cref="System.IComparable{ChargifyNET.Taxation}" />
    public class Taxation : ChargifyBase, ITaxation, IComparable<Taxation>
    {
        #region Field Keys
        private const string RootElementName = "taxation";
        private const string RateKey = "rate";
        private const string TaxAmountInCentsKey = "tax_amount_in_cents";
        private const string TaxIdKey = "tax_id";
        private const string TaxNameKey = "tax_name";
        private const string TaxRulesKey = "tax_rules";
        private string _rate = null;
        private int _taxAmountInCents;
        private string _taxId = null;
        private string _taxName = null;
        private List<string> _taxRules = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.  Values set to default
        /// </summary>
        public Taxation() : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lineItemXml">XML containing taxation info (in expected format)</param>
        public Taxation(string lineItemXml) : base()
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
            throw new ArgumentException("XML does not contain taxation information", "lineItemXml");
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="taxationNode">XML containing taxation info (in expected format)</param>
        internal Taxation(string rootElementName, XmlNode taxationNode) : base()
        {
            if (taxationNode == null) throw new ArgumentNullException("lineItemNode");
            if (taxationNode.Name != rootElementName) throw new ArgumentException("Not a vaild taxation node", "taxationNode");
            if (taxationNode.ChildNodes.Count == 0) throw new ArgumentException("XML not valid", "taxationNode");
            this.LoadFromNode(taxationNode);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="taxationObject">JsonObject containing taxation object info (in expected format)</param>
        public Taxation(JsonObject taxationObject)
            : base()
        {
            if (taxationObject == null) throw new ArgumentNullException("lineItemObject");
            if (taxationObject.Keys.Count <= 0) throw new ArgumentException("Not a vaild taxation node", "lineItemObject");
            this.LoadFromJSON(taxationObject);
        }

        /// <summary>
        /// Load data from a JsonObject
        /// </summary>
        /// <param name="taxationObject">The JsonObject containing subscription preview data</param>
        private void LoadFromJSON(JsonObject taxationObject)
        {
            foreach (string key in taxationObject.Keys)
            {
                switch (key)
                {
                    case RateKey:
                        _rate = taxationObject.GetJSONContentAsString(key);
                        break;
                    case TaxAmountInCentsKey:
                        _taxAmountInCents = taxationObject.GetJSONContentAsInt(key);
                        break;
                    case TaxIdKey:
                        _taxId = taxationObject.GetJSONContentAsString(key);
                        break;
                    case TaxNameKey:
                        _taxName = taxationObject.GetJSONContentAsString(key);
                        break;
                    case TaxRulesKey:
                        _taxRules = taxationObject.GetJSONContentAsResults<string>(key, x => x.ToString());
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Load data from a taxation node.
        /// </summary>
        /// <param name="taxationNode">The taxation node</param>
        private void LoadFromNode(XmlNode taxationNode)
        {
            foreach (XmlNode dataNode in taxationNode.ChildNodes)
            {
                switch (dataNode.Name)
                {
                    case RateKey:
                        _rate = dataNode.GetNodeContentAsString();
                        break;
                    case TaxAmountInCentsKey:
                        _taxAmountInCents = dataNode.GetNodeContentAsInt();
                        break;
                    case TaxIdKey:
                        _taxId = dataNode.GetNodeContentAsString();
                        break;
                    case TaxNameKey:
                        _taxName = dataNode.GetNodeContentAsString();
                        break;
                    case TaxRulesKey:
                        _taxRules = dataNode.GetNodeContentAsResults<string>("tax_rules", x => x.Value);
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region ITaxation Members

        /// <summary>
        /// Gets the rate.
        /// </summary>
        /// <value>
        /// The rate.
        /// </value>
        public string Rate
        {
            get
            {
                return _rate;
            }
        }

        /// <summary>
        /// Gets the tax amount in cents.
        /// </summary>
        /// <value>
        /// The tax amount in cents.
        /// </value>
        public int TaxAmountInCents
        {
            get
            {
                return _taxAmountInCents;
            }
        }

        /// <summary>
        /// Gets the tax amount.
        /// </summary>
        /// <value>
        /// The tax amount.
        /// </value>
        public decimal TaxAmount
        {
            get
            {
                return Convert.ToDecimal(this._taxAmountInCents) / 100;
            }
        }

        /// <summary>
        /// Gets the tax identifier.
        /// </summary>
        /// <value>
        /// The tax identifier.
        /// </value>
        public string TaxId
        {
            get
            {
                return _taxId;
            }
        }

        /// <summary>
        /// Gets the name of the tax.
        /// </summary>
        /// <value>
        /// The name of the tax.
        /// </value>
        public string TaxName
        {
            get
            {
                return _taxName;
            }
        }

        /// <summary>
        /// Gets the tax rules.
        /// </summary>
        /// <value>
        /// The tax rules.
        /// </value>
        public List<string> TaxRules
        {
            get
            {
                return _taxRules;
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Equals operator for two taxations.
        /// </summary>
        /// <returns>True if the taxations are equal</returns>
        public static bool operator ==(Taxation a, Taxation b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b)) { return true; }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null)) { return false; }

            return a.Rate == b.Rate
                && a.TaxAmountInCents == b.TaxAmountInCents
                && a.TaxName == b.TaxName;
        }

        /// <summary>
        /// Equals operator for two taxations.
        /// </summary>
        /// <returns>True if the taxations are equal</returns>
        public static bool operator ==(Taxation a, ITaxation b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b)) { return true; }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null)) { return false; }

            return a.Rate == b.Rate
                && a.TaxAmountInCents == b.TaxAmountInCents
                && a.TaxName == b.TaxName;
        }

        /// Equals operator for two taxations.
        /// </summary>
        /// <returns>True if the taxations are equal</returns>
        public static bool operator ==(ITaxation a, Taxation b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b)) { return true; }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null)) { return false; }

            return a.Rate == b.Rate
                && a.TaxAmountInCents == b.TaxAmountInCents
                && a.TaxName == b.TaxName;
        }

        /// <summary>
        /// Not Equals operator for two taxations.
        /// </summary>
        /// <returns>True if the taxations are not equal</returns>
        public static bool operator !=(Taxation a, Taxation b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Not Equals operator for two taxations.
        /// </summary>
        /// <returns>True if the taxations are not equal</returns>
        public static bool operator !=(Taxation a, ITaxation b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Not Equals operator for two taxations.
        /// </summary>
        /// <returns>True if the taxations are not equal</returns>
        public static bool operator !=(ITaxation a, Taxation b)
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

            if (typeof(ITaxation).IsAssignableFrom(obj.GetType()))
            {
                var castObj = obj as ITaxation;
                return this.Rate == castObj.Rate
                    && this.TaxAmountInCents == castObj.TaxAmountInCents
                    && this.TaxName == castObj.TaxName;
            }
            else
            {
                return base.Equals(obj);
            }
        }

        /// <summary>
        /// Returns a string representation of the taxation object.
        /// </summary>
        public override string ToString()
        {
            return string.Format("taxation {0}: {1:C0}", this.TaxName, this.TaxAmount);
        }

        #endregion

        #region IComparable<ITaxation> Members

        /// <summary>
        /// Compare this instance to another.
        /// </summary>
        /// <param name="other">The other instance</param>
        /// <returns>The result of the comparison</returns>
        public int CompareTo(ITaxation other)
        {
            return this.TaxName.CompareTo(other.TaxName);
        }

        #endregion

        #region IComparable<Taxation> Members

        /// <summary>
        /// Compare this instance to another.
        /// </summary>
        /// <param name="other">The other instance</param>
        /// <returns>The result of the comparison</returns>
        public int CompareTo(Taxation other)
        {
            return this.TaxName.CompareTo(other.TaxName);
        }

        #endregion
    }
}