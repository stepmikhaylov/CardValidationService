using System;
using CardValidationService.Data;

namespace CardValidationService.Repositories
{
    public interface ICardValidationServiceRepository : IDisposable
    {
        ValidateCard_Result ValidateCard(decimal cardNumber);
    }
}
