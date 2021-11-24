using Bank_3x.FolderPeople;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bank_3x
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {/// <summary>
    /// статичный логин == имя аккаунта пользователя
    /// </summary>
        public static string logName;
        /// <summary>
        /// запуск приложения и создание бд(пользователей)
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Account.CreateBD();// создание пользователей
            People.ItemsSource = Account.peoplePost;//передача информации пользователей
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
            bool eas = true;//уловие 
            foreach(var acc in Account.peoplePost)//чтение коллекции с пользователями
            {
                if(login == acc.Name)// проверка на имя аккаунта
                {
                    if(pass.Password == acc.Password)//проверка пароля 
                    {
                        logName = login;//передача имени в статическую переменную 
                        eas = false;
                        NewWindow();// окрытие другого метода 
                    }  
                    else
                    {
                        pass.Clear();// удаление введенного пароля
                        MessageBox.Show("не правильный пароль");//вывод сообщения
                    }
                }
                 if(eas == true)
                 { 
                    
                    MessageBox.Show("нет такого пользователя");//вывод сообщения
                 }
            }
        }
        /// <summary>
        /// открытие нового окна 
        /// </summary>
        public void NewWindow()
        {
            Account account = new Account();
            account.Show();
        }
    }
}
