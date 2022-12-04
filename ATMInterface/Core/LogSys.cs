using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Windows.Markup;

namespace ATM
{

    public enum LogType
    { Req, Ack }
    public class eLogger
    {
        public static eLog GenerateLog(eUserAction action, string data, string dst, string src, LogType type, Result result = Result.FAIL) { return new eLog(action, dst, src, type, result, data); }
        public static eLog GenerateLog(eUserAction action, Data data, string dst, string src, LogType type, Result result = Result.FAIL) { return new eLog(action, dst, src, type, data, result); }
        public static eLog GenerateLog(eHeader header, Data data, Result result = Result.FAIL) { return new eLog(header.action, header.dst, header.src, header.type, data, result); }
    }

    public struct Data
    {
        public string СardNumber { get; set; }
        public string Password { get; set; }
        public int MoneyAmount { get; set; }
    }
    public struct eHeader
    {
        public eUserAction action;
        public string dst;
        public string src;
        public LogType type;
        public Result result;
    }

    public class eLog
    {
        public eLog(eUserAction action, string dst, string src, LogType type, Result result, string data = null)
        {
            _data = new Data();

            _header.action = action;
            _header.dst = dst;
            _header.src = src;
            _header.type = type;
            _header.result = result;
            if (data != null) 
            {
                switch (action)
                {
                    case eUserAction.CREDIT_CARD_INSERTED:
                        _data.СardNumber = data;
                        break;
                    case eUserAction.PASSWORD_ENTERED:
                        _data.Password = data;
                        break;
                    case eUserAction.GET_CASH:
                        _data.MoneyAmount = Int32.Parse(data);
                        break;
                }   
            }
        }

        public eLog(eUserAction action, string dst, string src, LogType type, Data data, Result result)
        {
            _data = new Data();

            _header.action = action;
            _header.dst = dst;
            _header.src = src;
            _header.type = type;
            _header.result = result;
            _data = data;
        }

        public eHeader Header => _header;
        public Data UserData => _data;
        private eHeader _header;
        private Data _data;//from node is src, to node is dst answer

        internal void ApplyData(Data userData)
        {
            if(userData.Password != null) { this._data.Password = userData.Password; }
            if(userData.СardNumber != null) { this._data.СardNumber = userData.СardNumber; }
            if (userData.MoneyAmount != 0) { this._data.MoneyAmount = userData.MoneyAmount; }
        }
    }
}
