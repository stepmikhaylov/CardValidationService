using System.Web.Http;
using CardValidationService.Data;
using CardValidationService.Web.Models;

namespace CardValidationService.Web.Controllers
{
    public class DefaultApiController : ApiController
    {
        /// <summary>
        /// Validate card number
        /// </summary>
        /// <param name="cardNumber">Card number</param>
        /// <returns>Validation result <see cref="ValidationResult" /> </returns>
        public ValidationResult Validate([FromBody]decimal cardNumber)
        {
            using (var db = new CardValidationServiceDB())
            {
                var res = db.ValidateCard(cardNumber);

                return new ValidationResult
                {
                    CardType = res.CardType,
                    ValidationStatus = res.ValidationStatus,
                };
            }
        }
    }
}