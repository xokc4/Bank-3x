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
    /// Логика взаимодействия для Capitalisation.xaml
    /// </summary>
    public partial class Capitalisation : Window, Icapitalization
    {
        /// <summary>
        /// делегат по добавлении истории
        /// </summary>
        /// <param name="money"></param>
        public delegate void MSG(int money);
       
        public Capitalisation()
        {
            InitializeComponent();

         

        }
        /// <summary>
        /// условие для выбора метода
        /// </summary>
        public void choice()
        {
            if(Combobox.SelectedIndex == 0)
            {
                capitalizationYES();
            }
            if (Combobox.SelectedIndex == 1)
            {
                capitalizationNO();
            }      
        }
        /// <summary>
        /// метод (вклад без капитализации)
        /// </summary>
        public void capitalizationNO()
        {
           
          foreach (var item in Account.peoplePost)
          {
            if(MainWindow.Id == item.ID)
            {
                    if (item.OpenCard == false)
                    {
                        MessageBox.Show("Ваш аккаунт заблокирован, из-за большого количества переводов денег на другой счет");
                    }
                    else
                    {
                        try 
                        {
                            if (Convert.ToInt32(MoneyCapit.Text) <= item.Money)// условие для вклада 
                            {
                                if (Convert.ToInt32(MoneyCapit.Text) > 1000 || Convert.ToInt32(MoneyCapit.Text) == 1000)// условие для размера вклада
                                {
                                    item.Money = item.Money - Convert.ToInt32(MoneyCapit.Text);// снятие денег с счета

                                    int MoneyHistori = Convert.ToInt32(MoneyCapit.Text);

                                    MoneyEarsCapit.Content = Creat.CreatCapitNo(Convert.ToDouble(MoneyCapit.Text));

                                    item.CapitalMoney = Creat.CreatCapitNo(Convert.ToDouble(MoneyCapit.Text));//формула для вклада

                                    InfMoneyYears.Content = "будет через 12 месяцев";//
                                    MSG MSGHis = ((int Money) =>//метод по добавлении истории 
                                    {
                                        MainWindow.historis.Add(new Histori("CapitNo", Money, MainWindow.Id));
                                    });
                                    MSGHis(MoneyHistori);
                                }
                                else
                                {
                                    MessageBoxWPF.Content = "Вклады только с 1000 р";
                                }
                            }
                            else
                            {
                                MessageBoxWPF.Content = "не хватает денег";
                            }
                        
                            
                        }
                        catch(FormatException)
                        {
                            MoneyCapit.Clear();
                            MessageBoxWPF.Content = "нужно вписывать числа";
                        }
                    }
            }  
          }
        }
        /// <summary>
        /// метод (вклад с капитализацией)
        /// </summary>
        public void capitalizationYES()
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
                        try
                        {


                            if (Convert.ToInt32(MoneyCapit.Text) <= item.Money)
                            {
                                if (Convert.ToInt32(MoneyCapit.Text) > 1000 || Convert.ToInt32(MoneyCapit.Text) == 1000)
                                {
                                    int MoneyHistor = Convert.ToInt32(MoneyCapit.Text);
                                    item.Money = item.Money - Convert.ToInt32(MoneyCapit.Text);
                                    
                                    item.CapitalMoney = Creat.CreatCapitYES(Convert.ToDouble(MoneyCapit.Text));

                                    MoneyEarsCapit.Content = Creat.CreatCapitYES(Convert.ToDouble(MoneyCapit.Text));

                                    InfMoneyYears.Content = "будет через 12 месяцев";
                                    MSG MSGHis = ((int Money) =>//метод по добавлении истории 
                                    {
                                        MainWindow.historis.Add(new Histori("CapitYes", Money, MainWindow.Id));
                                    });
                                    MSGHis(MoneyHistor);
                                }
                                else
                                {
                                    MessageBoxWPF.Content = "Вклады только с 1000р";
                                }
                            }

                            else
                            {
                                MessageBoxWPF.Content = "не хватает денег";
                            }
                        }
                        catch (FormatException)
                        {
                            MoneyCapit.Clear();
                            MessageBoxWPF.Content = "нужно вписывать числа";
                        }
                    }
                }

            }
        }
        /// <summary>
        /// при нажатии на кнопку реализуеться метод с выбором вклада
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCapit_Click(object sender, RoutedEventArgs e)
        {
            choice();
        }
    }
}
