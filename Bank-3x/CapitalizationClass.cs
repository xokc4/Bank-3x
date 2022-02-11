using Bank_3x.FolderPeople;
using CreatSumm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bank_3x.Capitalisation;

namespace Bank_3x
{
    public class CapitalizationClass
    {
        /// <summary>
        /// метод (вклад без капитализации)
        /// </summary>
        /// <param name="MoneyCapit"></param>
        public static void capitalizationNO(string MoneyCapit)
        {
            foreach (var item in Account.peoplePost)
            {
                if (MainWindow.Id == item.ID)
                {
                    item.Money = item.Money - Convert.ToInt32(MoneyCapit);// снятие денег с счета

                    int MoneyHistori = Convert.ToInt32(MoneyCapit);

                    item.CapitalMoney = Creat.CreatCapitNo(Convert.ToDouble(MoneyCapit));//формула для вклада


                    MSG MSGHis = ((int Money) =>//метод по добавлении истории 
                    {
                        MainWindow.historis.Add(new Histori("CapitNo", Money, MainWindow.Id));
                    });
                    MSGHis(MoneyHistori);
                }
            }
        }
        /// <summary>
        /// метод (вклад с капитализацией)
        /// </summary>
        /// <param name="MoneyCapit"></param>
        public static void capitalizationYES(string MoneyCapit)
        {
            foreach (var item in Account.peoplePost)
            {
                if (MainWindow.Id == item.ID)
                {
                    int MoneyHistor = Convert.ToInt32(MoneyCapit);
                    item.Money = item.Money - Convert.ToInt32(MoneyCapit);

                    item.CapitalMoney = Creat.CreatCapitYES(Convert.ToDouble(MoneyCapit));

                    MSG MSGHis = ((int Money) =>//метод по добавлении истории 
                    {
                        MainWindow.historis.Add(new Histori("CapitYes", Money, MainWindow.Id));
                    });
                    MSGHis(MoneyHistor);
                }
            }
        }
    }
}
