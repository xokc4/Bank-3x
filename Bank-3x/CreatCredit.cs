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
    public class CreatCredit
    {
        static public void Credit(double MoneySumm, double Yuers, double bet)
        {
            foreach (var item in Account.peoplePost)
            {
                if (MainWindow.Id == item.ID)
                {
                    int Summ = Convert.ToInt32(MoneySumm);
                    item.CreditPrecent = Creat.CreatCredit(MoneySumm, Yuers, bet);
                    item.Credit = Summ;
                    item.CreditPrecent = Creat.CreatCredit(MoneySumm, Yuers, bet);
                    item.Money = item.Money + Summ;
                    int MoneyCreditMSg = Summ;
                    MSG mSG = ((int MoneyCr) => // метод по добавлении истории
                    {
                        MainWindow.historis.Add(new Histori("Credit", MoneyCr, MainWindow.Id));
                    });
                    mSG(MoneyCreditMSg);
                }
            }
        }
    }
}
