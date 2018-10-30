using CardValidationService.Data;

namespace CardValidationService.Repositories
{
    public class CardValidationServiceRepository : ICardValidationServiceRepository
    {
        public CardValidationServiceRepository()
        {
            DB = new CardValidationServiceDB();
        }

        readonly CardValidationServiceDB DB;

        public ValidateCard_Result ValidateCard(decimal cardNumber)
            => DB.ValidateCard(cardNumber);

        public void Dispose()
            => DB?.Dispose();
    }
}
