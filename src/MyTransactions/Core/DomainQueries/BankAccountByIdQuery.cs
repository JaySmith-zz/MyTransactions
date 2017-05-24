using System.Linq;
using Magrathea;
using MyTransactions.Core.DomainModels;

namespace MyTransactions.Core.DomainQueries
{
    public class BankAccountByIdQuery : Scalar<BankAccount>
    {
        public BankAccountByIdQuery(int id)
        {
            ContextQuery = c => c.AsQueryable<BankAccount>().First(x => x.Id == id);
        }
    }
}