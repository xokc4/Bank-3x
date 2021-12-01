using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_3x.FolderPeople
{
    public class legalPeople : PeoplePost
    {
        public legalPeople(string name, string lastName, string password, int cardNumber, int money, int capitalMoney, int credit, int creditPrecent, string type, int iD, bool openCard) : base(name, lastName, password, cardNumber, money, capitalMoney, credit, creditPrecent, type, iD, openCard)
        {
        }
    }
}
