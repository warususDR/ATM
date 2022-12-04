using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMInterface.DBClassess
{
    public class Transaction
    {
        public int Id_transaction { get; set; }
        public int Id_card_sender { get; set; }
        public int Id_card_receiver { get; set; }
        public string Date { get; set; }
        public int Amount { get; set; }
    }
}
