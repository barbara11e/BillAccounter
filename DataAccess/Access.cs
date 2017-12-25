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
using System.Reflection;

namespace DataAccess
{
    public class Access: IDAL
    {
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
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
                    log.Info("Данные сохранены успешно!");
                }
            }
            catch (Exception ex)
            {
                log.Error("Операция сохранени новой записи неможет быть выполнена успешно.");
                throw new InvalidDataException("Операция сохранени новой записи неможет быть выполнена успешно.", ex);
            }

        }

        public double GetDataFromDataBase()
        {
            string cnString = DataAccess.Properties.Settings.Default.ConnectionString;
            if (string.IsNullOrWhiteSpace(cnString))
                throw new ArgumentNullException();
            double result = 0;
       
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
                            log.Error("Некорректные данные!");
                            throw new InvalidDataException("Некорректные данные.", e);                         
                        }

                  }
              }
            return result;
        }
        public Access() { }
    }
}
