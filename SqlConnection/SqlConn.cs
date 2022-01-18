using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
        static private string pathFile = @"Connection.txt";
        static public FileInfo file = new FileInfo(pathFile);
        /// <summary>
        /// создание строки подключения 
        /// </summary>
        public static SqlConnectionStringBuilder sql = new SqlConnectionStringBuilder();
        /// <summary>
        /// строка подлючения 
        /// </summary>
        public static SqlConnection sqlConnection = new SqlConnection();
        /// <summary>
        /// метод для строки подключении в файле
        /// </summary>
        public static string SqlIf()
        {
                string text;
                FileStream stream = new FileStream(pathFile, FileMode.OpenOrCreate);
                StreamReader reader = new StreamReader(stream);
                text = reader.ReadToEnd();
                string[] vs = text.Split(new char[]{';'});
                if(vs.Length > 1)
                {
                    sql.Pooling = true;
                    sql.DataSource = vs[0];
                    sql.InitialCatalog = vs[1];
                    sql.IntegratedSecurity = Convert.ToBoolean(vs[2]);
                }
                else
                {
                    sql.Pooling = false;
                    sql.DataSource = "DesKTOP";
                    sql.InitialCatalog = "revolt";
                    sql.IntegratedSecurity = Convert.ToBoolean(null);
                }
                sqlConnection.ConnectionString = sql.ConnectionString;
            string Connect;
            return Connect = sql.ConnectionString.ToString();    
        }
        /// <summary>
        /// изменение строки подключение в файле
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="InitialCatalog"></param>
        /// <param name="IntegratedSecurity"></param>
        public static void sqlconFile(string dataSource, string InitialCatalog, bool IntegratedSecurity)
        {
            using (StreamWriter sw = new StreamWriter(pathFile, false, System.Text.Encoding.Default))
            {
                sw.WriteLine($"{dataSource};{InitialCatalog};{IntegratedSecurity}");
            }
        }
    }
}
