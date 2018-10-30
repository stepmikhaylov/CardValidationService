using System;
using System.Web.Http;
using CardValidationService.Repositories;
using CardValidationService.Web.Models;

namespace CardValidationService.Web.Controllers
{
    public class CardValidationApiController : ApiController
    {
        public CardValidationApiController(ICardValidationServiceRepository repository)
        {
            Repository = repository;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
                Repository?.Dispose();
        }

        readonly NLog.Logger Logger = NLog.LogManager.GetLogger("WEB");
        readonly ICardValidationServiceRepository Repository;

        [HttpGet]
        public object Index()
            => Redirect(new Uri("/swagger/ui/index", UriKind.Relative));

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

                var res = Repository.ValidateCard(cardNumberDec.Value);

                return new ValidationResult
                {
                    CardType = res.CardType,
                    ValidationStatus = res.ValidationStatus,
                };
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Validate cardNumber={cardNumber}");
                return InternalServerError(e);
            }
        }
    }
}
