using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ATMInterface.Tools.Utilities
{
    class Validation
    {
        private static readonly Regex numbers = new Regex("^[0-9]+$");
        
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
            return int.TryParse(text, out _);
        }

        public static bool AlwaysExecute(object obj)
        {
            return true;
        }
    }
}
