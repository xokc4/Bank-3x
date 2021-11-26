using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_3x.FolderPeople
{
 
   public class PeoplePost 
    {
        Random random = new Random();//рандомайз
        public string Name { get; set; }//имя
        public string LastName { get; set; }//фамилия
        public string Password { get; set; }//пароль
        public int Money { get; set; }//деньги
        public int CardNumber { get; set; }//счет
        public double CapitalMoney { get; set; }//вклад
        public int Credit { get; set; }//кредит
        public int CreditPrecent { get; set; }//ставка кредита
        public string Type { get; set; }//тип пользователя
        public int ID { get; set; }
        /// <summary>
        /// конструктор по созданию клиента
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="password"></param>
        /// <param name="cardNumber"></param>
        /// <param name="money"></param>
        /// <param name="capitalMoney"></param>
        /// <param name="credit"></param>
        /// <param name="creditPrecent"></param>
        /// <param name="type"></param>
        public PeoplePost(string name, string lastName, string password, int cardNumber, int money, int capitalMoney, int credit, int creditPrecent, string type, int iD)
        {
            this.Name = name;
            this.LastName = lastName;
            this.Password = password;
            this.CardNumber = cardNumber;
            this.Money = money;
            this.CapitalMoney = capitalMoney;
            this.Credit = credit;
            this.CreditPrecent = creditPrecent;
            this.Type = type;
            this.ID = iD;

        }
        public PeoplePost()
        {

        }
       
       
      
        /// <summary>
        /// создания коллекции с пользователсями
        /// </summary>
        /// <returns></returns>
       static public List<PeoplePost> BDCreat()
        {
           List<PeoplePost> peoplePost = new List<PeoplePost>();
            Random random = new Random();
            int vsego = 20;
            int legal = random.Next(10, 12);
            int Regular = random.Next(5, 7);
            int Vip = vsego - legal - Regular;
            int ID = 1;
            for (int q = 0; q < legal; q++)
            {
                string password = Convert.ToString(random.Next(10000, 99999));
                int CardNumber = random.Next(100000000, 999999999);
                int money = random.Next(60000, 140000);
                peoplePost.Add(new legalPeople($"Name_{ID}", $"last_name{ID}", password, CardNumber, money, 0, 0, 0, "legal", ID));
                ID++;
            }
            for (int w = 0; w < Regular; w++)
            {
                string password = Convert.ToString(random.Next(10000, 99999));
                int CardNumber = random.Next(100000000, 999999999);
                int money = random.Next(60000, 140000);
                peoplePost.Add(new RegularPeople($"Name_{ID}", $"last_name{ID}", password, CardNumber, money, 0, 0, 0, "regular", ID));
                ID++;
            }
            for (int e = 0; e < Vip; e++)
            {
                string password = Convert.ToString(random.Next(10000, 99999));
                int CardNumber = random.Next(100000000, 999999999);
                int money = random.Next(60000, 140000);
                peoplePost.Add(new VIPpeople($"Name_{ID}", $"last_name{ID}", password, CardNumber, money, 0, 0, 0, "VIP", ID));
                ID++;
            }
            return peoplePost;
        }

   }
}
