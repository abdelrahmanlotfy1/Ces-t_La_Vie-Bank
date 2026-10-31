using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_est_la_vie_Bank
{
    internal class Transaction
    {
        private object _Locked = new object();
        public Account account1;
        public Account account2;
        private readonly decimal amount;
        public Transaction(Account account1, Account account2, decimal amount)
        {

            this.account1 = account1;
            this.account2 = account2;
            this.amount = amount;
        }
        public Transaction(Account acc1)
        {
            this.account1 = acc1;
        }

        public bool Transfare()
        {
            
            if (account2 == null || amount <= 0 || account1.ID == account2.ID) return false;
            bool Accountfirst1 = string.CompareOrdinal(account1.ID, account2.ID) > 0;
            Account first = Accountfirst1 ? account1 : account2;
            Account second = Accountfirst1 ? account2 : account1;
            lock (first.SyncRoot)
            {
                lock (second.SyncRoot)
                {
                    if (!account1.Withdraw(amount))
                    {
                        
                        return false;
                    }
                    account2.Deposite(amount);
                    return true;

                   }
                }
            }
        }
    }

    

