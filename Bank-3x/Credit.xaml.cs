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

namespace Bank_3x
{
    /// <summary>
    /// Логика взаимодействия для Credit.xaml
    /// </summary>
    public partial class Credit : Window
    {
        
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

                    if (item.Credit == 0)// условие для принятия кредита
                    {
                        if (Regex.IsMatch(MoneyCredit.Text, "[0-9]") && Regex.IsMatch(MothEyars.Text, "[0-9]"))
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
                                double payment = (MoneySumm + MoneySumm * bet * Yuers / 100) / (Yuers * 12);//формула
                                double otvet = Math.Round(payment, 2);
                                item.CreditPrecent = Convert.ToInt32(otvet);
                                RepaymentMoth.Content = otvet;
                                item.Credit = Convert.ToInt32(MoneySumm);
                                item.CreditPrecent = Convert.ToInt32(otvet);
                                item.Money = item.Money + Convert.ToInt32(MoneySumm);
                            }
                            else
                            {
                                InfCredit.Content = "кредит можно взять только с 100000 р ";
                            }
                        }
                        else
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
