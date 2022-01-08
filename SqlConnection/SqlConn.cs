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
        public static void SqlIf()
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

        /// <summary>
        /// Создание клиентов в банк
        /// </summary>
        public static void SqlBDCreat()
        {
            try
            {
                var s ="CREATE TABLE People([ID] INT IDENTITY(1,1) NOT NULL,[Name] NVARCHAR(50) NOT NULL,[LastName] NVARCHAR(50) NOT NULL," +
                    "[PassWord] INT NOT NULL,[CardNumber] INT NOT NULL,[Money] INT NULL,[CapitalMoney] INT NULL,[Credit] INT NULL," +
                    "[CreditPrecent] INT NULL,[Type] INT NOT NULL,[OpenCard] TINYINT NOT NULL);" +
                    "INSERT People VALUES('Name_1', 'LastName_1', '56678', '119324360', '45000', '0', '0', '0', '1', '1'),('Name_2', 'LastName_2', '16379', '219324369', '90000', '0', '0', '0', '1', '1')," +
                    "('Name_3', 'LastName_3', '12665', '615324344', '70000', '0', '0', '0', '1', '1'),('Name_4', 'LastName_4', '24470', '519624336', '64000', '0', '0', '0', '1', '1')," +
                    "('Name_5', 'LastName_5', '86335', '419374367', '99000', '0', '0', '0', '1', '1'),('Name_6', 'LastName_6', '97700', '319384363', '14000', '0', '0', '0', '1', '1')," +
                    "('Name_7', 'LastName_7', '52689', '232324367', '50000', '0', '0', '0', '1', '1'),('Name_8', 'LastName_8', '54356', '559520068', '33000', '0', '0', '0', '1', '1')," +
                    "('Name_9', 'LastName_9', '12378', '229324449', '44000', '0', '0', '0', '1', '1'),('Name_10', 'LastName_10', '56611', '719324368', '98000', '0', '0', '0', '1', '1')," +
                    "('Name_11', 'LastName_11', '56324', '619335362', '55000', '0', '0', '0', '1', '1'),('Name_12', 'LastName_12', '22478', '519332061', '58000', '0', '0', '0', '1', '1')," +
                    "('Name_13', 'LastName_13', '67228', '132366967', '74400', '0', '0', '0', '1', '1'),('Name_14', 'LastName_14', '47118', '352368767', '75500', '0', '0', '0', '1', '1')," +
                    "('Name_15', 'LastName_15', '41798', '212366767', '57000', '0', '0', '0', '1', '1'),('Name_16', 'LastName_16', '32298', '176365766', '67000', '0', '0', '0', '1', '1')," +
                    "('Name_17', 'LastName_17', '55798', '112369962', '77000', '0', '0', '0', '3', '1'),('Name_18', 'LastName_18', '11733', '100326960', '88000', '0', '0', '0', '1', '1')," +
                    "('Name_19', 'LastName_19', '11228', '118516761', '99000', '0', '0', '0', '2', '1'),('Name_20', 'LastName_20', '11558', '812366763', '35000', '0', '0', '0', '2', '1')," +
                    "('Name_21', 'LastName_21', '60044', '802366766', '86000', '0', '0', '0', '1', '1'),('Name_22', 'LastName_22', '22277', '332336769', '88000', '0', '0', '0', '1', '1')," +
                    "('Name_23', 'LastName_23', '34569', '662366760', '44000', '0', '0', '0', '1', '1'),('Name_24', 'LastName_24', '36345', '882366888', '16700', '0', '0', '0', '3', '1')," +
                    "('Name_25', 'LastName_25', '96547', '662366767', '88600', '0', '0', '0', '3', '1'),('Name_26', 'LastName_26', '65498', '112366545', '57800', '0', '0', '0', '3', '1')," +
                    "('Name_27', 'LastName_27', '65543', '882366733', '99000', '0', '0', '0', '3', '1'),('Name_28', 'LastName_28', '45658', '662366711', '88000', '0', '0', '0', '2', '1')," +
                    "('Name_29', 'LastName_29', '37700', '332366767', '11000', '0', '0', '0', '2', '1'),('Name_30', 'LastName_30', '46567', '882366333', '44000', '0', '0', '0', '2', '1');" +
                    "CREATE TABLE TypePeople([ID] INT IDENTITY(1,1) NOT NULL,[TyPeople] NVARCHAR(20) NOT NULL); " +
                    "INSERT TypePeople VALUES('Legal'),('Regular'),('Vip');";
                SqlCommand command = new SqlCommand(s, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
