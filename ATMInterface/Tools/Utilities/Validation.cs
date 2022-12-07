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

        private static readonly Regex currency = new Regex(@"^((([1-9]\d{0,2},(\d{3},)*\d{3}|[1-9]\d*)(.\d{1,4})?)|(0\.\d{1,4}))$");  
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
            return currency.IsMatch(text);
        }

        public static bool AlwaysExecute(object obj)
        {
            return true;
        }
    }
}
