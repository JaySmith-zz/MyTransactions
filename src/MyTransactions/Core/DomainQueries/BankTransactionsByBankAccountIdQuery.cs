using System.Linq;
using Magrathea;
using MyTransactions.Core.DomainModels;

namespace MyTransactions.Core.DomainQueries
{
    public class BankTransactionsByBankAccountIdQuery : Scalar<BankTransaction>
    {
        public BankTransactionsByBankAccountIdQuery(int accountId)
        {
            ContextQuery = c => c.AsQueryable<BankTransaction>().FirstOrDefault(x => x.Id == accountId);
        }
    }
}