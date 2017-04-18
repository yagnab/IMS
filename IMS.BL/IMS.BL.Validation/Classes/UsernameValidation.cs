using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL.DataModel;

namespace IMS.BL.Validation
{
    /*
    public class UsernameValidation : StringValidation
    {
        public static string ValidateUsername(string username)
        {
            string errorMessage = "";
            errorMessage += ValidateString(username);
            errorMessage += IsLengthOk(username);
            errorMessage += IsUsernameUnique(username);
            return errorMessage;
        }

        protected static string IsLengthOk(string username)
        {
            if(username.Length < 5)
            {
                return "That username is too short.\n";
            }
            else if(username.Length > 50)
            {
                return "That username is too long.\n";
            }
            return "";
        }

        protected static string IsUsernameUnique(string username)
        {
            using(var dbContext = new InventoryContext())
            {
                var account = from a in dbContext.UserAccounts
                              where a.Username == username
                              select a;

                //If account with that username already exists
                if (account.Count() > 0)
                {
                    return "That username isn't unique.\n";
                }

                return "";
            }
        }
    }*/
}
