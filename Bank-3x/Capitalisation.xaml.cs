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
        /// метод (вклад без капитализации)
        /// </summary>
        public void CapitalizationMethod()
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
                            int MoneyCapitInt = Convert.ToInt32(MoneyCapit.Text);
                            bool ComboBox = Convert.ToBoolean(Combobox.SelectedIndex);
                            if (MoneyCapitInt <= item.Money)// условие для вклада 
                            {
                                if (MoneyCapitInt > 1000 || MoneyCapitInt == 1000)// условие для размера вклада
                                {
                                   CapitalizationClass.Capitalization(MoneyCapit.Text, ComboBox);

                                    if(Combobox.SelectedIndex == 1)
                                    {
                                        MoneyEarsCapit.Content = Creat.CreatCapitNo(Convert.ToDouble(MoneyCapit.Text));
                                    }
                                    if(Combobox.SelectedIndex == 0)
                                    {
                                        Creat.CreatCapitYES(Convert.ToDouble(MoneyCapit));
                                    }  
                                    InfMoneyYears.Content = "будет через 12 месяцев";
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
                            MessageBoxWPF.Content = "нужно вписывать числа или выбрать капитализацию";
                        }
                    }
            }  
          }
        }
        /// при нажатии на кнопку реализуеться метод с выбором вклада
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCapit_Click(object sender, RoutedEventArgs e)
        {
            CapitalizationMethod();
        }
    }
}
