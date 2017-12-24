using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DataAccess
{
    [DataContract]
    public class CurrencyRate
    {
        [DataMember(Name = "rates")]
        public double Rate { get; set; }
    }
    
}
