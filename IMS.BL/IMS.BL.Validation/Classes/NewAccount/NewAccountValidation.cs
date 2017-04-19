using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.Validation
{
    public class NewAccountValidation : ValidationBase
    {
        public bool isUsernameValid { get; private set; }
        public bool arePasswordsValid { get; private set; }
        public bool isNewAccountValid { get; private set; }
        public NewAccountValidation(string username, string password, string confirmPassoword, 
            string usernameFieldName = "Username", string passwordFieldName = "Password", string confirmPasswordFieldName = "Password Confirmation") : base()
        {
            var newUV = new NewUsernameValidation(username, usernameFieldName);
            var npV = new NewPasswordValidation(password, confirmPassoword, passwordFieldName, confirmPasswordFieldName);

            isUsernameValid = newUV.isNewUsernameValid;
            arePasswordsValid = npV.arePasswordsValid;

            if(isNewAccountValid && arePasswordsValid)
            {
                isNewAccountValid = true;
            }
            else
            {
                isNewAccountValid = false;
            }

            ErrorMessage += newUV.ErrorMessage;
            ErrorMessage += npV.ErrorMessage;

            //allow for reuse of objects
            newUV.Complete();
            npV.Complete();
        }
    }
}
