using JsonFileDatabase;
using MyTransactions.Core.DomainModels;
using MyTransactions.Models;

namespace MyTransactions.Core
{
    public class JsonFileRepository
    {
        private readonly FileDataRepository _data;

        public JsonFileRepository()
        {
            _data = new FileDataRepository("~/App_Data/" + Properties.Settings.Default.DataFile);
        }

        public BankAccount ReadFinancialAccount()
        {
            return _data.Read<BankAccount>();
        }

        public void SaveFinancialAccount(BankAccount account)
        {
            _data.Save(account);
        }
    }
}