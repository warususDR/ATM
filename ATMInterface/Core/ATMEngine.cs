using System;
using System.Collections.Generic;
using System.Text;

namespace ATM
{
    public class eATMEngine : Node
    {
        private eBank bankAcquire;
        private eATM ATMowner;
        private bool sessionIsOn = false;
        private int result = 0;
        private Tuple<string, string> printInfo;
        public eATMEngine(eATM _owner, eCommutator _commutator, eBank _bankAcquire)
            : base("ATM", _commutator)
        {
            this.ATMowner = _owner;
            bankAcquire = _bankAcquire;
            bankAcquire.ATMregister(this);
            Init();
        }
        public void OnNewSession()//to use by interface
        {
            sessionIsOn = true;
        }
        public void SessionIsOver()//to use by interface
        {
            sessionIsOn = false;
            Send(eLogger.GenerateLog(eUserAction.SESSION_OFF, "", bankAcquire.Name, Name, LogType.Req));
        }
        public int OnUserInput(eUserAction _action, string _userInput)//to use by interface
        {
            if (sessionIsOn) ProcessAction(_action, _userInput);
            return result;
        }

        public Tuple<string, string> OnUserInput(eUserAction _action)//to use by interface
        {
            if (sessionIsOn) ProcessAction(_action, "");
            return printInfo;
        }
        private bool ProcessAction(eUserAction _action, string _userInput)
        {
            Send(eLogger.GenerateLog(_action, _userInput, bankAcquire.Name, Name, LogType.Req));
            return true;
        }

        protected override void Process(eLog _payload)
        {
            if (_payload.Header.dst == Name)
            {
                if (_payload.Header.type == LogType.Req)//request
                {
                }
                else//answer
                {
                    if (_payload.Header.action == eUserAction.PRINT_BALANCE || _payload.Header.action == eUserAction.CHECK_BALANCE)
                    {
                        printInfo = new Tuple<string, string>(_payload.UserData.СardNumber, _payload.UserData.MoneyAmount.ToString());
                    }
                    else
                    {
                        result = _payload.Header.result.Equals(Result.ERROR) ? -1 : _payload.Header.result.Equals(Result.FAIL) ? 0 : 1;
                    }
                }
            }
        } 
    }
}
