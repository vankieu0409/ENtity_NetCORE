using System.Linq;
using _1_0_EFCORE_DataBaseFirts.Context;

namespace _1_0_EFCORE_DataBaseFirts
{
    public class AccountService
    {
        private DatabaseContext dbContext;
        public AccountService()
        {
            dbContext = new DatabaseContext();
            var listACC = dbContext.AccountsAdos.ToList();
        }
    }
}