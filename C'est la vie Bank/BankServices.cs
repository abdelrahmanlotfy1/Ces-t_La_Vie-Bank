using C_est_la_vie_Bank;
using System;
using System.Collections.Generic;
using System.Linq;

internal class BankServices
{
    public List<Account> Accounts = new List<Account>();
    public List<Customer> Customers = new List<Customer>();
    private readonly List<TransictionRecord> _history = new List<TransictionRecord>();
    private object _history_lock = new object();

    public BankServices() { }

    public void CheckAccount(Account account)
    {
        bool accexist = Accounts.Any(c => c.ID == account.ID);
        if (accexist)
        {
            Console.WriteLine(" This Account Is Exist ");
        }
        else
        {
            throw new NotFoundAccountException($" There is No Found Account with this ID : {account.ID}", account.ID);
        }
    }

    private void log(TransictionRecord record)
    {
        lock (_history_lock)
        {
            _history.Add(record);
        }
    }

    public bool Deposite(Account ac1, decimal Amount)
    {
        bool ok = ac1.Deposite(Amount);
        log(new TransictionRecord(TransactionType.Deposit, ac1.ID, null, Amount, ok));
        return ok;
    }

    public bool Withdraw(Account ac1, decimal Amount)
    {
        bool ok = ac1.Withdraw(Amount);
        log(new TransictionRecord(TransactionType.Withdraw, ac1.ID, null, Amount, ok));
        return ok;
    }

    public bool Transfer(Account acc1, Account acc2, decimal amount)
    {
        bool ok = new Transaction(acc1, acc2, amount).Transfare();
        log(new TransictionRecord(TransactionType.Transfer, acc1.ID, acc2.ID, amount, ok));
        return ok;
    }

    public List<TransictionRecord> GetStatement(string accountId)
    {
        lock (_history_lock)
        {
            return _history
                .Where(t => t.FromAccountID == accountId || t.ToAccountID == accountId)
                .OrderBy(t => t.Timestamp)
                .ToList();
        }
    }
}
