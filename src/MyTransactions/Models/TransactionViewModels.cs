using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using MyTransactions.Core.DomainModels;

namespace MyTransactions.Models
{
    public class TransactionIndexViewModel
    {
        public BankAccountViewModel Account { get; set; }
        public BankTransactionViewModel NewTransaction { get; set; }
        public List<SelectListItem>TransactionTypeSelectListItems { get; set; }
    }

    public class TransactionIndexViewModelBuilder
    {
        public TransactionIndexViewModel Build(BankAccount account)
        {
            var viewModel = new TransactionIndexViewModel
            {
                Account = BuildAccountViewModel(account),
                NewTransaction = new BankTransactionViewModel
                {
                    BankAccountId = account.Id,
                    Date = DateTime.Now.ToShortDateString()
                },
                TransactionTypeSelectListItems = GetTransactionTypeSelectListItems()
            };


            return viewModel;
        }

        private BankAccountViewModel BuildAccountViewModel(BankAccount account)
        {
            var model = new BankAccountViewModel
            {
                Id = account.Id,
                Name = account.Name,
                StartingBalance = account.StartingBalance,
                CurrentBalance = account.CurrentBalance,
                Transactions = BuildTransactionsViewModels(account.Transactions, account.StartingBalance)
            };

            return model;
        }

        private static IEnumerable<BankTransactionViewModel> BuildTransactionsViewModels(IEnumerable<BankTransaction> accountTransactions, decimal accountCurrentBalance)
        {
            var transactionsSorted = accountTransactions.OrderBy(x => x.PostedDate).ToList();

            var models = new List<BankTransactionViewModel>();
            foreach (var transaction in transactionsSorted)
            {
                if (transaction.TransactionType == BankTransactionType.Credit)
                    accountCurrentBalance += transaction.Amount;
                else
                    accountCurrentBalance -= transaction.Amount;

                var model = new BankTransactionViewModel
                {
                   BankAccountId = transaction.BankAccount.Id,
                   TransactionType = Enum.GetName(typeof(BankTransactionType), transaction.TransactionType),
                   Id = transaction.Id,
                   CheckNumber = transaction.CheckNumber,
                   Amount = transaction.Amount,
                   Description = transaction.Description,
                   Date = transaction.PostedDate.ToShortDateString(),
                   Cleared = transaction.Cleared,
                   Memo = transaction.Memo,
                   Balance = accountCurrentBalance
                };

                models.Add(model);
            }

            return models.AsEnumerable();
        }

        private List<SelectListItem> GetTransactionTypeSelectListItems()
        {
            var items = new List<SelectListItem>();
            var values = Enum.GetValues(typeof(BankTransactionType));

            foreach (var value in values)
            {
                items.Add(new SelectListItem
                {
                    Value = value.ToString(),
                    Text = Enum.GetName(typeof(BankTransactionType), value)
                });
            }

            return items;
        }
    }

    public class BankAccountViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal StartingBalance { get; set; }
        public decimal CurrentBalance { get; set; }
        public IEnumerable<BankTransactionViewModel> Transactions { get; set; }
    }

    public class BankTransactionViewModel
    {
        public int BankAccountId { get; set; }

        public int Id { get; set; }

        [Required]
        public string Date { get; set; }

        public int? CheckNumber { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public string Description { get; set; }

        [Required]
        public string TransactionType { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public bool Cleared { get; set; }

        public string Memo { get; set; }

        public decimal Balance { get; set; }
        
    }
}