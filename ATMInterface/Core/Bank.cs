using ATMInterface.AccesDataSQL;
using ATMInterface.DBClassess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public abstract class eBank : Node
    {
        public List<eATMEngine> ATMNetwork { get; set; }
        public eBank(string name, eCommutator _commutator)
            : base(name, _commutator)
        {}

        public abstract bool ATMregister(eATMEngine newATM);
        protected abstract Result ProcessQuery(eLog payload, out int answer);
    }

    public class ePrivatBank : eBank 
    {
        private eBankUser CurrentUser { get; set; }
        int queryAnswer;//shit code might delete later
        public ePrivatBank(eCommutator _commutator)
            : base("PrivatBank", _commutator)
        {
            ATMNetwork = new List<eATMEngine>();
            init();
        }
        public override bool ATMregister(eATMEngine newATM)
        {
            ATMNetwork.Add(newATM);
            return true;
        }

        public override void receive(eLog payload)
        {
            Process(payload);
        }

        private bool Process(eLog payload)
        {
            if (payload.Header.dst == Name)
            {
                if (payload.Header.type == LogType.Req)//request
                {
                    ReqSenders.Push(payload.Header.src);
                    if (ReqSenders.Peek() == "PAYMENT_SYSTEM")
                    {
                        
                        
                        if (payload.Header.action == eUserAction.CREDIT_CARD_INSERTED) 
                        { 
                            CurrentUser = new eBankUser(); 
                            var data = CurrentUser.UserData; 
                            data.СardNumber = payload.UserData.СardNumber; 
                            CurrentUser.UserData = data; 
                        }
                        //else if (payload.Header.action == eUserAction.SESSION_OFF) { CurrentUser = null; return true; }
                        else if (payload.Header.action == eUserAction.PASSWORD_ENTERED) { var data = CurrentUser.UserData; data.Password = payload.UserData.Password; CurrentUser.UserData = data; }
                        else if (payload.Header.action == eUserAction.GET_CASH || payload.Header.action == eUserAction.PUT_CASH) { var data = CurrentUser.UserData; data.MoneyAmount = payload.UserData.MoneyAmount; CurrentUser.UserData = data; }

                        Result res = ProcessQuery(payload, out queryAnswer);
                        if (payload.Header.action == eUserAction.CHECK_BALANCE)
                        {
                            Data balanceData = new Data();
                            balanceData.MoneyAmount = queryAnswer;
                            payload.ApplyData(balanceData);
                        }
                        send(eLogger.GenerateLog(payload.Header.action, payload.UserData, ReqSenders.Pop(), Name, LogType.Ack, res));

                    }
                    else
                    {
                        send(eLogger.GenerateLog(payload.Header.action, payload.UserData, "PAYMENT_SYSTEM", Name, payload.Header.type));
                    }
                }
                else//answer
                {
                    send(eLogger.GenerateLog(payload.Header.action, payload.UserData, ReqSenders.Pop(), Name, payload.Header.type));
                }
                return true;
            }
            return false;
        }

        //public enum eUserAction
        //{
        //    CREDIT_CARD_INSERTED,
        //    PASSWORD_ENTERED,
        //    CHECK_BALANCE,
        //    PRINT_BALANCE,//same action as check balance?...
        //    GET_CASH,
        //    PUT_CASH,
        //    SESSION_ON,
        //    SESSION_OFF,
        //}

        //public enum Result
        //{
        //    ERROR = -1,
        //    FAIL = 0,
        //    SUCCESS = 1,
        //}

        protected override Result ProcessQuery(eLog payload, out int answer)
        {
            string id = CurrentUser.UserData.СardNumber;
            string pass = CurrentUser.UserData.Password;
            int money = CurrentUser.UserData.MoneyAmount;
            bool cardIsValid = SqlDataAccess.ExistsId(id);
            Card card = new Card();
            if (cardIsValid) 
            {
                card = SqlDataAccess.LoadInfo(id);
            } 

            int put_limit = 1000;
            answer = -1;

            switch (payload.Header.action)
            {
                case eUserAction.CREDIT_CARD_INSERTED:
                    answer = cardIsValid ? 1 : 0;
                    break;
                case eUserAction.PASSWORD_ENTERED:
                    answer = SqlDataAccess.CorrectPassword(id, pass) ? 1 : 0;
                    break;
                case eUserAction.CHECK_BALANCE:
                    answer = card.Balance;
                    break;
                case eUserAction.PRINT_BALANCE:
                    answer = card.Balance;
                    break;
                case eUserAction.PUT_CASH:
                    answer = (card.Balance >= 0 && (card.Balance - money) >= 0) ? 1 : 0;
                    break;
                case eUserAction.GET_CASH:
                    answer = (money <= put_limit) ? 1 : 0;
                    break;
            }

            return answer == -1 ? Result.ERROR : answer == 0 ? Result.FAIL : Result.SUCCESS;
            //if (payload.Header.action == eUserAction.CHECK_BALANCE) answer = 3000;
            //answer = 1;
            //return answer == -1 ? Result.ERROR : answer == 0 ? Result.FAIL : Result.SUCCESS;
        }
    }
}
