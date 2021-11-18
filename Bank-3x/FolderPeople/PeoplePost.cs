using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_3x.FolderPeople
{
   public class PeoplePost : Icapitalization
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int CardNumber { get; set; }

        public PeoplePost(string name, string lastName, string password, int cardNumber)
        {
            this.Name = name;
            this.LastName = lastName;
            this.Password = password;
            this.CardNumber = cardNumber;
        }
        public PeoplePost()
        {

        }

        public void capitalizationYES()
        {
            throw new NotImplementedException();
        }

        public void capitalizationNO()
        {
            throw new NotImplementedException();
        }
    }
}
