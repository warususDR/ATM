using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class eATM
    {
        private eATMEngine engine;
        public eATMEngine Engine => engine;

        public eATM(eCommutator _commutator, eBank _bankAcquire)
        {
            engine = new eATMEngine(this, _commutator, _bankAcquire);
        }
    }
}