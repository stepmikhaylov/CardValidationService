using System.Runtime.Serialization;

namespace CardValidationService.Web.Models
{
    /// <summary>
    /// Validation result
    /// </summary>
    [KnownType(typeof(ValidationResult))]
    [DataContract]
    public class ValidationResult
    {
        /// <summary>
        /// Validation status
        /// </summary>
        [DataMember]
        public string ValidationStatus;

        /// <summary>
        /// Card type
        /// </summary>
        [DataMember]
        public string CardType;
    }
}
