using System;
using System.Web.Http;
using CardValidationService.Data;
using CardValidationService.Web.Models;

namespace CardValidationService.Web.Controllers
{
    public class CardValidationApiController : ApiController
    {
        readonly NLog.Logger Logger = NLog.LogManager.GetLogger("WEB");

        /// <summary>
        /// Validate card number
        /// </summary>
        /// <param name="cardNumber">Card number</param>
        /// <returns>Validation result <see cref="ValidationResult" /> </returns>
        public object Validate([FromBody]string cardNumber)
        {
            try
            {
                if (cardNumber == null)
                    return BadRequest($"Card number not specified");

                decimal? cardNumberDec = CardNumberHelper.TryParse(cardNumber);
                if (cardNumberDec == null)
                    return BadRequest($"Invalid card number");

                using (var db = new CardValidationServiceDB())
                {
                    var res = db.ValidateCard(cardNumberDec.Value);

                    return new ValidationResult
                    {
                        CardType = res.CardType,
                        ValidationStatus = res.ValidationStatus,
                    };
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Validate cardNumber={cardNumber}");
                return InternalServerError(e);
            }
        }
    }
}