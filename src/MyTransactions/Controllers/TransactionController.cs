using System;
using System.Web.Mvc;
using Magrathea.Interfaces;
using MyTransactions.Core.DomainModels;
using MyTransactions.Core.DomainQueries;
using MyTransactions.Models;

namespace MyTransactions.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly IRepository _repository;

        public TransactionController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index(int id)
        {
            var query = new BankAccountByIdQuery(id);
            var account = _repository.Find(query);

            var model = new TransactionIndexViewModelBuilder().Build(account);

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(TransactionIndexViewModel model)
        {
            var query = new BankAccountByIdQuery(model.NewTransaction.BankAccountId);
            var account = _repository.Find(query);

            var newTransaction = new BankTransaction
            {
                PostedDate = DateTime.Parse(model.NewTransaction.Date),
                CheckNumber = model.NewTransaction.CheckNumber,
                Amount = model.NewTransaction.Amount,
                Cleared = model.NewTransaction.Cleared,
                Description = model.NewTransaction.Description,
                Memo = model.NewTransaction.Memo,
                TransactionType =
                    (BankTransactionType)
                    Enum.Parse(typeof(BankTransactionType), model.NewTransaction.TransactionType)
            };

            account.Transactions.Add(newTransaction);
            _repository.Context.Commit();

            return RedirectToAction("Index");
        }

        [Authorize]
        public JsonResult SetCleared(int transactionId, bool isCleared)
        {
            var query = new BankTransactionByIdQuery(transactionId);
            var transaction = _repository.Find(query);

            transaction.Cleared = isCleared;
            _repository.Context.Commit();

            return Json("Success");
        }
    }
}