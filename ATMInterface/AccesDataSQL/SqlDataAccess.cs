using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;
using ATMInterface.DBClassess;
using System.Windows;


namespace ATMInterface.AccesDataSQL
{
    public class SqlDataAccess
    {
        public static int LoadBalance(string id_card)
        {
            int mybalance = 0;
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.QuerySingle<Card>("select balance from card where id_number = " + id_card, 1);
                    mybalance = output.Balance;

                    // MessageBox.Show(mybalance.ToString());
                    return mybalance;
                }

           
        }



        public static bool ExistsId(string id_card)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try {
                    var output = cnn.QuerySingle("select id_number from card where id_number =" + id_card, 1);
                    //MessageBox.Show(output.ToString());
                    return true;
                }
                catch {
                    return false; 
                }
                    
               
            }
        }

        public static bool CorrectPassword(string id_card, string password)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    var output = cnn.QuerySingle<Card>("select pasword from card where id_number =" + id_card, 1);

                    return (password == output.Pasword);
                }
                catch
                {
                   
                    return false;
                }


            }
        }

        public static void UpdateBalance(string id_card, int newBalance)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    cnn.Execute("update card set balance=@bal where id_number =@id", param: new {bal = newBalance, id = id_card });
                }
                catch
                {                   
                }
            }
        }


        public static Card? LoadInfo(string id_card)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    Card output = cnn.QuerySingle<Card>("select * from card where id_number = " + id_card, 1);
                    return output;
                }
                catch {
                    //MessageBox.Show("smth goes wrong with id of a card");
                    return null;
                }
                
            }
        }


        public static Card_bank? LoadBank(string id_bank)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    Card_bank output = cnn.QuerySingle<Card_bank>("select * from bank where id_number = " + id_bank, 1);
                    return output;
                }
                catch
                {
                    return null;
                }

            }
        }

        public static string? LoadBankName(string id_bank)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    Card_bank output = cnn.QuerySingle<Card_bank>("select * from bank where id_number = " + id_bank, 1);
                    return output.Name;
                }
                catch
                {
                    return null;
                }

            }
        }

        public static string LoadCurrency(string id_card)
        {
            string currency = "";
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.QuerySingle<Card>("select currency from card where id_number = " + id_card, 1);
                currency = output.Currency;

                // MessageBox.Show(mybalance.ToString());
                return currency;
            }


        }

        public static int? LoadBankComission(string id_bank)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    Card_bank output = cnn.QuerySingle<Card_bank>("select * from card_bank where id_bank = " + id_bank, 1);
                    return output.Com_put;
                }
                catch
                {
                    return null;
                }

            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
