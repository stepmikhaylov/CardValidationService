using CardValidationService.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardValidationService.Tests
{
    [TestClass]
    public class CardNumberHelperTests
    {
        static void TestParseCardNumber(string cardNumberString, decimal? expectedCardNumber)
        {
            decimal? actualCardNumber = CardNumberHelper.TryParse(cardNumberString);
            Assert.AreEqual(expectedCardNumber, actualCardNumber);
        }

        [TestMethod]
        public void TestParseCardNumberEmptyInvalid()
            => Assert.AreEqual(null, CardNumberHelper.TryParse(string.Empty));

        [TestMethod]
        public void TestParseCardNumberNonDigitCharsInvalid()
            => Assert.AreEqual(null, CardNumberHelper.TryParse("1234567812345A"));

        [TestMethod]
        public void TestParseCardNumberLess15Invalid()
            => Assert.AreEqual(null, CardNumberHelper.TryParse("12345678123456"));

        [TestMethod]
        public void TestParseCardNumber15Valid()
            => Assert.AreEqual(123456781234567M, CardNumberHelper.TryParse("123456781234567"));

        [TestMethod]
        public void TestParseCardNumber16Valid()
            => Assert.AreEqual(1234567812345678M, CardNumberHelper.TryParse("1234567812345678"));

        [TestMethod]
        public void TestParseCardNumberGreater16Invalid()
            => Assert.AreEqual(null, CardNumberHelper.TryParse("12345678123456781"));

        [TestMethod]
        public void TestParseCardNumber16WhitespaceDelimValid()
            => Assert.AreEqual(1234567812345678M, CardNumberHelper.TryParse("1234 5678 1234 5678"));

        [TestMethod]
        public void TestParseCardNumber15WhitespaceDelimValid()
            => Assert.AreEqual(123456789012345M, CardNumberHelper.TryParse("1234 567890 12345"));

        [TestMethod]
        public void TestParseCardNumber16HyphenDelimValid()
            => Assert.AreEqual(1234567812345678M, CardNumberHelper.TryParse("1234-5678-1234-5678"));

        [TestMethod]
        public void TestParseCardNumber15HyphenDelimValid()
            => Assert.AreEqual(123456789012345M, CardNumberHelper.TryParse("1234-567890-12345"));

        [TestMethod]
        public void TestParseCardNumber16MixDelimInvalid()
            => Assert.AreEqual(null, CardNumberHelper.TryParse("1234-5678 1234-5678"));

        [TestMethod]
        public void TestParseCardNumber15MixDelimInvalid()
            => Assert.AreEqual(null, CardNumberHelper.TryParse("1234 567890-12345"));
    }
}
