using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public enum eUserAction
    {
        CREDIT_CARD_INSERTED,
        PASSWORD_ENTERED,
        CHECK_BALANCE,
        PRINT_BALANCE,//same action as check balance?...
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

    class utils
    {
        public static string UserActionToString(eUserAction action)
        {
            switch (action)
            {
                case eUserAction.CREDIT_CARD_INSERTED:
                    return "CREDIT_CARD_INSERTED";
                case eUserAction.PASSWORD_ENTERED:
                    return "PASSWORD_ENTERED";
                case eUserAction.CHECK_BALANCE:
                    return "CHECK_BALANCE";
                case eUserAction.PRINT_BALANCE:
                    return "PRINT_BALANCE";
                case eUserAction.GET_CASH:
                    return "GET_CASH";
                case eUserAction.PUT_CASH:
                    return "PUT_CASH";
                case eUserAction.SESSION_ON:
                    return "SESSION_ON";
                case eUserAction.SESSION_OFF:
                    return "SESSION_OFF";

            }
            return null;
        }

        public static string ResultToString(int result)
        {
            switch(result)
            {
                case -1:
                    return "ERROR";
                case 0:
                    return "FAIL";
                case 1:
                    return "SUCCESS";
            }
            return null;
        }
    }
    
}
