using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL.DataModel;
using IMS.BL.Repositories;

namespace IMS.BL.Validation
{
    /// <summary>
    /// This will assume that username is valid if 
    /// Constructor is being called
    /// </summary>
    class OldPasswordValidation : StringValidation
    {
        /// <summary>
        /// Does password match with the password
        /// For the account with Username = username
        /// </summary>
        public bool doesPasswordMatch { get; private set; }
        public bool isOldPasswordValid { get; private set; }
        public OldPasswordValidation(string correctUsername, string password, string passwordFieldName = "Password", string usernameFieldName = "Username") : base(password, passwordFieldName)
        {
            doesPasswordMatch = DoesPasswordMatch(correctUsername, password);

            //setting isPasswordValid
            if (doesPasswordMatch && isStringValid)
            {
                isOldPasswordValid = true;
            }
            else
            {
                isOldPasswordValid = false;
            }

            //building error message
            if(!doesPasswordMatch)
            {
                ErrorMessage += passwordFieldName + " doesn't match that " + usernameFieldName;
            }
        }

        public bool DoesPasswordMatch(string correctUsername, string password)
        {
            UserAccount user;

            using (var uRepo = new UserAccountRepo(new InventoryContext()))
            {
                user = uRepo.UserAccountByUsername(correctUsername).First();
                uRepo.Complete();
            }

            //if password matches
            if(user.Password == password)
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
