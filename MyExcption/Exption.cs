using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExcption
{
    public class Exption: Exception
    {
        public Exption(string message) : base(message)
        {

        }

    }
}
