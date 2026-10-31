using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_est_la_vie_Bank
{
    public class CurrentAccount : Account
    {
        public decimal OverdraftLimit { get; }
        public decimal MonthlyFee { get; }

        public override string AccountType => "Current";

        protected override decimal MinimumAllowedBalance => -OverdraftLimit;

        public CurrentAccount(string id, decimal initialBalance = 0,
                              decimal overdraftLimit = 1000m,
                              decimal monthlyFee = 10m)
            : base(id, initialBalance)
        {
            if (overdraftLimit < 0)
                throw new ArgumentException(" Overdraft limit can't be negative ");
            OverdraftLimit = overdraftLimit;
            MonthlyFee = monthlyFee;
        }

        public override void ApplyMonthEnd()
        {
            Withdraw(MonthlyFee);  
        }
    }
}
