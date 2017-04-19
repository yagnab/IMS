using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL.Repositories;
using IMS.BL.DataModel;

namespace IMS.BL.Validation
{
    public class NewUsernameValidation : StringValidation
    {
        public bool doesUsernameExist { get; private set; }
        public bool isNewUsernameValid { get; private set; }
        public NewUsernameValidation(string username, string usernameFieldName = "Username") : base(username, usernameFieldName)
        {
            doesUsernameExist = DoesUsernameExist(username);

            //initialising isUsernameValid
            if(!doesUsernameExist && isStringValid)
            {
                isNewUsernameValid = true;
            }
            else
            {
                isNewUsernameValid = false;
            }

            //building ErrorMessage
            if(doesUsernameExist)
            {
                ErrorMessage += "That " + usernameFieldName + " already exists.\n";
            }
        }

        bool DoesUsernameExist(string username)
        {
            List<UserAccount> user;
            using (var uaRepo = new UserAccountRepo(new InventoryContext()))
            {
                user = uaRepo.UserAccountByUsername("username");
                uaRepo.Complete();
            }

            //user found
            if (user.Count > 0)
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
