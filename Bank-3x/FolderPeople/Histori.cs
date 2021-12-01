using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_3x.FolderPeople
{
    public class Histori
    {
       static public int numberAttempts = 0;//количество переводов
        public DateTime Date { get; }//дата действия
        public string Type { get; set; }//тип действия
        public int Amount { get; set; }//сумма
        public string MSG { get; set; }//сообщение
        public int IdAccount { get; set;}//айди аккаунта
        /// <summary>
        /// конструктор по создании истории переводов 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        /// <param name="id"></param>
        public Histori(string type, int amount, int id)
        {
            this.IdAccount = id;
            this.Date = DateTime.Now;
            this.Type = type;
            this.Amount = amount;
            
            if (type == "Translat")//перевод на другой акк
            {
                numberAttempts++;
                this.MSG = $"произошёл перевод, на сумму: {Amount}";
            }
            if(type == "CapitNo")//вклад без капп
            {
                this.MSG = $"был совершен вклад без капитализации, на сумму: {Amount}";
            }
            if (type == "CapitYes")//вклад без капп
            {
                this.MSG = $"был совершен вклад c капитализациeй, на сумму: {Amount}";
            }
            if (type == "Credit")//кредит
            {
                this.MSG = $"был получен кредит, на сумму: {Amount}";
            }
            if(type == "CreditPrecent")//кредитная ставка
            {
                this.MSG = $"списание ставки за кредит, на сумму: {Amount}";
            }  
        }
    }
}
