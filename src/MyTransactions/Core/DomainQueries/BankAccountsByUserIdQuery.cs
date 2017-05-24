using System.Linq;
using Magrathea;
using MyTransactions.Core.DomainModels;

namespace MyTransactions.Core.DomainQueries
{
    public class BankAccountsByUserIdQuery : Query<BankAccount>
    {
        public BankAccountsByUserIdQuery(string userId)
        {
            ContextQuery = c => c.AsQueryable<BankAccount>().Where(x => x.UserId == userId);
        }
    }
}