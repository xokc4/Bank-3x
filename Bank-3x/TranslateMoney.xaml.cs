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
    /// Логика взаимодействия для TranslateMoney.xaml
    /// </summary>
    public partial class TranslateMoney : Window
    {
        public TranslateMoney()
        {
            InitializeComponent();

        }
        /// <summary>
        /// метод для перевода денег
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TranslateButton_Click(object sender, RoutedEventArgs e)
        {
            bool eua = true;//условие дял проверки
            foreach (var item2 in Account.peoplePost)//чтение коллекции
            {
                if (MainWindow.Id == item2.ID)//условие по анахождении пользователя
                {
                    int summa = 0;//процентная ставка для перевода 
                    if(item2.Type == "legal")// условие для процента
                    {
                        summa = 10;//для обычного пользователя 
                    }
                    if(item2.Type == "regular")
                    {
                        summa = 5;//для юриста 
                    }
                    else
                    {
                        summa = 3;//для VIP пользователя 
                    }
                    
                    if(item2.Money< 0)
                    {
                        labelMessege.Content = "на счету слишком мало денег";//сообщение 
                    }
                    if(item2.Money>0 )//условие при наличие денег
                    {
                        item2.Money = item2.Money - (Convert.ToInt32(MoneyTranslate.Text) + (Convert.ToInt32(MoneyTranslate.Text) / 100 * summa));// расчет и снятие денег
                        foreach (var item in Account.peoplePost)//чтение коллекции
                        {
                            if (Convert.ToInt32(CardNumberTranslate.Text) == item.CardNumber)//нахождение аккаунта
                            {
                                item.Money = item.Money + Convert.ToInt32(MoneyTranslate.Text);//перевод денег
                                labelMessege.Content = "перевод был совершон";//сообщение

                            }
                            if (eua == false)
                            {
                                MessageBox.Show("не правильный счет");//сообщение
                            }
                        }
                    }

                }
            }
            
        }
        /// <summary>
        /// кнопка для закрытие окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();//закрытие аккаунта
        }
    }
}
