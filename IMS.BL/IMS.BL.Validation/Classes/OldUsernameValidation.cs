using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL.Repositories;
using IMS.BL.DataModel;

namespace IMS.BL.Validation
{
    public class OldUsernameValidation : StringValidation
    {
        public bool doesUsernameExist { get; private set; }
        public bool isOldUsernameValid { get; private set; }
        public OldUsernameValidation(string username, string usernameFieldName = "Username") : base(username, usernameFieldName)
        {
            doesUsernameExist = DoesUsernameExist(username);

            //setting isOldUsernameValid
            if(!doesUsernameExist && isStringValid)
            {
                isOldUsernameValid = true;
            }
            else
            {
                isOldUsernameValid = false;
            }

            //building error message
            if(doesUsernameExist)
            {
                ErrorMessage += "That " + usernameFieldName  +  " already exists.\n";
            }
        }

        bool DoesUsernameExist(string username)
        {
            List<UserAccount> user; 
            using (var uRepo = new UserAccountRepo(new InventoryContext() ))
            {
                user = uRepo.UserAccountByUsername(username);
                uRepo.Complete();
            }

            //user found
            if(user.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
