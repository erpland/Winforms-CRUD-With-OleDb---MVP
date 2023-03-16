using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Product_Manager.Database
{
    public abstract class BaseDatabase
    {
        protected string connectionString;
        public BaseDatabase()
        {
            connectionString = ConfigurationManager.ConnectionStrings["StoreAccessDb"].ConnectionString;
        }
    }
}
