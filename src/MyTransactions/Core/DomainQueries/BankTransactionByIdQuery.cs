using System.Linq;
using Magrathea;
using MyTransactions.Core.DomainModels;

namespace MyTransactions.Core.DomainQueries
{
    public class BankTransactionByIdQuery : Scalar<BankTransaction>
    {
        public BankTransactionByIdQuery(int id)
        {
            ContextQuery = c => c.AsQueryable<BankTransaction>().First(x => x.Id == id);
        }
    }
}