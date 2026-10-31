using System;
using System.Collections.Generic;
using System.Linq;

namespace C_est_la_vie_Bank
{
    public enum TransactionType { Deposit, Withdraw, Transfer }

    public class TransictionRecord
    {
        public TransactionType Type { get; }
        public string FromAccountID { get; }
        public string? ToAccountID { get; }
        public decimal Amount { get; }
        public bool IsSuccessful { get; }
        public DateTime Timestamp { get; }

        public TransictionRecord(TransactionType type, string fromAccountId, string? toAccountId, decimal amount, bool isSuccessful)
        {
            Type = type;
            FromAccountID = fromAccountId;
            ToAccountID = toAccountId;
            Amount = amount;
            IsSuccessful = isSuccessful;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{Timestamp:yyyy-MM-dd HH:mm:ss}] {Type} | Amount: {Amount} | Success: {IsSuccessful} | To: {ToAccountID ?? "N/A"}";
        }
    }
}