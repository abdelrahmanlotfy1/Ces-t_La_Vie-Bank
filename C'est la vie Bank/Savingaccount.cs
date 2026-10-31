using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_est_la_vie_Bank
{
    public class SavingAccount : Account
    {
        public decimal InterestRate { get; }
        public decimal MaxWithdrawPerOperation { get; }

        public override string AccountType => "Saving";

        public SavingAccount(string id, decimal initialBalance = 0,
                             decimal interestRate = 0.05m,
                             decimal maxWithdrawPerOperation = 5000m)
            : base(id, initialBalance)
        {
            if (interestRate < 0)
                throw new ArgumentException(" Interest rate can't be negative ");
            InterestRate = interestRate;
            MaxWithdrawPerOperation = maxWithdrawPerOperation;
        }

        public override bool Withdraw(decimal amount)
        {
            if (amount > MaxWithdrawPerOperation)
            {
                OnTransictionFailed(new TransactionFailedEventArgs(
                    TransactionType.Withdraw, amount, $"Saving account limit per withdraw is {MaxWithdrawPerOperation}"));
                return false;
            }
            return base.Withdraw(amount);
        }

        public override void ApplyMonthEnd()
        {
            decimal interest = Math.Round(Balance * InterestRate / 12, 2);
            if (interest > 0)
                Deposite(interest);
        }
    }
}
