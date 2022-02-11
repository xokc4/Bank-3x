using Bank_3x.FolderPeople;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_3x
{
    public static class WorkEntity
    {
        /// <summary>
        /// Создания пользователей в бд
        /// </summary>
        static public void SqlCreat()
        {
            People people1 = new People("Name_1", "LastName_1", 56678, 119324110, 45000, 0, 0, 0, 1, Convert.ToByte(1));
            People people2 = new People("Name_2", "LastName_2", 87978, 216554220, 95000, 0, 0, 0, 1, Convert.ToByte(1));
            People people3 = new People("Name_3", "LastName_3", 25879, 229654323, 75000, 0, 0, 0, 1, Convert.ToByte(1));
            People people4 = new People("Name_4", "LastName_4", 57868, 432323360, 31000, 0, 0, 0, 1, Convert.ToByte(1));
            People people5 = new People("Name_5", "LastName_5", 89055, 679321780, 18000, 0, 0, 0, 1, Convert.ToByte(1));
            People people6 = new People("Name_6", "LastName_6", 25467, 559323389, 38800, 0, 0, 0, 2, Convert.ToByte(1));
            People people7 = new People("Name_7", "LastName_7", 13566, 336544578, 99000, 0, 0, 0, 2, Convert.ToByte(1));
            People people8 = new People("Name_8", "LastName_8", 36790, 329324099, 10000, 0, 0, 0, 2, Convert.ToByte(2));
            People people9 = new People("Name_9", "LastName_9", 58327, 316767730, 4000, 0, 0, 0, 1, Convert.ToByte(2));
            People people10 = new People("Name_10", "LastName_10", 68908, 143527660, 66000, 0, 0, 0, 1, Convert.ToByte(2));
            People people11 = new People("Name_11", "LastName_11", 56734, 743876986, 7800, 0, 0, 0, 3, Convert.ToByte(2));
            People people12 = new People("Name_12", "LastName_12", 89047, 973609934, 50000, 0, 0, 0, 3, Convert.ToByte(2));
            People people13 = new People("Name_13", "LastName_13", 14578, 904234578, 12000, 0, 0, 0, 3, Convert.ToByte(3));
            People people14 = new People("Name_14", "LastName_14", 87478, 358790990, 23000, 0, 0, 0, 3, Convert.ToByte(3));
            People people15 = new People("Name_15", "LastName_15", 46588, 768087409, 46600, 0, 0, 0, 2, Convert.ToByte(3));

            var refreshSql = MainWindow.entities.People;
            var BDPeopleSql = new List<People>() { people1, people2, people3, people4, people5, people6, people7, people8, people9, people10, people11, people12, people13, people14, people15 };
            foreach (var item in BDPeopleSql)
            {
                MainWindow.entities.People.Add(item);
            }
            MainWindow.entities.SaveChanges();
        }
        /// <summary>
        /// обнoвление данных в sql
        /// </summary>
        static public void UpDateSql()
        {
            if (MainWindow.Id == -1)
            {
            }
            else
            {

                foreach (var people in Account.peoplePost)
                {
                    if (MainWindow.Id == people.ID)
                    {
                        int boll = ConBool(people.OpenCard);
                        var item = MainWindow.entities.People.Where(e => e.ID == MainWindow.Id).FirstOrDefault();
                        item.Name = people.Name;
                        item.LastName = people.LastName;
                        item.PassWord = Convert.ToInt32(people.Password);
                        item.Money = people.Money;
                        item.CardNumber = people.CardNumber;
                        item.CapitalMoney = Convert.ToInt32(people.CapitalMoney);
                        item.Credit = people.Credit;
                        item.CreditPrecent = people.CreditPrecent;
                        item.OpenCard = Convert.ToByte(1);
                        MainWindow.entities.Entry(item).State = EntityState.Modified;
                        MainWindow.entities.SaveChanges();
                    }
                }
            }
        }
        /// <summary>
        /// база данных через entityFramework
        /// </summary>
        public static void SqlEntityDownPeople()
        {
            var refresh = MainWindow.entities.People.Join(MainWindow.entities.TypePeople,
                     peopleBD => peopleBD.Type,
                     typePeopleBD => typePeopleBD.ID,
                     (peopleBD, typePeopleBD) => new
                     {
                         peopleBD.ID,
                         peopleBD.Name,
                         peopleBD.LastName,
                         peopleBD.PassWord,
                         peopleBD.Money,
                         peopleBD.CardNumber,
                         peopleBD.CapitalMoney,
                         peopleBD.Credit,
                         peopleBD.CreditPrecent,
                         typePeopleBD.TyPeople,
                         peopleBD.OpenCard
                     });
            List<PeoplePost> people = new List<PeoplePost>();
            foreach (var item in refresh)
            {
                people.Add(new PeoplePost(item.Name.ToString(), item.LastName.ToString(),

                item.PassWord.ToString(), item.CardNumber,

                Convert.ToInt32(item.Money), Convert.ToInt32(item.CapitalMoney),

                Convert.ToInt32(item.Credit), Convert.ToInt32(item.CreditPrecent),

                item.TyPeople.ToString(), Convert.ToInt32(item.ID), Convert.ToBoolean(item.OpenCard)));
            }
            Account.peoplePost = people;
        }
        /// <summary>
        /// обновление данных с переводом 
        /// </summary>
        /// <param name="idPeople"></param>
        static public void UpDateMoneySql(object idPeople)
        {
            int id = Convert.ToInt32(idPeople);
            foreach (var people in Account.peoplePost)
            {
                if (id == people.ID)
                {
                    int boll = ConBool(people.OpenCard);
                    var SqlTranslate = MainWindow.entities.People.Where(e => e.ID == id).FirstOrDefault();
                    SqlTranslate.Money = people.Money;
                    MainWindow.entities.Entry(SqlTranslate).State = EntityState.Modified;
                    MainWindow.entities.SaveChanges();
                }
            }
        }
        /// <summary>
        /// запись для значения bool
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static int ConBool(bool e)
        {
            if (e == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


    }
}
