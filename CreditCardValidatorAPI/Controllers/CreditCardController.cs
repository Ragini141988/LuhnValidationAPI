using System.Web.Http;

namespace CreditCardValidatorAPI.Controllers
{
    public class CreditCardController : ApiController
    {
        /// <summary>
        /// Validates the provided credit card number using the Luhn algorithm.
        /// </summary>
        /// <param name="cardNumber">Credit card number to validate</param>
        /// <returns>Boolean indicating whether the credit card number is valid</returns>
        [HttpGet]
        [Route("api/creditcard/validate")]
        public IHttpActionResult ValidateCreditCard([FromUri] string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                return BadRequest("Credit card number cannot be empty.");
            }

            if (!IsDigitsOnly(cardNumber))
            {
                return BadRequest("Credit card number must contain only digits.");
            }

            return Ok(LuhnCheck(cardNumber));
        }

        /// <summary>
        /// Checks if the input string contains only digits.
        /// </summary>
        /// <param name="s">Input string</param>
        /// <returns>True if the string contains only digits, otherwise false</returns>
        private bool IsDigitsOnly(string s)
        {
            foreach (char c in s)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Validates the credit card number using the Luhn algorithm.
        /// </summary>
        /// <param name="cardNumber">Credit card number</param>
        /// <returns>True if the credit card number is valid, otherwise false</returns>
        private bool LuhnCheck(string cardNumber)
        {
            int sum = 0;
            bool alternate = false;
            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(cardNumber[i].ToString());
                if (alternate)
                {
                    n *= 2;
                    if (n > 9)
                    {
                        n -= 9;
                    }
                }
                sum += n;
                alternate = !alternate;
            }
            return (sum % 10 == 0);
        }

    }
}
