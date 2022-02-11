using Bank_3x.FolderPeople;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CreatSumm;

namespace Bank_3x
{
    /// <summary>
    /// Логика взаимодействия для Credit.xaml
    /// </summary>
    public partial class Credit : Window
    {
        /// <summary>
        /// делегат по добавлении истории
        /// </summary>
        /// <param name="money"></param>
        delegate void MSG(int money);
        public Credit()
        {
            InitializeComponent();
        }
        /// <summary>
        /// метод для создания кредита
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyLoan_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in Account.peoplePost)
            {
                if (MainWindow.Id == item.ID)
                {
                    if (item.OpenCard == false)
                    {
                        MessageBox.Show("Ваш аккаунт заблокирован, из-за большого количества переводов денег на другой счет");
                    }
                    else
                    {
                        if (item.Credit == 0)// условие для принятия кредита
                        {
                            try
                            {


                                if (Convert.ToInt32(MoneyCredit.Text) > 100000 || Convert.ToInt32(MoneyCredit.Text) == 100000)// условие минимального кредита
                                {
                                    double MoneySumm = Convert.ToDouble(MoneyCredit.Text);
                                    double Yuers = Convert.ToDouble(MothEyars.Text);
                                    double bet = 2;
                                    if (item.Type == "legal")
                                    {
                                        if (Yuers <= 3)
                                        {
                                            bet = 4;
                                        }
                                        if (Yuers > 3 && Yuers < 8)
                                        {
                                            bet = 6;
                                        }
                                        if (Yuers > 10)
                                        {
                                            bet = 8;
                                        }
                                    }//условие процентной ставки
                                    PresentBet.Content = bet + "%";
                                    CreatCredit.Credit(MoneySumm, Yuers, bet);
                                    RepaymentMoth.Content = Creat.CreatCredit(MoneySumm, Yuers, bet);                                   
                                }
                                else
                                {
                                    InfCredit.Content = "кредит можно взять только с 100000 р ";
                                }
                            }
                            catch (FormatException)
                            {
                                MoneyCredit.Clear();
                                MothEyars.Clear();
                                MessageBox.Show("нужно вписать только числа");
                            }

                        }
                        else
                        {
                            InfCredit.Content = " у вас уже есть кредит";
                        }
                    }  
                }
            }
        }
    }
}
