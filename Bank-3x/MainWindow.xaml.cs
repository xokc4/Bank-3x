
using Bank_3x.FolderPeople;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
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
       static Entities1 entities = new Entities1();
       
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
            SqlEntityFrmework();

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
        /// база данных через entityFramework
        /// </summary>
        public void SqlEntityFrmework()
        {
            try
            {
                var refresh = entities.People.Join(entities.TypePeople,
                     peopleBD => peopleBD.Type,
                     typePeopleBD => typePeopleBD.ID,
                     (peopleBD, typePeopleBD) => new
                     {
                         peopleBD.ID,
                         peopleBD.Name,
                         peopleBD.LastName,
                         peopleBD.PassWord,
                         peopleBD.Money,
                         peopleBD.CardNumber,
                         peopleBD.CapitalMoney,
                         peopleBD.Credit,
                         peopleBD.CreditPrecent,
                         typePeopleBD.TyPeople,
                         peopleBD.OpenCard
                     });
                List <PeoplePost> people = new List<PeoplePost>();
                foreach (var item in refresh)
                {
                    people.Add(new PeoplePost(item.Name.ToString(), item.LastName.ToString(),

                    item.PassWord.ToString(), item.CardNumber,

                    Convert.ToInt32(item.Money), Convert.ToInt32(item.CapitalMoney),

                    Convert.ToInt32(item.Credit), Convert.ToInt32(item.CreditPrecent),

                    item.TyPeople.ToString(), Convert.ToInt32(item.ID), Convert.ToBoolean(item.OpenCard)));
                }
                Account.peoplePost = people;
                if (Account.peoplePost.Count < 1)
                {
                    SqlBDCreat();
                    MessageBox.Show("Были созданы клиенты для банка, тк не было никого");
                }
            }
            catch(Exception e)
            {
                    SqlParametrs sqlParametrs = new SqlParametrs();
                    sqlParametrs.MessageException();
                    sqlParametrs.Show();
                    this.Close();
                    
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
                    var q = @"";
                    foreach (var people in Account.peoplePost)
                    {
                        if (Id == people.ID)
                        {
                            int boll = ConBool(people.OpenCard);
                            var item = entities.People.Where(e => e.ID == Id).FirstOrDefault();
                            item.Name = people.Name;
                            item.LastName = people.LastName;
                            item.PassWord = Convert.ToInt32(people.Password);
                            item.Money = people.Money;
                            item.CardNumber = people.CardNumber;
                            item.CapitalMoney = Convert.ToInt32(people.CapitalMoney);
                            item.Credit = people.Credit;
                            item.CreditPrecent = people.CreditPrecent;
                            item.OpenCard = Convert.ToByte(1);
                            entities.Entry(item).State = EntityState.Modified;
                            entities.SaveChanges();
                        }
                    }
               }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message.ToString());
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
                            var SqlTranslate = entities.People.Where(e => e.ID == id).FirstOrDefault();
                           SqlTranslate.Money = people.Money;
                           entities.Entry(SqlTranslate).State = EntityState.Modified;
                           entities.SaveChanges();
                        }
                    }
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
        /// <summary>
        /// Добавлене в базу клиентов
        /// </summary>
        public void SqlBDCreat()
        {
            try
            {
                People people1 = new People("Name_1", "LastName_1", 56678, 119324110, 45000, 0, 0, 0, 1,  Convert.ToByte(1));
                People people2 = new People("Name_2", "LastName_2", 87978, 216554220, 95000, 0, 0, 0, 1, Convert.ToByte(1));
                People people3 = new People("Name_3", "LastName_3", 25879, 229654323, 75000, 0, 0, 0, 1, Convert.ToByte(1));
                People people4 = new People("Name_4", "LastName_4", 57868, 432323360, 31000, 0, 0, 0, 1,  Convert.ToByte(1));
                People people5 = new People("Name_5", "LastName_5", 89055, 679321780, 18000, 0, 0, 0, 1, Convert.ToByte(1));
                People people6 = new People("Name_6", "LastName_6", 25467, 559323389, 38800, 0, 0, 0, 2, Convert.ToByte(1));
                People people7 = new People("Name_7", "LastName_7", 13566, 336544578, 99000, 0, 0, 0, 2, Convert.ToByte(1));
                People people8 = new People("Name_8", "LastName_8", 36790, 329324099, 10000, 0, 0, 0, 2, Convert.ToByte(2));
                People people9 = new People("Name_9", "LastName_9", 58327, 316767730, 4000, 0, 0, 0, 1, Convert.ToByte(2));
                People people10 = new People("Name_10", "LastName_10", 68908, 143527660, 66000, 0, 0, 0, 1, Convert.ToByte(2));
                People people11 = new People("Name_11", "LastName_11", 56734, 743876986, 7800, 0, 0, 0, 3, Convert.ToByte(2));
                People people12 = new People("Name_12", "LastName_12", 89047, 973609934, 50000, 0, 0, 0, 3, Convert.ToByte(2));
                People people13 = new People("Name_13", "LastName_13", 14578, 904234578, 12000, 0, 0, 0, 3, Convert.ToByte(3));
                People people14 = new People("Name_14", "LastName_14", 87478, 358790990, 23000, 0, 0, 0, 3, Convert.ToByte(3));
                People people15 = new People("Name_15", "LastName_15", 46588, 768087409, 46600, 0, 0, 0, 2, Convert.ToByte(3));

                var refreshSql = entities.People;
                var BDPeopleSql = new List<People>() { people1, people2, people3, people4, people5, people6, people7, people8, people9, people10, people11, people12, people13, people14, people15 };
                foreach(var item in BDPeopleSql)
                {
                    entities.People.Add(item);
                }
                entities.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                this.Close();
            }
        }
    }
}
