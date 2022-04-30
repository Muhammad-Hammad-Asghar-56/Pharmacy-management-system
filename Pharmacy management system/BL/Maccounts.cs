using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_management_system.BL
{
    class Maccounts
    {
        public string userName;
        public string userPassword;
        public string userRole;
        public void displayAccounts()
        {
            Console.WriteLine("{0}   {1}", userName, userPassword);
        }
        public Maccounts()
        {
            userName=" ";
            userPassword=" ";
            userRole=" ";
        }
        public Maccounts(string userName, string userPassword, string userRole)
        {
            this.userName =userName;
            this.userPassword =userPassword;
            this.userRole = userRole;
        }
    }
}
