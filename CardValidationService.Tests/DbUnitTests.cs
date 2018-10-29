using System.IO;
using System.Linq;
using System.Reflection;
using CardValidationService.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardValidationService.Tests
{
    [TestClass]
    public class DbUnitTests
    {
        static bool IsDbSet { get; set; } = false;

        static public void SetupTestDB()
        {
            if (IsDbSet)
                return;
            IsDbSet = true;

            string cs = CardValidationServiceDB.DefaultConnectionString;
            var ecb = new System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder(cs);
            var cb = new System.Data.SqlClient.SqlConnectionStringBuilder(ecb.ProviderConnectionString)
            {
                AttachDBFilename = Path.GetFullPath("CardValidationServiceDB.mdf"),
            };
            ecb.ProviderConnectionString = cb.ToString();
            CardValidationServiceDB.DefaultConnectionString = ecb.ConnectionString;

            using (var db = new CardValidationServiceDB())
            {
                try { if (db.Database.Exists()) db.Database.Delete(); }
                catch { }
                db.Database.Create();
                db.Database.ExecuteSqlCommand(File.ReadAllText(@"..\..\..\CardValidationService.DB\Functions\GetCardType.sql"));
                string fuzzDBLibPath = Path.GetFullPath(@"..\CardValidationService.DB\CardValidationServiceDB.dll");
                db.Database.ExecuteSqlCommand($"CREATE ASSEMBLY [CardValidationServiceDB] FROM '{fuzzDBLibPath}'");
                db.Database.ExecuteSqlCommand(@"
CREATE FUNCTION [dbo].[IsPrimeNumber](@number INT)
RETURNS BIT
EXTERNAL NAME [CardValidationServiceDB].[UserDefinedFunctions].[IsPrimeNumber]");
                db.Database.ExecuteSqlCommand(File.ReadAllText(@"..\..\..\CardValidationService.DB\Functions\IsExpiryDateValid.sql"));
                db.Database.ExecuteSqlCommand(File.ReadAllText(@"..\..\..\CardValidationService.DB\Procedures\ValidateCard.sql"));

                db.Card.AddRange(
                    from fi in typeof(CardTestInfo).GetFields(BindingFlags.Public | BindingFlags.Static)
                    where fi.FieldType == typeof(CardTestInfo)
                    let ci = (CardTestInfo)fi.GetValue(typeof(CardTestInfo))
                    where ci.ExpiryDate != null
                    select new Card
                    {
                        Number = ci.Number,
                        ExpiryDate = ci.ExpiryDate.Value
                    });

                db.SaveChanges();
            }
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            SetupTestDB();
        }

        void DbTestCard(CardTestInfo cardInfo)
        {
            using (var db = new CardValidationServiceDB())
            {
                var res = db.ValidateCard(cardInfo.Number);
                Assert.AreEqual(cardInfo.Type, res.CardType);
                Assert.AreEqual(cardInfo.Status, res.ValidationStatus);
            }
        }

        [TestMethod]
        public void DbTestVisaValid()
            => DbTestCard(CardTestInfo.VisaValid);

        [TestMethod]
        public void DbTestVisaInvalid()
            => DbTestCard(CardTestInfo.VisaInvalid);

        [TestMethod]
        public void DbTestVisaNotExist()
            => DbTestCard(CardTestInfo.VisaNotExist);

        [TestMethod]
        public void DbTestMasterCardValid()
            => DbTestCard(CardTestInfo.MasterCardValid);

        [TestMethod]
        public void DbTestMasterCardInvalid()
            => DbTestCard(CardTestInfo.MasterCardInvalid);

        [TestMethod]
        public void DbTestMasterCardNotExist()
            => DbTestCard(CardTestInfo.MasterCardNotExist);

        [TestMethod]
        public void DbTestAmexValid()
            => DbTestCard(CardTestInfo.AmexValid);

        [TestMethod]
        public void DbTestAmexNotExist()
            => DbTestCard(CardTestInfo.AmexNotExist);

        [TestMethod]
        public void DbTestJCBValid()
            => DbTestCard(CardTestInfo.JCBValid);

        [TestMethod]
        public void DbTestJCBNotExist()
            => DbTestCard(CardTestInfo.JCBNotExist);
    }
}
