
using Bank_3x.FolderPeople;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Windows;
using System.Windows.Threading;



namespace Bank_3x
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml

    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// происходит Connection к SQL
        /// </summary>
        public static SqlConnectionStringBuilder sql = SqlConn.SqlConn.sql;
        /// <summary>
        /// строка подключения
        /// </summary>
        public static SqlConnection sqlConnection = SqlConn.SqlConn.sqlConnection;

        DispatcherTimer TimeRefresh = new DispatcherTimer();// время рефреш
        static public List<Histori> historis = new List<Histori>();// коллекция истории переведов и тд
         public static Account account = new Account();
        /// <summary>
        /// статичный id 
        /// </summary>
        static public int Id =-1;
        /// <summary>
        /// запуск приложения и создание бд(пользователей)
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            SqlConn.SqlConn.SqlIf();
            SqlBd();
            
            People.ItemsSource = Account.peoplePost;//передача информации пользователей
            Refresh();
        }
        public void Refresh()
        {
            TimeRefresh.Interval = new TimeSpan(0, 0, 5); // в часах, минутах, секундах. обновление всего происходит каждые 5 секунд
            TimeRefresh.Tick += dtClockTime_Tick;//при обновлении времени включается метод по названии
            TimeRefresh.Start();//запуск работы времени
        }

        private void dtClockTime_Tick(object sender, EventArgs e)//обновление таблицы
        {
            People.ItemsSource = null;
            People.ItemsSource = Account.peoplePost;
        }
        /// <summary>
        /// при нажатии на кнопку происходит проверка введённых данных 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HefNow_Click(object sender, RoutedEventArgs e)
        {
            СheckAccount();
        }

     /// <summary>
     /// проверка данных
     /// </summary>
        public void СheckAccount()
        {
            string login = Convert.ToString(log.Text);//ввод логина
            bool eas = false;//уcловие 
            foreach(var acc in Account.peoplePost)//чтение коллекции с пользователями
            {
                try
                {
                    if (login == acc.Name)// проверка на имя аккаунта
                    {
                        if (pass.Password == acc.Password)//проверка пароля 
                        {
                            Id = acc.ID;//передача имени в статическую переменную 

                            NewWindow();// окрытие другого метода 
                        }
                        else
                        {
                            log.Clear();
                            pass.Clear();// удаление введенного пароля
                            MessageBox.Show("не правильный пароль");//вывод сообщения
                        }
                    }
                    if (eas == true)
                    {
                        log.Clear();
                        pass.Clear();
                        eas = false;
                        MessageBox.Show("нет такого пользователя");//вывод сообщения
                    }
                }
                catch(FormatException)
                {
                    pass.Clear();
                    MessageBox.Show("для пароля нужны только цифры");
                }
            }
        }
        /// <summary>
        /// открытие нового окна 
        /// </summary>
        public void NewWindow()
        {
            account.Show();
        }
        /// <summary>
        /// метод по передачи информации из SQL в коллекцию пользователей 
        /// </summary>
        public void SqlBd()
        {
            try
            {
                sqlConnection.Open();
                var q = @"SELECT 
	            People.ID,
	            People.Name,
	            People.LastName,
	            People.PassWord,
	            People.Money,
	            People.CardNumber,
	            People.CapitalMoney,
	            People.Credit,
	            People.CreditPrecent,
	            TypePeople.TyPeople,
	            People.OpenCard
                FROM People, TypePeople
                WHERE People.Type = TypePeople.ID;";
                SqlCommand command = new SqlCommand(q, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                
                    List<PeoplePost> people = new List<PeoplePost>();
                    while (reader.Read())
                    {
                        people.Add(new PeoplePost(reader["Name"].ToString(), reader["LastName"].ToString(),

                        reader["PassWord"].ToString(), Convert.ToInt32(reader["CardNumber"]),

                        Convert.ToInt32(reader["Money"]), Convert.ToInt32(reader["CapitalMoney"]),

                        Convert.ToInt32(reader["Credit"]), Convert.ToInt32(reader["CreditPrecent"]),

                        reader["TyPeople"].ToString(), Convert.ToInt32(reader["ID"]), Convert.ToBoolean(reader["OpenCard"])));
                    }
                    Account.peoplePost = people;
            }
            catch(SqlException e)
            {
                if(e.State == 1)
                {
                    SqlConn.SqlConn.SqlBDCreat();
                    MessageBox.Show("Были созданы клиенты для банка, тк не было никого");
                }
                else
                {
                    SqlParametrs sqlParametrs = new SqlParametrs();
                    sqlParametrs.Show();
                    this.Close();
                    MessageBox.Show("Если вышла ошибка то проблема в подключении базы данных(SQL server), для решения этой проблемы. №1 создайте " +
                        "базу данных, №2 напишите данные о базе в форму, если не получилось посмотрите правильность написания.");
                }
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        /// <summary>
        /// Обнавление информации о пользователе в SQL
        /// </summary>
        public static void UpDate()
        {
            try
            {
               if(Id == -1)
               {
               }
               else
               {
                    sqlConnection.Open();
                    var q = @"";
                    foreach (var people in Account.peoplePost)
                    {
                        if (Id == people.ID)
                        {
                            int boll = ConBool(people.OpenCard);
                            
                        q = $@"UPDATE People
                        SET People.Name = '{people.Name}',
	                        People.LastName ='{people.LastName}',
	                        People.PassWord ='{people.Password}',
	                        People.Money ='{people.Money}',
	                        People.CardNumber ='{people.CardNumber}',
	                        People.CapitalMoney ='{people.CapitalMoney}',
	                        People.Credit ='{people.Credit}',
	                        People.CreditPrecent ='{people.CreditPrecent}',
	                        People.OpenCard ='{boll}'
                        WHERE People.ID = '{Id}';";
                        }
                    }
                    SqlCommand command = new SqlCommand(q, sqlConnection);
                    command.ExecuteNonQuery();
               } 
            }
            catch (Exception e)
            {
                
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        /// <summary>
        /// скрипт в SQL для перевода денег пользователю
        /// </summary>
        /// <param name="idPeople"></param>
        public static void UpDateTranslate(object idPeople)
        {
            int id = Convert.ToInt32(idPeople);
            try
            {
                    sqlConnection.Open();
                    var q = @"";
                    foreach (var people in Account.peoplePost)
                    {
                        if (id == people.ID)
                        {
                            int boll = ConBool(people.OpenCard);

                            q = $@"UPDATE People
                        SET People.Money ='{people.Money}'
                        WHERE People.ID = '{idPeople}';";
                        }
                    }
                    SqlCommand command = new SqlCommand(q, sqlConnection);
                    command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        /// <summary>
        /// метод по записи Bool значений в SQL
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static int ConBool(bool e)
        {
            if (e == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
