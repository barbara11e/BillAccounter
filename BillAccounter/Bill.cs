using BL;
using Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BillAccounter
{
    class Bill
    {
        public string Name { get; set; }
        public string  Desc { get; set; }

        public static void SetTypesToDataBase( string tableName, string category, string billType, double amount, string date)
        {
            List<Bill> result = new List<Bill>();
            Config connectionProperty = new Config();
            
            connectionProperty.ConnectionString = BillAccounter.Properties.Settings.Default.ConnectionString;

            MainLogic ml = new MainLogic();
            ml.SetData(tableName, category, billType, amount, date);
            /*
            try
            {
                using (SqlConnection cnn = new SqlConnection(property.ConnectionString))
                {
                    cnn.Open();
                    string sql = string.Format("INSERT INTO dbo.{0} (Category, TypeName, Amount) VALUES ('{1}', '{2}', '{3}')", tableName, category, billType, amount);
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                throw new InvalidDataException("Wrong data!", ex);
            }*/
            
        }

        /*
        public static List<Bill> SetTypesToDataBase()
        {
            string Name = "naame";
            List<Bill> result = new List<Bill>();
            using (SqlConnection cnn = new SqlConnection("Server=ALL\\MSSQLSERVER01;Database=HomeAccount;Trusted_Connection=true"))
            {
                cnn.Open();
                string sql = string.Format("INSERT INTO dbo.Type (   Name) VALUES ('{0}')", Name);
                //var cmd = new SqlCommand("INSERT INTO dbo.Type (Id, Name) VALUES (@Id @name)", cnn);
                SqlCommand cmd = new SqlCommand(sql, cnn);
                //cmd.Parameters.Add("@name", "Gorod");
                cmd.ExecuteNonQuery();
            }
            return result;
        }*/
    }
}
