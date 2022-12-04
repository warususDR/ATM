using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMInterface.DBClassess
{
    public class Card_bank
    {
        public int Id_bank { get; set; }
        public string Name { get; set; }
        public int Usd_buy { get; set; }
        public int Usd_sell { get; set; }
        public int Eur_buy { get; set; }
        public int Eur_sell { get; set; }
        public int Com_to_us { get; set; }
        public int Com_from_us { get; set; }
    }
}
