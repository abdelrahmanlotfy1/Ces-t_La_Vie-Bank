using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_est_la_vie_Bank
{
    public class InvalidAmountException: Exception
    {
        public string message;
        private decimal amount;

        public InvalidAmountException(string message , decimal amount ) 
        {
            this.message = message;
            this.amount = amount;
        }

    }
}
