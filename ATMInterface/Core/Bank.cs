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

        protected override Result ProcessQuery(eLog payload, out int answer)
        {
            //query impl
            if(payload.Header.action == eUserAction.CHECK_BALANCE) answer = 1000;
            answer = 1;
            return answer == -1 ? Result.ERROR : answer == 0 ? Result.FAIL : Result.SUCCESS;
        }
    }
}
