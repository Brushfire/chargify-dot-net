using System;
using ChargifyDotNetTests.Base;
using System.Linq;
using ChargifyNET;
using System.Collections.Generic;
using System.Diagnostics;
#if NUNIT
using NUnit.Framework;
#else
using TestFixture = Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
using TestFixtureSetUp = Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute;
using SetUp = Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace ChargifyDotNetTests
{
    [TestFixture]
    public class SubscriptionPreviewTests : ChargifyTestBase
    {
        #region Tests
        [Test]
        public void SubscriptionPreview_Create_UsingOptions_ProductHandle()
        {
            // Arrange
            var product = Chargify.GetProductList().Values.FirstOrDefault();
            Assert.IsNotNull(product, "Product not found");

            var productFamily = Chargify.GetProductFamilyList().Values.FirstOrDefault();
            
            var options = new SubscriptionCreateOptions()
            {
                ProductHandle = product.Handle
            };
            
         
            // Act
            var result = Chargify.CreateSubscriptionPreview(options);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.CurrentBillingManifest);
            Assert.IsNotNull(result.NextBillingManifest);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void SubscriptionPreview_Create_UsingOptions_TooManyProducts()
        {
            // Arrange
            var exampleCustomer = Chargify.GetCustomerList().Values.DefaultIfEmpty(defaultValue: null).FirstOrDefault();
            var product = Chargify.GetProductList().Values.FirstOrDefault();
            var options = new SubscriptionCreateOptions()
            {
                CustomerID = exampleCustomer.ChargifyID,
                ProductHandle = product.Handle,
                ProductID = product.ID
            };
           

            // Act
            var result = Chargify.CreateSubscriptionPreview(options);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void SubscriptionPreview_Create_UsingOptions_MissingProduct()
        {
            // Arrange
            var exampleCustomer = Chargify.GetCustomerList().Values.DefaultIfEmpty(defaultValue: null).FirstOrDefault();
            var options = new SubscriptionCreateOptions() {
                CustomerID = exampleCustomer.ChargifyID
            };           

            // Act
            var result = Chargify.CreateSubscriptionPreview(options);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void SubscriptionPreview_Create_UsingOptions_MissingAllDetails()
        {
            // Arrange
            var options = new SubscriptionCreateOptions();

            // Act
            var result = Chargify.CreateSubscriptionPreview(options);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void SubscriptionPreview_Create_UsingOptions_Null()
        {
            // Arrange
            SubscriptionCreateOptions options = null;
            
            // Act
            var result = Chargify.CreateSubscriptionPreview(options);
        }
        
        #endregion
    }
}
