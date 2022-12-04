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
                    MessageBox.Show("false");
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
                    
                    if(password == output.Pasword) {  return true; }
                    else return false;
                }
                catch
                {
                   
                    return false;
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
