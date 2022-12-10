namespace ATM
{
    public enum eUserAction
    {
        CREDIT_CARD_INSERTED,
        PASSWORD_ENTERED,
        CHECK_BALANCE,
        PRINT_BALANCE,
        GET_CASH,
        PUT_CASH,
        SESSION_ON,
        SESSION_OFF,
    }

    public enum Result
    {
        ERROR = -1,
        FAIL = 0,
        SUCCESS = 1,
    }

    internal class Utils
    {
        public static string CalculateComission(int comissionPercentage, string balance)
        {
            return $"{comissionPercentage}% = {int.Parse(balance) * comissionPercentage / 100}$";
        }
    }
}
