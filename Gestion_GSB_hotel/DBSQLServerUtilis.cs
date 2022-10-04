using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Gestion_GSB_hotel
{
     class DBSQLServerUtilis
    {
        public static SqlConnection
                GetDBConnection(string datasource, string database)
        {
            //
            // Data Source=TRAN-VMWARE\SQLEXPRESS;Initial Catalog=simplehr;Persist Security Info=True;User ID=sa;Password=12345
            //Data Source=DESKTOP-FARHMED;Initial Catalog=Hotel_Management_System;Integrated Security=True

            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True";
            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }
    }
}
