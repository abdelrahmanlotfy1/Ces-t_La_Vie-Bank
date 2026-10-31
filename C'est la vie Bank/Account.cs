using System;
using System.Linq;

namespace C_est_la_vie_Bank
{
    public abstract class Account
    {
        private string _id = string.Empty;

        public string ID
        {
            get => _id;
            private set
            {
                if (value == null || value.Length < 10 || !value.All(char.IsDigit))
                    throw new ArgumentException(" Bank ID Must be 10 Number At least");
                _id = value;
            }
        }

        public decimal Balance { get; private set; }

        public object SyncRoot { get; } = new object();

        public event EventHandler<TransactionFailedEventArgs>? TransictionField;
        public event EventHandler<ProccesCompletedEventArgs>? ProccesCompleted;
        public event EventHandler<AlertLowBalanceEventHandler>? LowBalanceDetected;

        // Each child says what it is
        public abstract string AccountType { get; }

        // Each child says what it does at month end
        public abstract void ApplyMonthEnd();

        // The one number that differs between account types
        protected virtual decimal MinimumAllowedBalance => 0;

        protected Account(string id, decimal initialBalance = 0)
        {
            ID = id;
            if (initialBalance < 0)
                throw new ArgumentException(" Balance can't be less than 0 ");
            Balance = initialBalance;
        }
        decimal newBalnce;
        public  bool Withdraw(decimal amount)
        {
            

            lock (SyncRoot)
            {
                if (amount <= 0)
                {
                    OnTransictionFailed(new TransactionFailedEventArgs(TransactionType.Withdraw, amount, "the amount must be positive"));
                    return false;
                }
                if (Balance - amount < MinimumAllowedBalance)
                {
                    OnTransictionFailed(new TransactionFailedEventArgs(TransactionType.Withdraw, amount, "the amount Cannot be bigger than balance"));
                    return false;
                }

                Balance -= amount;
                newBalnce = Balance;
            }

            OnProccessCompleted(new ProccesCompletedEventArgs(this, $"WithDraw Operation is done Sucssesfully and your Balance is {newBalnce}"));
            CheckLowBalance();
            return true;
        }

        public bool Deposite(decimal amount)
        {
            lock (SyncRoot)
            {
                if (amount <= 0)
                {
                    OnTransictionFailed(new TransactionFailedEventArgs(TransactionType.Deposit, amount, $" the amount is not appropiate {amount} "));
                    return false;
                }
                Balance += amount;
            }

            OnProccessCompleted(new ProccesCompletedEventArgs(this, $"Deposite Operation is done Sucssesfully and your Balance is {Balance}"));
            return true;
        }

        public void CheckLowBalance()
        {
            if (Balance < 300)
                AlertLowBalance(new AlertLowBalanceEventHandler(this, $"You are Reached a Low Level Balance Please Check your balance {ID}"));
        }

        protected void OnTransictionFailed(TransactionFailedEventArgs e) => TransictionField?.Invoke(this, e);
        protected void OnProccessCompleted(ProccesCompletedEventArgs e) => ProccesCompleted?.Invoke(this, e);
        protected void AlertLowBalance(AlertLowBalanceEventHandler e) => LowBalanceDetected?.Invoke(this, e);
    }
}