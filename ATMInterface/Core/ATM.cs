using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class eATM
    {
        //has interface in it,
        /*private eMoneyChecker moneyChecker;
        private ePrinter printer;*/
        private eATMEngine engine;
        public eATMEngine Engine => engine;
        /*internal eMoneyChecker MoneyChecker { get => moneyChecker; set => moneyChecker = value; }
        internal ePrinter Printer { get => printer; set => printer = value; }*/

        public eATM(eCommutator _commutator, eBank _bankAcquire)
        {
            engine = new eATMEngine(this, _commutator, _bankAcquire);
        }
    }

    //engine just receives info about action
    //encodes and sends via commutator
    
    /*internal class eMoneyChecker 
    {
        public bool paperValid()
        {
            var rand = new Random();
            return rand.Next(2) == 1;//accepts or not
        }
    }*/

    /*internal class ePrinter
    {
        
    }*/
}