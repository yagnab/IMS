using IMS.BL.DataModel;
using System.Collections.Generic;

namespace IMS.BL.Repositories
{
    public interface IAdminAccountsRepo : IRepository<AdminAccount>
    {
        IEnumerable<AdminAccount> AdminAccountByUsername(string username);
    }
}
