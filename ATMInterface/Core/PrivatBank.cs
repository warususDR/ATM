using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class ePrivatBank : eBank
    {
        static private string bankCode = "22";
        public ePrivatBank(eCommutator _commutator)
            : base(bankCode, _commutator)
        {}
    }
}