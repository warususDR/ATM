using ATMInterface.AccesDataSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class eMonobank : eBank
    {
        static private string bankCode = "11";
        public eMonobank(eCommutator _commutator)
            : base(bankCode, _commutator)
        {}
    }
}