using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL.DataModel;

namespace IMS.BL.Repositories
{
    public class UserAccountRepo : Repository<UserAccount>, IUserAccountRepo
    {
        public UserAccountRepo(InventoryContext context) : base(context)
        {
        }

        public List<UserAccount> UserAccountByUsername(string username)
        {
            var user = (from u in Context.UserAccounts
                        where u.Username == username
                        select u).ToList();
            return user;
        }
    }
}
