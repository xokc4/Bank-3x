using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_3x.FolderPeople
{
   public class VIPpeople : PeoplePost
    {
        public VIPpeople(string name, string lastName, string password, int cardNumber) : base(name, lastName, password, cardNumber)
        {
        }
    }
}
