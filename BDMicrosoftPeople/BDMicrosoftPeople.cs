using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace BDMicrosoftPeople
{
    public class BDMicrosoftPeople
    {
        public static SqlConnectionStringBuilder sql = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP - G4B9IB0",
            InitialCatalog = "LocalDemo",
            IntegratedSecurity = true,
            Pooling = true,
        };
        public static SqlConnection sqlConnection = new SqlConnection()
        { ConnectionString = sql.ConnectionString };
        public void Connect()
        {
            try
            {
                sqlConnection.Open();
            }
            catch(Exception e)
            {
                
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
