using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_est_la_vie_Bank
{
    internal class NotFoundAccountException : Exception
    {
        public string message;
        public string AccountId { get; }
        public NotFoundAccountException(string message, string accountId) : base(message)
        {
            {
                this.AccountId = accountId;
            }


        }

        public NotFoundAccountException(string? message) : base(message)
        {
        }
    }
}
