using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IDAL
    {
        void SetDataToDataBase(string tableName, string category, string billType, double amount, string date);
    }
}
