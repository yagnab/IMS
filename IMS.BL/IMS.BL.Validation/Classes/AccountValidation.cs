using IMS.BL;
using IMS.BL.DataModel;
using System.Linq;
using System.Data.Entity;

namespace IMS.BL.Validation
{
    public class AccountValidation
    {
        protected static string ValidateAccount(string username, string password, InventoryContext dbContext)
        {
            string errorMessage = "";
            IQueryable<UserAccount> account = GetAccount(username, password, dbContext);

            errorMessage += DoesAccountExist(account);
            return errorMessage;
        }
        protected static string ValidateAccount(string username, string password, InventoryContext dbContext, IQueryable<UserAccount> account)
        {
            string errorMessage = "";
            
            errorMessage += DoesAccountExist(account);
            return errorMessage;
        }
        protected static string DoesAccountExist(IQueryable<UserAccount> account)
        {
            //Account doesn't exist
            if(account.Count() < 1)
            {
                return "That account doesn't exist";
            }
            return "";
        }
        protected static IQueryable<UserAccount> GetAccount(string username, string password, InventoryContext dbContext)
        {
            var account = from a in dbContext.UserAccounts
                          where a.Username == username && a.Password == password
                          select a;
            return account;
        }
    }
}
