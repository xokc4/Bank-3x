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
        public static void Capitalization(string MoneyCapit, bool Icap)
        {
            foreach (var item in Account.peoplePost)
            {
                if (MainWindow.Id == item.ID)
                {
                    int MoneyContent = Convert.ToInt32(MoneyCapit);
                    if (Icap  == true)
                    {
                        
                        item.Money = item.Money - MoneyContent;// снятие денег с счета

                        int MoneyHistori = MoneyContent;

                        item.CapitalMoney = Creat.CreatCapitNo(Convert.ToDouble(MoneyCapit));//формула для вклада


                        MSG MSGHis = ((int Money) =>//метод по добавлении истории 
                        {
                            MainWindow.historis.Add(new Histori("CapitNo", Money, MainWindow.Id));
                        });
                        MSGHis(MoneyHistori);
                    }
                    if(Icap == false)
                    {
                        int MoneyHistor = MoneyContent;
                        item.Money = item.Money - MoneyContent;

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
}
