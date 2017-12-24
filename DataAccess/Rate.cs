using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DataAccess
{
    [DataContract(Name="rates")]
    public class Rate
    {
        [DataMember(Name = "RUB")]
        public double rub { get; set; }
    }
}
