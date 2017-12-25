using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class Config
    {
        public string ConnectionString { get; set; }
        public string AppID { get; set; }
        
    }
}
