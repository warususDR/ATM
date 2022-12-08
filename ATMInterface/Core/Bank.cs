﻿using ATMInterface.AccesDataSQL;
using ATMInterface.DBClassess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public abstract class eBank : Node
    {
        protected static int PASSWORD_ATTEMPTS = 3;

        public List<eATMEngine> ATMNetwork { get; set; }
        public eBank(string name, eCommutator _commutator)
            : base(name, _commutator)
        {}

        public abstract bool ATMregister(eATMEngine newATM);
        protected abstract Result ProcessQuery(eLog payload, out int answer);
    }

    public class eMonobank : eBank 
    {
        private eBankUser CurrentUser { get; set; }
        int queryAnswer;
        int passwordInputAttempts;
        public eMonobank(eCommutator _commutator)
            : base("monobank", _commutator)
        {
            ATMNetwork = new List<eATMEngine>();
            Init();
        }
        public override bool ATMregister(eATMEngine newATM)
        {
            ATMNetwork.Add(newATM);
            return true;
        }

        private bool SessionOff(eLog payload)
        {
            if (payload.Header.action == eUserAction.SESSION_OFF)
            {
                CurrentUser = null;
                return true;
            }
            return false;
        }

        private void ProcessAction(eLog payload)
        {
            switch(payload.Header.action)
            {
                case eUserAction.CREDIT_CARD_INSERTED:
                    CreditCardInserted(payload);
                    break;
                case eUserAction.PUT_CASH:
                case eUserAction.GET_CASH:
                case eUserAction.PASSWORD_ENTERED:
                    NewDataEntered(payload);
                    break;

            }
        }

        private void CreditCardInserted(eLog payload)
        {
            CurrentUser = new eBankUser();
            passwordInputAttempts = PASSWORD_ATTEMPTS;
            NewDataEntered(payload);
        }

        private void NewDataEntered(eLog payload)
        {
            var data = CurrentUser.UserData;
            data.MoneyAmount = payload.UserData.MoneyAmount == 0? data.MoneyAmount : payload.UserData.MoneyAmount;
            data.СardNumber = payload.UserData.СardNumber == null? data.СardNumber : payload.UserData.СardNumber;
            data.Password = payload.UserData.Password == null? data.Password : payload.UserData.Password; 
            CurrentUser.UserData = data;
        }

        private void CheckBalance(eLog payload)
        {
            if (payload.Header.action == eUserAction.CHECK_BALANCE)
            {
                Data balanceData = new Data();
                balanceData.MoneyAmount = queryAnswer;
                payload.ApplyData(balanceData);
            }
            else return;
        }

        protected override void Process(eLog payload)
        {
            if (payload.Header.dst == Name)
            {
                if (payload.Header.type == LogType.Req)//request
                {
                    ReqSenders.Push(payload.Header.src);
                    if (ReqSenders.Peek() == "PAYMENT_SYSTEM")
                    {
                        if (SessionOff(payload)) return;

                        ProcessAction(payload);

                        Result res = ProcessQuery(payload, out queryAnswer);

                        if (res == Result.FAIL && payload.Header.action == eUserAction.PASSWORD_ENTERED) passwordInputAttempts--;
                        if(queryAnswer != -1) CheckBalance(payload);

                        if(payload.Header.action == eUserAction.PASSWORD_ENTERED && passwordInputAttempts == 0)
                            Send(eLogger.GenerateLog(payload.Header.action, payload.UserData, ReqSenders.Pop(), Name, LogType.Ack, Result.ERROR));
                        else Send(eLogger.GenerateLog(payload.Header.action, payload.UserData, ReqSenders.Pop(), Name, LogType.Ack, res));
                    }
                    else
                    {
                        Send(eLogger.GenerateLog(payload.Header.action, payload.UserData, "PAYMENT_SYSTEM", Name, payload.Header.type, payload.Header.result));
                    }
                }
                else//answer
                {
                    Send(eLogger.GenerateLog(payload.Header.action, payload.UserData, ReqSenders.Pop(), Name, payload.Header.type, payload.Header.result));
                }
            }
        }
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
                    return cardIsValid ? Result.SUCCESS : Result.FAIL;
                case eUserAction.PASSWORD_ENTERED:
                    return SqlDataAccess.CorrectPassword(id, pass) ? Result.SUCCESS : Result.FAIL;
                case eUserAction.CHECK_BALANCE:
                    answer = card.Balance;
                    return Result.SUCCESS;
                case eUserAction.PRINT_BALANCE:
                    answer = card.Balance;
                    return Result.SUCCESS;
                case eUserAction.GET_CASH:
                    if (card.Balance > 0 && (card.Balance - money) >= 0) { SqlDataAccess.UpdateBalance(id, (card.Balance - money)); return Result.SUCCESS; }
                    else return Result.FAIL;
                case eUserAction.PUT_CASH:
                    if (money <= put_limit) { SqlDataAccess.UpdateBalance(id, (money + card.Balance)); return Result.SUCCESS; }
                    else return Result.FAIL;
                default:
                    return Result.ERROR;
            }
        }
    }
}
