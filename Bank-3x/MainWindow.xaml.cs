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
    {
        public static List<legalPeople> leagalPeoples;
        public MainWindow()
        {
            InitializeComponent();
            leagalPeoples = new List<legalPeople>();
            CreateBD();
            People.ItemsSource = leagalPeoples;
        }

        private void HefNow_Click(object sender, RoutedEventArgs e)
        {
            СheckAccount();
        }

        public void CreateBD()//
        {
            Random random = new Random();
            for(int q = 0; q<random.Next(20, 25); q++)
            {
                leagalPeoples.Add(new legalPeople($"Name_{q}", $"last_name{q}", GeneratorPassword(), GeneratorCardNumber()));
            }
        }

        public string  GeneratorPassword()// перекинуть в PeoplePost
        {
            string password = "";
            Random random = new Random();
            for(int i=0; i<5; i++)
            {
               int Qun =  random.Next(1, 9);
                password = password + Convert.ToString(Qun);
            }
            return password;
        }
        public int GeneratorCardNumber()// перекинуть в PeoplePost
        {
            Random random = new Random();
            int CardNumber = random.Next(100000000, 999999999);
            return CardNumber;
        }
        public void СheckAccount()
        {
            string login = Convert.ToString(log.Text);
            
            foreach(var acc in leagalPeoples)
            {
                if(login == acc.Name)
                {
                    if(pass.Password == acc.Password)
                    {
                        NewWindow();
                    }    
                }
            }
        }
        public void NewWindow()
        {
            Account account = new Account();
            account.Show();
        }
    }
}
