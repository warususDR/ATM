using System.Text.RegularExpressions;

namespace ATMInterface.Tools.Utilities
{
    class Validation
    {
        private static readonly Regex numbers = new Regex("^[0-9]+$");
        private const int PUT_LIMIT = 1000;
        
        public static bool HasCreditCardNumberFormat(string text)
        {
            return numbers.IsMatch(text) && text.Length == 4;
        }

        public static bool HasPinFormat(string text)
        {
            return numbers.IsMatch(text) && text.Length == 4;
        }

        public static bool HasCurrencyFormat(string text)
        {
            if (int.TryParse(text, out _))
            {
                return int.Parse(text) >= 1; 
            }
            return false;
        }

        public static bool CanAdd(string text)
        {
            if (int.TryParse(text, out _))
            {
                int currency = int.Parse(text);
                return currency >= 1 && currency <= PUT_LIMIT;
            }
            return false;
        }

        public static bool AlwaysExecute(object obj)
        {
            return true;
        }
    }
}
