using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Services
{

    public class AccountNumberGenerator
    {
        private static int accountNumber = 1;

        public static int GetNextAccountNumber()
        {
            return accountNumber++;
        }
    }

}
