using MyTransactions.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MyTransactions.Core.DomainModels
{
    public class BankAccount
    {
        public BankAccount()
        {
            Transactions.CollectionChanged += Transactions_CollectionChanged;
        }

        private void Transactions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateCurrentBalance();
        }

        private void UpdateCurrentBalance()
        {
            var balance = StartingBalance;
            foreach (var transaction in Transactions)
            {
                if (transaction.TransactionType == BankTransactionType.Debit)
                    balance -= transaction.Amount;
                else
                    balance += transaction.Amount;
            }

            CurrentBalance = balance;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public decimal StartingBalance { get; set; }
        public decimal CurrentBalance { get; set; }
        public virtual ObservableCollection<BankTransaction> Transactions { get; set; } = new ObservableCollection<BankTransaction>();
        //public decimal ReconileBalance { get; set; }
        //public DateTime? ReconcileDate { get; set; }

    }
}