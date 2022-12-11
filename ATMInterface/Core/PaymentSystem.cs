using ATMInterface.AccesDataSQL;

namespace ATM
{
    class ePaymentSystem : Node
    {
        public string BankEmitent;
        internal class Router
        {
            public static string? DetermineBank(string bankCode)
            {
                return SqlDataAccess.LoadBankName(bankCode);
            }
        }
        public ePaymentSystem(eCommutator _commutator)
            : base("PAYMENT_SYSTEM", _commutator)
        {
            Init();
        }

        protected override void Process(eLog payload)
        {
            if (payload.Header.dst == Name)
            {
                if (payload.Header.type == LogType.Req)
                {
                    ReqSenders.Push(payload.Header.src);
                    if (payload.Header.action == eUserAction.CREDIT_CARD_INSERTED)
                        BankEmitent = Router.DetermineBank(payload.UserData.СardNumber.Substring(0, 2));

                    if(BankEmitent == null) 
                        Send(eLogger.GenerateLog(payload.Header.action, payload.UserData, payload.Header.dst, Name, LogType.Ack, payload.Header.result));
                    else
                        Send(eLogger.GenerateLog(payload.Header.action, payload.UserData, BankEmitent, Name, payload.Header.type, payload.Header.result));
                }
                else
                {
                    Send(eLogger.GenerateLog(payload.Header.action, payload.UserData, ReqSenders.Pop(), Name, payload.Header.type, payload.Header.result));
                }
            }
        }
    }
}
