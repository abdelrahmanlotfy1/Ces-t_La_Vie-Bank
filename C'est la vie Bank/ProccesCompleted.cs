using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_est_la_vie_Bank
{
    public class ProccesCompletedEventArgs : EventArgs
    {
        public Account account1;
        public Account account2;
        private readonly decimal amount;
        public string message;
        public ProccesCompletedEventArgs(Account account1, Account account2, decimal amount , string message)
        {
            this.account1 = account1;
            this.account2 = account2;
            this.amount = amount;
            this.message = message;
        }
        public ProccesCompletedEventArgs(Account acc1 , string message)
        {
            this.account1 = acc1;
            this.message=message;
            
        }
        
    }
}
