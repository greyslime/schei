using Microsoft.VisualBasic;
using System;
using System.Data.SqlClient;

namespace Schei
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection cnn;

            cnn = new SqlConnection(@"Data Source=LAPTOP-GVEUPMBQ\SQLEXPRESS;Initial Catalog=AndroMoney;User ID=sqlexpress;Password=sqlexpress");
            cnn.Open();

            decimal tot;
            tot = 0;

            for (int year = 2017; year < 2021; year++)
            {
                Console.WriteLine("=========================" + year + "=========================");
                for (int month = 1; month <= 12; month++)
                {
                    decimal entrate, uscite;

                    entrate = (decimal) FastQuery("select case when SUM(IMPORTO) is null then 0 else SUM(IMPORTO) end from GUADAGNI where Anno = " + year + " and Mese = " + month, cnn);
                    uscite = (decimal) FastQuery("select case when SUM(IMPORTO) is null then 0 else SUM(IMPORTO) end from SPESE where Anno = " + year + " and Mese = " + month, cnn);

                    tot += entrate - uscite;
                    Console.WriteLine(month + ": " + (entrate - uscite));
                }
            }

            Console.WriteLine("TOT: " + tot);
            //while(dr.Read())
            cnn.Close();
        }

        public static object FastQuery(string sql, SqlConnection cnn)
        {
            SqlCommand cmd; SqlDataReader dr; object result;
            cmd = new SqlCommand(sql, cnn);
            dr = cmd.ExecuteReader();
            dr.Read();
            result = dr.GetValue(0);
            dr.Close();
            return result;
        }
    }
}
