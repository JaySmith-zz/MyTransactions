using System.ComponentModel.DataAnnotations;

namespace MyTransactions.Core.DomainModels
{
    using System;

    public class BankTransaction
    {
        public int Id { get; set; }

        [Required]
        public DateTime PostedDate { get; set; }

        public int? CheckNumber { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public bool Cleared { get; set; }

        public string Memo { get; set; }

        public int? BankAccountId { get; set; }

        public BankTransactionType TransactionType { get; set; }

        public virtual BankAccount BankAccount { get; set; }
    }
}
