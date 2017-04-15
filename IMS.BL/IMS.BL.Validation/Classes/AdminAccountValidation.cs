using IMS.BL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.Validation
{
    public class AdminAccountValidation : AccountValidation
    {
        public static AdminAccount ValidateAdminAccount(string username, string password, InventoryContext dbContext)
        {
            string errorMessage = "";
            IQueryable<UserAccount> account = GetAccount(username, password, dbContext);
            errorMessage += ValidateAccount(username, password, dbContext);
            
            //If above validation succeded and account is admin account
            if (errorMessage == "" && IsAccountAdminAccount(account))
            {
                var admin = (from ad in dbContext.UserAccounts.OfType<AdminAccount>()
                             where ad.Username == username
                                 && ad.Password == password
                             select ad).First();
                return admin;
            }

            return null;
        }

        protected static bool IsAccountAdminAccount(IQueryable<UserAccount> account)
        {
            if (!account.First().IsAdmin)
            {
                return false;
            }
            return true;
        }
    }
}
