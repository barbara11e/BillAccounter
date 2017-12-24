using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using log4net;
using log4net.Config;

namespace DataAccess
{
    public class Access: IDAL
    {
        //Logger.InitLogger();//инициализация - требуется один раз в начале
        Logger.InitLogger();
        Logger.Log.Info("Logger initialized.");
        public void SetDataToDataBase(string tableName, string category, string billType, double amount, string date)
        {
            string cnString = DataAccess.Properties.Settings.Default.ConnectionString;
            if (string.IsNullOrWhiteSpace(cnString))
                throw new ArgumentNullException("Строка подключения пуста!");
            try
            {
                using (SqlConnection cnn = new SqlConnection(cnString))
                {
                    cnn.Open();
                    string sql = string.Format("INSERT INTO dbo.{0} (Category, TypeName, Amount, Date) VALUES ('{1}', '{2}', '{3}', '{4}')", tableName, category, billType, amount, date);
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    //
                    //Logger.Log.Error("Данные сохранены успешно!");
                }
            }
            catch (Exception ex)
            {
                ////Logger.Log.Error("Операция сохранени новой записи неможет быть выполнена успешно.");
                throw new InvalidDataException("Операция сохранени новой записи неможет быть выполнена успешно.", ex);
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
                        try
                        {
                            result = (double)studentReader["Summ"];
                        }
                        catch(Exception e)
                        {
                       return result = 0;
                        ///LOGGER
                        ///Logger.Log.Error("Некорректные данные!");
                        throw new InvalidDataException("Некорректные данные.", e);
                         
                    }

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
