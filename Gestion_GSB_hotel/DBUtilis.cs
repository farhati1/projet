using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Gestion_GSB_hotel
{
     class DBUtilis
    {
        public static SqlConnection GetDBConnection()
        {
            string datasource = @"DESKTOP-FARHMED";
            string database = "Hotel_Management_System";
            return DBSQLServerUtilis.GetDBConnection(datasource, database);
        }
    }
}
