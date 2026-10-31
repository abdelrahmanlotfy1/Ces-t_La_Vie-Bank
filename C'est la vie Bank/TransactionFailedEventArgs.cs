using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_est_la_vie_Bank
{

    
    public class TransactionFailedEventArgs : EventArgs
    {
        TransactionType t1;
        
        public decimal Amount { get; }
        public string Reason { get; }

        public TransactionFailedEventArgs(TransactionType t2, decimal amount, string reason)
        {
            this.t1 = t2;
            Amount = amount;
            Reason = reason;
        }
    }
}
