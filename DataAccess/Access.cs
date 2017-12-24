using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace DataAccess
{
    public class Access: IDAL
    {
        /*
        public static void WriteData()
        {
            string cnString = DataAccess.Properties.Settings.Default.ConnectionString;
            if (string.IsNullOrWhiteSpace(cnString))
                throw new ArgumentNullException();
        }*/
        public void SetDataToDataBase(string tableName, string category, string billType, double amount, string date)
        {
            string cnString = DataAccess.Properties.Settings.Default.ConnectionString;
            if (string.IsNullOrWhiteSpace(cnString))
                throw new ArgumentNullException();

            try
            {
                using (SqlConnection cnn = new SqlConnection(cnString))
                {
                    cnn.Open();
                    string sql = string.Format("INSERT INTO dbo.{0} (Category, TypeName, Amount, Date) VALUES ('{1}', '{2}', '{3}', '{4}')", tableName, category, billType, amount, date);
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show("Данные сохранены успешно!");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Wrong data!", ex);
            }

        }

        public double GetDataFromDataBase()
        {
            string cnString = DataAccess.Properties.Settings.Default.ConnectionString;
            if (string.IsNullOrWhiteSpace(cnString))
                throw new ArgumentNullException();
            double result = 0;
           
                //List<Bill> result = new List<Bill>(); +++++
                using (SqlConnection cnn = new SqlConnection(cnString))
                {
                    cnn.Open();
                    string sql = string.Format("select SUM(Amount) as Summ from Bill;");
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    var studentReader = cmd.ExecuteReader();
                    while (studentReader.Read())
                    {
                        result = (double)studentReader["Summ"];
                        //создаем экземпляр и записываем в его поля данные Student s= new Student();
                        //s.Id = (Int)studentReader["Id"]; ++++
                        //result.Add(s); +++++
                    }

                   // cmd.ExecuteNonQuery();
                    
                    //MessageBox.Show("Данные сохранены успешно!");
                }
            
            return result;
        }

        public Access() { }

        
    }
}
