using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{

    //todo:
    //en/decoders implementation
    //try to get rid of multiple usage of "BANK", "PAYMENT_SYSTEM" etc
    //test with another operations
    //check for patterns eligibility

    /*class Program
    {
        eCommutator commutator;
        eATM currentATM;
        ePaymentSystem ps;
        ePrivatBank pb;
        public Program()
        {
            commutator = new eCommutator();
            pb = new ePrivatBank(commutator);
            currentATM = new eATM(commutator, pb);
            ps = new ePaymentSystem(commutator);
        }


        static void Main(string[] args)
        {
            Program a = new Program();
            a.currentATM.Engine.OnNewSession();
            a.currentATM.Engine.OnUserInput(eUserAction.CREDIT_CARD_INSERTED, "4441114425127188");
        }
    }*/
}
