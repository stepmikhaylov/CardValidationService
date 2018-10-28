using CardValidationService.Web.Controllers;
using CardValidationService.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardValidationService.Tests
{
    [TestClass]
    public class WebUnitTests
    {
        void WebTestCard(CardTestInfo cardInfo)
        {
            var api = new DefaultApiController();
            var res = api.Validate(cardInfo.Number);
            Assert.AreEqual(cardInfo.Type, res.CardType);
            Assert.AreEqual(cardInfo.Status, res.ValidationStatus);
        }

        [TestMethod]
        public void WebTestVisaValid()
        => WebTestCard(CardTestInfo.VisaValid);

        [TestMethod]
        public void WebTestVisaInvalid()
            => WebTestCard(CardTestInfo.VisaInvalid);

        [TestMethod]
        public void WebTestVisaNotExist()
            => WebTestCard(CardTestInfo.VisaNotExist);

        [TestMethod]
        public void WebTestMasterCardValid()
            => WebTestCard(CardTestInfo.MasterCardValid);

        [TestMethod]
        public void WebTestMasterCardInvalid()
            => WebTestCard(CardTestInfo.MasterCardInvalid);

        [TestMethod]
        public void WebTestMasterCardNotExist()
            => WebTestCard(CardTestInfo.MasterCardNotExist);

        [TestMethod]
        public void WebTestAmexValid()
            => WebTestCard(CardTestInfo.AmexValid);

        [TestMethod]
        public void WebTestAmexNotExist()
            => WebTestCard(CardTestInfo.AmexNotExist);

        [TestMethod]
        public void WebTestJCBValid()
            => WebTestCard(CardTestInfo.JCBValid);

        [TestMethod]
        public void WebTestJCBNotExist()
            => WebTestCard(CardTestInfo.JCBNotExist);
    }
}
