using IMS.BL.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace IMS.BL.Repositories
{
    public class AdminAccountsRepo : Repository<AdminAccount>, IAdminAccountsRepo
    {
        public AdminAccountsRepo(InventoryContext context) : base(context)
        {
        }

        public IEnumerable<AdminAccount> AdminAccountByUsername(string username)
        {
            var user = (from u in Context.UserAccounts.OfType<AdminAccount>()
                        where u.Username == username
                        select u);
            return user;
        }
    }
}
