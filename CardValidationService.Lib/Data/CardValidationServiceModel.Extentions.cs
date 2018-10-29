using System;
using System.Linq;

namespace CardValidationService.Data
{
    public static class CardType
    {
        public const string Visa = "Visa";
        public const string MasterCard = "MasterCard";
        public const string Amex = "Amex";
        public const string JCB = "JCB";
        public const string Unknown = "Unknown";
    }

    public static class ValidationStatus
    {
        public const string Invalid = "Invalid";
        public const string Valid = "Valid";
        public const string NotExist = "Does not exist";
    }

    partial class CardValidationServiceDB
    {
        public static string DefaultConnectionString { get; set; }
            = System.Configuration.ConfigurationManager.ConnectionStrings["CardValidationServiceDB"].ConnectionString;

        NLog.Logger Logger { get; } = NLog.LogManager.GetLogger("DB");

        public ValidateCard_Result ValidateCard(decimal cardNumber)
        {
            try
            {
                var res = raw_ValidateCard(cardNumber).Single();
                Logger.Info($"ValidateCard card={cardNumber} type={res.CardType} status={res.ValidationStatus}");
                return res;
            }
            catch (Exception e)
            {
                Logger.Error(e, $"ValidateCard card={cardNumber}");
                throw;
            }
        }
    }
}
