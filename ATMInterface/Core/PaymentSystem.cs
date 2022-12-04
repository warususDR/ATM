using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
   
    class ePaymentSystem : Node
    {
        Dictionary<string, string> cardsCodes;//create dictionary<string, list<string>>
        public string BankEmitent;
        public ePaymentSystem(eCommutator _commutator)
            : base("PAYMENT_SYSTEM", _commutator)
        {
            cardsCodes = new Dictionary<string, string>();//it should be database also..
            init();

            cardsCodes.Add("PrivatBank", "1111");
        }

        public override void receive(eLog payload)
        {
            Process(payload);
        }

        private bool Process(eLog payload)
        {
            if (payload.Header.dst == Name)
            {

                if (payload.Header.type == LogType.Req)
                {
                    ReqSenders.Push(payload.Header.src);
                    if(BankEmitent == null) BankEmitent = cardsCodes.First(i => i.Value == payload.UserData.СardNumber.Substring(0, 4)).Key;//must be a database query?
                    send(eLogger.GenerateLog(payload.Header.action, payload.UserData, BankEmitent, Name, payload.Header.type));
                }
                else
                {
                    send(eLogger.GenerateLog(payload.Header.action, payload.UserData, ReqSenders.Pop(), Name, payload.Header.type));
                }
                return true;
            }
            return false;
        }
    }
}
