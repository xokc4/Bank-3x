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
    /// Логика взаимодействия для SqlParametrs.xaml
    /// </summary>
    public partial class SqlParametrs : Window
    {
        public SqlParametrs()
        {
            InitializeComponent();
            
        }
        /// <summary>
        /// передача данных для метода по создании файла с параметрами подключения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickSave_Click(object sender, RoutedEventArgs e)
        {
            SqlConn.SqlConn.sqlconFile(DataSourse.Text.ToString(), InitialCatalog.Text.ToString(), Convert.ToBoolean(IntegratedSecurity.Text));
            MessageBox.Show("изменение были совершены, перезапустите приложение");
        }
        public void MessageException()
        {
            MessageBox.Show("Если вышла ошибка то проблема в подключении базы данных(SQL server), для решения этой проблемы. №1 создайте " +
                        "базу данных, №2 напишите данные о базе подключения в форму, если не получилось посмотрите правильность написания.");
        }
    }
}
