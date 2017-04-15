using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL.DataModel;

namespace IMS.BL.Validation
{
    public class StaffAccountValidation : AccountValidation
    {
        public static StaffAccount ValidateStaffAccount(string username, string password, InventoryContext dbContext)
        {
            string errorMessage = "";
            IQueryable<UserAccount> account = GetAccount(username, password, dbContext);
            errorMessage += ValidateAccount(username, password, dbContext);

            //If above validation succeded and account is admin account
            if (errorMessage == "" && IsAccountStaffAccount(account))
            {
                var staff = (from sa in dbContext.UserAccounts.OfType<StaffAccount>()
                             where sa.Username == username
                                 && sa.Password == password
                             select sa).First();
                return staff;
            }

            return null;
        }

        protected static bool IsAccountStaffAccount(IQueryable<UserAccount> account)
        {
            if (account.First().IsAdmin)
            {
                return false;
            }
            return true;
        }
    }
}
