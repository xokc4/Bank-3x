using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlConn
{
    /// <summary>
    /// бибблиотека для подключения к SQL
    /// </summary>
    public class SqlConn
    {
        /// <summary>
        /// создание строки подключения 
        /// </summary>
        public static SqlConnectionStringBuilder sql = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-G4B9IB0",
            InitialCatalog = "LocalDemo",
            IntegratedSecurity = true,
            Pooling = true,
        };
        /// <summary>
        /// строка подлючения 
        /// </summary>
        public static SqlConnection sqlConnection = new SqlConnection()
        { ConnectionString = sql.ConnectionString };
    }
}
