
using Bank_3x.FolderPeople;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;



namespace Bank_3x
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml

    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer TimeRefresh = new DispatcherTimer();// время рефреш
        static public List<Histori> historis = new List<Histori>();// коллекция истории переведов и тд
         public static Account account = new Account();
        /// <summary>
        /// статичный id 
        /// </summary>
        static public int Id;
        /// <summary>
        /// запуск приложения и создание бд(пользователей)
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Account.CreateBD();// создание пользователей
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
    }
}
