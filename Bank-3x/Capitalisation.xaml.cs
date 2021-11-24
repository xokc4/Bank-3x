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
using System.Windows.Shapes;

namespace Bank_3x
{
    /// <summary>
    /// Логика взаимодействия для Capitalisation.xaml
    /// </summary>
    public partial class Capitalisation : Window, Icapitalization
    {
       
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
            if(MainWindow.logName == item.Name)
            {
                    if(Convert.ToInt32(MoneyCapit.Text)<=item.Money)// условие для вклада 
                    {
                        if(Convert.ToInt32(MoneyCapit.Text) > 1000 || Convert.ToInt32(MoneyCapit.Text) == 1000)// условие для размера вклада
                        {
                            item.Money = item.Money - Convert.ToInt32(MoneyCapit.Text);// снятие денег с счета
                            
                            MoneyEarsCapit.Content = (Convert.ToDouble(MoneyCapit.Text) * 0.12) + Convert.ToDouble(MoneyCapit.Text);
                            item.CapitalMoney = (Convert.ToDouble(MoneyCapit.Text) * 0.12) + Convert.ToDouble(MoneyCapit.Text);//формула для вклада
                            InfMoneyYears.Content = "будет через 12 месяцев";//
                        }
                        else
                        {
                            MessageBox.Content = "Вклады только с 1000 р";
                        }
                    }
                    else
                    {
                        MessageBox.Content = "не хватает денег";
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
                if (MainWindow.logName == item.Name)
                {
                    if (Convert.ToInt32(MoneyCapit.Text) <= item.Money)
                    {
                        if (Convert.ToInt32(MoneyCapit.Text) > 1000 || Convert.ToInt32(MoneyCapit.Text) == 1000)
                        {
                            item.Money = item.Money - Convert.ToInt32(MoneyCapit.Text);
                            double MoneyBeginning = Convert.ToDouble(MoneyCapit.Text);
                          
                          
                            for (int i = 0; i<12; i++)// цикл для формулы
                            {
                                double persentMonth = MoneyBeginning * 12 / 100;
                                double persentMiniMoth = persentMonth * 12 / 100;
                                MoneyBeginning = persentMiniMoth + MoneyBeginning;// формула
                                
                            }
                            item.CapitalMoney = Convert.ToInt32(MoneyBeginning);
                            MoneyEarsCapit.Content = Convert.ToInt32(MoneyBeginning);
                            InfMoneyYears.Content = "будет через 12 месяцев";
                        }
                        else
                        {
                            MessageBox.Content = "Вклады только с 1000р";
                        }
                    }
                    else
                    {
                        MessageBox.Content = "не хватает денег";
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
