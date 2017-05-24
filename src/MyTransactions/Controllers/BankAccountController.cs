using Magrathea.Interfaces;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyTransactions.Core.DomainModels;
using MyTransactions.Core.DomainQueries;

namespace MyTransactions.Controllers
{
    [Authorize]
    public class BankAccountController : Controller
    {
        private readonly IRepository _repository;

        public BankAccountController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var query = new BankAccountsByUserIdQuery(User.Identity.GetUserId());
            var myAccounts = _repository.Find(query);

            return View(myAccounts);
        }

        public ActionResult Create()
        {
            return View(new BankAccount());
        }

        [HttpPost]
        public ActionResult Create(BankAccount account)
        {
            account.UserId = User.Identity.GetUserId();

            _repository.Context.Add(account);
            _repository.Context.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var query = new BankAccountByIdQuery(id);
            var account = _repository.Find(query);

            _repository.Context.Remove(account);
            _repository.Context.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var query = new BankAccountByIdQuery(id);
            var account = _repository.Find(query);

            return View(account);
        }
    }
}