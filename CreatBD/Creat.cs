using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatSumm
{
    /// <summary>
    /// Создание суммы для капитализации и кредита
    /// </summary>
    static public class Creat
    {
        /// <summary>
        /// число без капитализации
        /// </summary>
        /// <param name="MoneyCapit"></param>
        /// <returns></returns>
       static public int CreatCapitNo( double MoneyCapit)
       {

            return Convert.ToInt32((MoneyCapit * 0.12) + MoneyCapit);
       }
        /// <summary>
        /// число с капитализации
        /// </summary>
        /// <param name="MoneyBeginning"></param>
        /// <returns></returns>
        static public int CreatCapitYES(double MoneyBeginning)
        {
            for (int i = 0; i < 12; i++)// цикл для формулы
            {
                double persentMonth = MoneyBeginning * 12 / 100;
                double persentMiniMoth = persentMonth * 12 / 100;
                MoneyBeginning = persentMiniMoth + MoneyBeginning;// формула

            }
            return Convert.ToInt32(MoneyBeginning);
        }
        /// <summary>
        /// число для кредита
        /// </summary>
        /// <param name="MoneySumm"></param>
        /// <param name="Yuers"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        static public int CreatCredit(double MoneySumm, double Yuers, double bet)
        {

            double payment = (MoneySumm + MoneySumm * bet * Yuers / 100) / (Yuers * 12);//формула
            
            return Convert.ToInt32(Math.Round(payment, 2));
        }
    }
}
