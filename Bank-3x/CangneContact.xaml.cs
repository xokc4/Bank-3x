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
    /// Логика взаимодействия для CangneContact.xaml
    /// </summary>
    public partial class CangneContact : Window
    {

        public CangneContact()
        {
            InitializeComponent();
            People();
        }
        /// <summary>
        /// подставление данных о тебе
        /// </summary>
        public void People()
        {
            foreach(var item in Account.peoplePost)// чтение коллекции
            {
                if(item.ID == MainWindow.Id)// выбор своего аккаунта 
                {
                    ChangeName.Text = item.Name;
                    ChangeLastName.Text = item.LastName;
                    ChangePassword.Text = item.Password;
                }
            }
        }
        /// <summary>
        /// кнопка для сохранения данных в Account.peoplePost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in Account.peoplePost)// чтение коллекции
            {
                if (item.ID == MainWindow.Id)// выбор своего аккаунта 
                {
                    item.Name = ChangeName.Text;// передача данных
                    item.LastName = ChangeLastName.Text;//передача данных
                    try { 
                    
                        item.Password = ChangePassword.Text;//передача данных
                    }
                    catch(FormatException) { MessageBox.Show("пароль состаит только из чисел"); }
                }
            }
            
        }
    }
}
