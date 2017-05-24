using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MyTransactions.Core.DomainModels;

namespace MyTransactions.Core
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=DataContext")
        {
        }

        public virtual DbSet<BankAccount> BankAccounts { get; set; }
        public virtual DbSet<BankTransaction> BankTransactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<FinancialAccount>()
            //    .HasMany(e => e.Transactions)
            //    .WithRequired(e => e.FinancialAccount)
            //    .HasForeignKey(e => e.FinancialAccount_Id);

            //modelBuilder.Entity<FinancialAccount>()
            //    .HasRequired(n => n.ApplicationUser)
            //    .WithMany(a => a.FinancialAccounts)
            //    .HasForeignKey(n => n.ApplicationUserID)
            //    .WillCascadeOnDelete(false);
        }
    }
}