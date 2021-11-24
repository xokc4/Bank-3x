using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_3x.FolderPeople
{
    interface ICreatInfPeople //интерфейс для создания пароля, счета и денег
    {
        string PasswordCreat();//создание пароля
        int NumberCreat();//создание счета
        int MoneyCreat();//создания денег на счету
    }
}
