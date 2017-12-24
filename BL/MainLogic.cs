using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using DataAccess;

namespace BL
{
    public class MainLogic
    {

        public MainLogic()
        {

        }
        
        public void SetData(string tableName, string category, string billType, double amount, string date)
        {
            //REVIEW: enum надо
            amount = (billType == "Расход") ? -amount : amount;
            Access ac = new Access();
            ac.SetDataToDataBase(tableName, category, billType, amount, date);
        }
        public string GetCurrency()
        {
            CurrencyRequest makeRequest = new CurrencyRequest();
            return makeRequest.GetCurrency();

        }
        public static void GenerateExcel()
        {
            FileGenerator generator = new FileGenerator();
            FileGenerator.GenerateExcel();

        }
        public double GetDataFromDataBase()
        {
            Access ac = new Access();
            double summ = ac.GetDataFromDataBase();
            return summ;
        }
    }
}
