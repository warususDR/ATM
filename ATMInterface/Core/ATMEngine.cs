using System;
using System.Collections.Generic;
using System.Text;

namespace ATM
{
    public class eATMEngine : Node
    {
        private eBank bankAcquire;
        private eATM ATMowner;
        //bool sessionIsOn = false;
        private int result = 0;
        public eATMEngine(eATM _owner, eCommutator _commutator, eBank _bankAcquire)
            : base("ATM", _commutator)
        {
            this.ATMowner = _owner;
            bankAcquire = _bankAcquire;
            bankAcquire.ATMregister(this);
            init();
        }
/*        public void OnNewSession()//to use by interface
        {
            sessionIsOn = true;
        }
        public void SessionIsOver()//to use by interface
        {
            sessionIsOn = false;
            send(eLogger.GenerateLog(eUserAction.SESSION_OFF, "", bankAcquire.Name, Name, LogType.Req));
        }*/
        public int OnUserInput(eUserAction _action, string _userInput)//to use by interface
        {
            ProcessAction(_action, _userInput);

            return result;
        }
        private bool ProcessAction(eUserAction _action, string _userInput)
        {
            send(eLogger.GenerateLog(_action, _userInput, bankAcquire.Name, Name, LogType.Req));
            return true;
        }

        public override void receive(eLog _payload)
        {
            if (_payload.Header.dst == Name)
            {
                if (_payload.Header.type == LogType.Req)//request
                {
                }
                else//answer
                {
                    if (_payload.Header.action == eUserAction.CHECK_BALANCE) result = _payload.UserData.MoneyAmount;
                    else
                        result = _payload.Header.result.Equals("FAIL") ? -1 : _payload.Header.result.Equals("ERROR") ? 0 : 1;
                }
            }
        } 
    }
}
