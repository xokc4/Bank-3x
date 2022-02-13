using Bank_3x.FolderPeople;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Bank_3x
{
    /// <summary>
    /// Логика взаимодействия для Account.xaml
    /// </summary>
    public partial class Account : Window
    {
        public static List<PeoplePost> peoplePost = new List<PeoplePost>();//коллекция пользователей
        DispatcherTimer TimeRefresh = new DispatcherTimer();//время для обновления 
        DispatcherTimer TimerCredit = new DispatcherTimer();//время для снятия кредита
        DispatcherTimer TimerCapitMoney = new DispatcherTimer();//время для вывода денег со вклада
        public delegate void RefrefhHistori();// делегат для обновления таблицы истории
        public delegate void HistoriMsg(int money);// делегат для метода по добавления истории
        /// <summary>
        /// запуск окна и методов с обновлением и информации о аккаунте
        /// </summary>
        public Account()
        {
            InitializeComponent();
            InformationOutput();
            Refresh();

        }
        /// <summary>
        /// обновление времени
        /// </summary>
        public void Refresh()
        {
            TimeRefresh.Interval = new TimeSpan(0, 0, 5); // в часах, минутах, секундах. обновление всего происходит каждые 5 секунд
            TimeRefresh.Tick += dtClockTime_Tick;//при обновлении времени включается метод по названии
            TimeRefresh.Start();//запуск работы времени

            TimerCredit.Interval = new TimeSpan(15, 0, 0, 0);// в часах, минутах, секундах. обновление  кредита
            TimerCredit.Tick += TimerCredit_Tick;//при обновлении времени включается метод по названии
            TimerCredit.Start();//запуск работы времени

            TimerCapitMoney.Interval = new TimeSpan(20, 0, 0, 0);//в часах, минутах, секундах. обновление  вклада
            TimerCapitMoney.Tick += TimerCapitMoney_Tick;//при обновлении времени включается метод по названии

            TimerCapitMoney.Start();//запуск работы времени

        }
        /// <summary>
        /// таймер при добавлении денег от вклада
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerCapitMoney_Tick(object sender, EventArgs e)
        {
            foreach (var item in peoplePost)
            {
                if (MainWindow.Id == item.ID)// выбор аккаунта по имени
                {
                    if (item.CapitalMoney > 0)//условие если на счету есть деньги
                    {
                        int capit = Convert.ToInt32(item.CapitalMoney);
                        item.Money = item.Money + capit;// перевод суммы
                        item.CapitalMoney = 0;//обнуление вклада
                    }
                }
            }
        }
        /// <summary>
        /// таймер при обновлении информации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtClockTime_Tick(object sender, EventArgs e)
        {
            InformationOutput();
        }
        /// <summary>
        /// таймер при снятии денег со счета 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerCredit_Tick(object sender, EventArgs e)
        {
            foreach (var item in peoplePost)
            {
                if (MainWindow.Id == item.ID)
                {
                    if (item.CreditPrecent > 0)
                    {
                        if (item.Money > item.CreditPrecent || item.Money == item.CreditPrecent)
                        {
                            item.Money = item.Money - item.CreditPrecent;
                            item.Credit = item.Credit - item.CreditPrecent;
                            int CreditPrecent = item.CreditPrecent;
                            HistoriMsg msg = ((int Money) =>//метод по добавлении истории
                            {
                                MainWindow.historis.Add(new Histori("CreditPrecent", Money, MainWindow.Id));
                            });
                            msg(CreditPrecent);
                        }
                        else
                        {
                          Message.Text = "пополните счет";
                        }
                    }
                }
            }
        }
        /// <summary>
        /// кнопка по открытии нового окна для перевода денег 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Translate_Click(object sender, RoutedEventArgs e)
        {
            ShowTranslateMoney();
        }
        /// <summary>
        /// кнопка по открытии нового окна для вклада 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Capitalization_Click(object sender, RoutedEventArgs e)
        {
            ShowCapitalisation();
        }
        /// <summary>
        /// кнопка по открытии нового окна для кредита
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Сredit_Click(object sender, RoutedEventArgs e)
        {
            ShowCredit();
        }
        /// <summary>
        /// метод по открытии нового окна для перевода денег 
        /// </summary>
        public void ShowTranslateMoney()
        {
            TranslateMoney translate = new TranslateMoney();
            translate.Show();
        }
        /// <summary>
        ///  метод по открытии нового окна для вклада 
        /// </summary>
        public void ShowCapitalisation()
        {
            Capitalisation capitalisation = new Capitalisation();
            capitalisation.Show();
        }
        /// <summary>
        /// метод по открытии нового окна для кредита
        /// </summary>
        public void ShowCredit()
        {
            Credit credit = new Credit();
            credit.Show();
        }
        /// <summary>
        /// метод по информации о аккаунте 
        /// </summary>
        public void InformationOutput()
        {

            foreach (var item in peoplePost)
            {
                if (MainWindow.Id == item.ID)
                {

                    Money.Content = item.Money + "  р.";
                    CardNumberLabel.Content = "Номер счета:   " + item.CardNumber;
                    if (item.CapitalMoney == 0)
                    {
                        CapitalizationNumber.Content = "";
                    }
                    else
                    {
                        CapitalizationNumber.Content = "через год +" + item.CapitalMoney + " р";
                    }
                    if (item.Credit == 0)
                    {
                        СreditNumber.Content = "";
                        CreditPresent.Content = "";
                    }
                    else
                    {
                        СreditNumber.Content = "-" + item.Credit + " р";
                        CreditPresent.Content = "ежемесечный платеж равен: " + item.CreditPrecent + " р";
                    }
                    RefrefhHistori refrefh = RefreshHistoriMethod;
                    refrefh();
                    if (Histori.numberAttempts == 10)
                    {
                        item.OpenCard = false;
                        Histori.numberAttempts = 0;
                    }
                    else
                    {

                    }
                }
            }
            Thread ThreadSave = new Thread(MainWindow.UpDate);
            ThreadSave.Start();
        }
        /// <summary>
        /// создание коллекции пользователей
        /// </summary>
        static public void CreateBD()
        {
            peoplePost = PeoplePost.BDCreat();
        }

        private void ToSend_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.account.Close();
        }
        private void RefreshHistoriMethod()
        {
            MSGSupport.ItemsSource = null;
            MSGSupport.ItemsSource = HistoriMainMethod();
        }
        public List<Histori> HistoriMainMethod()// метод по передаче своей истории в таблицу 
        {
            List<Histori> HistoriMain = new List<Histori>();// твоя таблицы историй переводов
           foreach(var item in MainWindow.historis)
           {
                if(MainWindow.Id == item.IdAccount)// добавление в таблицу твоей истории
                {
                    HistoriMain.Add(item);
                }
           }
            return HistoriMain;
        }

        private void changeContact_Click(object sender, RoutedEventArgs e)// открытие окна для изменений информации о тебе
        {
         CangneContact change = new CangneContact();
            change.Show();
        }
        /// <summary>
        /// кнопка для обнуления попыток перевода и выход с аккаунта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void ExitAccount_Click(object sender, RoutedEventArgs e)
        {
            Histori.numberAttempts = 0;
            MainWindow.account.Close();
        }
    }
}
