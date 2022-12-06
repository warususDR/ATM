using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class eATM
    {
        /*private ePrinter printer;*/
        private eATMEngine engine;
        public eATMEngine Engine => engine;
        /*
        internal ePrinter Printer { get => printer; set => printer = value; }*/

        public eATM(eCommutator _commutator, eBank _bankAcquire)
        {
            engine = new eATMEngine(this, _commutator, _bankAcquire);
        }
    }

    /*internal class ePrinter
    {
        
    }*/
}