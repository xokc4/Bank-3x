using Bank_3x.FolderPeople;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_3x
{
    public class WorkEntity
    {
        static private string NewUSer = @"NewUsers.txt";
        
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
                        int password = Convert.ToInt32(people.Password);
                        int Capit = Convert.ToInt32(people.CapitalMoney);
                        int boll = ConBool(people.OpenCard);

                        var item = MainWindow.entities.People.Where(e => e.ID == MainWindow.Id).FirstOrDefault();

                        item.Name = people.Name;
                        item.LastName = people.LastName;
                        item.PassWord = password;
                        item.Money = people.Money;
                        item.CardNumber = people.CardNumber;
                        item.CapitalMoney = Capit;
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
        static public void SqlEntityDownPeople()
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
        static public int ConBool(bool e)
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
        /// <summary>
        /// Создания пользователей в бд
        /// </summary>
        static public void SqlCreat()
        {
            string text;
            FileStream stream = new FileStream(NewUSer, FileMode.OpenOrCreate);
            StreamReader reader = new StreamReader(stream);
            text = reader.ReadToEnd();
            string[] vs = text.Split(new char[] { ';' });

             List<People> people = new List<People>();
            foreach(string i in vs)
            {
                string textUser = i;
                string[] vs2 = textUser.Split(new char[] { ',' });
                people.Add(new People(vs2[0],vs2[1],Convert.ToInt32(vs2[2]),Convert.ToInt32(vs2[3]),Convert.ToInt32(vs2[4]),
                Convert.ToInt32(vs2[5]), Convert.ToInt32(vs2[6]), Convert.ToInt32(vs2[7]), Convert.ToInt32(vs2[8]),Convert.ToByte(vs2[9])));
            }
            foreach(var item in people)
            {
                MainWindow.entities.People.Add(item);
            }
            MainWindow.entities.SaveChanges();
        }
        
    }
}
