using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class ePaymentSystem : Node
    {
        public string BankEmitent;
        public ePaymentSystem(eCommutator _commutator)
            : base("PAYMENT_SYSTEM", _commutator)
        {
            init();
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
                    if (BankEmitent == null) BankEmitent = "monobank";//here should be method to get name from db
                    send(eLogger.GenerateLog(payload.Header.action, payload.UserData, BankEmitent, Name, payload.Header.type, payload.Header.result));
                }
                else
                {
                    send(eLogger.GenerateLog(payload.Header.action, payload.UserData, ReqSenders.Pop(), Name, payload.Header.type, payload.Header.result));
                }
                return true;
            }
            return false;
        }
    }
}
