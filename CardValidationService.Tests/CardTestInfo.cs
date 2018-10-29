using System;
using CardValidationService.Data;

namespace CardValidationService.Tests
{
    struct CardTestInfo
    {
        CardTestInfo(string type, decimal number, DateTime? expiryDate, string status)
        {
            Type = type;
            Number = number;
            NumberString = number.ToString();
            ExpiryDate = expiryDate;
            Status = status;
        }

        public readonly string Type;
        public readonly decimal Number;
        public readonly string NumberString;
        public readonly DateTime? ExpiryDate;
        public readonly string Status;

        const int LeapYear = 2020;
        const int NonLeapYear = 2018;
        const int PrimeYear = 2017;
        const int NonPrimeYear = 2018;
        const int AnyYear = 2019;

        public static readonly CardTestInfo VisaValid = new CardTestInfo(CardType.Visa, 4000000000000000, new DateTime(LeapYear, 1, 1), ValidationStatus.Valid);
        public static readonly CardTestInfo VisaInvalid = new CardTestInfo(CardType.Visa, 4000000000000001, new DateTime(NonLeapYear, 1, 1), ValidationStatus.Invalid);
        public static readonly CardTestInfo VisaNotExist = new CardTestInfo(CardType.Visa, 4000000000000002, null, ValidationStatus.NotExist);
        public static readonly CardTestInfo MasterCardValid = new CardTestInfo(CardType.MasterCard, 5000000000000000, new DateTime(PrimeYear, 1, 1), ValidationStatus.Valid);
        public static readonly CardTestInfo MasterCardInvalid = new CardTestInfo(CardType.MasterCard, 5000000000000001, new DateTime(NonPrimeYear, 1, 1), ValidationStatus.Invalid);
        public static readonly CardTestInfo MasterCardNotExist = new CardTestInfo(CardType.MasterCard, 5000000000000002, null, ValidationStatus.NotExist);
        public static readonly CardTestInfo AmexValid = new CardTestInfo(CardType.Amex, 340000000000000, new DateTime(AnyYear, 1, 1), ValidationStatus.Valid);
        public static readonly CardTestInfo AmexNotExist = new CardTestInfo(CardType.Amex, 370000000000002, null, ValidationStatus.NotExist);
        public static readonly CardTestInfo JCBValid = new CardTestInfo(CardType.JCB, 3528000000000000, new DateTime(AnyYear, 1, 1), ValidationStatus.Valid);
        public static readonly CardTestInfo JCBNotExist = new CardTestInfo(CardType.JCB, 3589000000000002, null, ValidationStatus.NotExist);
    }
}
