using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Bank_3x.Account;

namespace Bank_3x
{
   static public class TranslateClass
    {
        static public void Translate(string MoneyTranslate, string CardNumberTranslate, int summa)
        {
            foreach (var item2 in Account.peoplePost)//чтение коллекции
            {
                if (MainWindow.Id == item2.ID)//условие по уменьшении денег из своего счета
                {

                    int Histori321 = (Convert.ToInt32(MoneyTranslate) + (Convert.ToInt32(MoneyTranslate) / 100 * summa));
                    item2.Money = item2.Money - (Convert.ToInt32(MoneyTranslate) + (Convert.ToInt32(MoneyTranslate) / 100 * summa));// расчет и снятие денег
                    foreach (var item in Account.peoplePost)//чтение коллекции
                    {
                        if (Convert.ToInt32(CardNumberTranslate) == item.CardNumber)//нахождение аккаунта
                        {

                            item.Money = item.Money + Convert.ToInt32(MoneyTranslate);//перевод денег
                            HistoriMsg histori = ((int Money) =>//метод по добавлении истории в коллекцию
                            {
                                MainWindow.historis.Add(new FolderPeople.Histori("Translat", Money, MainWindow.Id));
                            });

                            Thread ThreadTranslate = new Thread(new ParameterizedThreadStart(MainWindow.UpDateTranslate));
                            ThreadTranslate.Start(item.ID);

                            histori(Histori321);
                            HistoriMsg historiPeopeleTranslate = ((int Money) =>//метод по добавлении истории в коллекцию другому пользователю
                            {
                                MainWindow.historis.Add(new FolderPeople.Histori("Translat", Money, item.ID));
                            });
                            historiPeopeleTranslate(Histori321);
                        }
                    }
                }
            }   
        }
        public static string percent(string MoneyTranslate, int summa)
        {
            return "комиссия  " + Convert.ToInt32(MoneyTranslate) / 100 * summa;
        }
    }
}
