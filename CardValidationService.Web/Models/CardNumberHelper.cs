using System.Linq;
using System.Text.RegularExpressions;

namespace CardValidationService.Web.Models
{
    public static class CardNumberHelper
    {
        static readonly Regex[] CardRegexList = new Regex[]
        {
            new Regex(@"^(\d{15,16})$"),
            new Regex(@"^(\d{4})\s(\d{4})\s(\d{4})\s(\d{4})$"),
            new Regex(@"^(\d{4})-(\d{4})-(\d{4})-(\d{4})$"),
            new Regex(@"^(\d{4})\s(\d{6})\s(\d{5})$"),
            new Regex(@"^(\d{4})-(\d{6})-(\d{5})$"),
        };

        public static decimal? TryParse(string cardNumber)
            => (from r in CardRegexList
                let m = r.Match(cardNumber)
                where m.Success
                select (decimal?)decimal.Parse(string.Join("",
                    m.Groups.Cast<Group>().Skip(1).Select(g => g.Value))))
                .FirstOrDefault();
    }
}
