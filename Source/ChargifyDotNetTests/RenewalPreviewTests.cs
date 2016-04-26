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
    public class RenewalPreviewTests : ChargifyTestBase
    {
        #region Tests
        [Test]
        public void RenewalPreview_Create()
        {
            // Arrange
            var subscription = Chargify.GetSubscriptionList(1, 1).Values.FirstOrDefault();
            Assert.IsNotNull(subscription, "Subscription not found");

            // Act
            var result = Chargify.CreateRenewalPreview(subscription.SubscriptionID);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Total > 0);
        }
                
        #endregion
    }
}
