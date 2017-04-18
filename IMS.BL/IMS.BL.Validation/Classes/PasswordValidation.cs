using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.Validation
{
    public class ExistingPasswordValidation : StringValidation
    {
        public PasswordValidation()
        {

        }
        public static string ValidatePassword(string password)
        {
            string errorMessage = "";
            errorMessage += ValidateString(password);
            errorMessage += IsPasswordLenghtOK(password);
            return errorMessage;
        }

        protected static string IsPasswordLenghtOK(string password)
        {
            if( password.Length < 9)
            {
                return "That password is too small.\n";
            }
            else if (password.Length > 100)
            {
                return "That password is too long.\n";
            }
            return "";
        }
    }
}
