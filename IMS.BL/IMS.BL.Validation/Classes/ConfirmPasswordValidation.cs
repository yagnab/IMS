using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.Validation
{
    public class ConfirmPasswordValidation : StringValidation
    {
        public static string ValidateConfirmPassword(string password, string confirmPassword)
        {
            string errorMessage = "";
            errorMessage += IsPasswordMatching(password, confirmPassword);
            return errorMessage;
        }

        protected static string IsPasswordMatching(string password, string confirmPassword)
        {
            if(password != confirmPassword)
            {
                return "Those passwords dont match.\n";
            }
            return "";
        }
    }
}
