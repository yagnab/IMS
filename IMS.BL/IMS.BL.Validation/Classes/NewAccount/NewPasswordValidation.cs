using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.Validation
{
    public class NewPasswordValidation : ValidationBase
    {
        public bool doPasswordsMatch { get; private set; }
        //this references first arguement in constructor named "password"
        public bool isPasswordValid { get; private set; }
        //this refernces second arguement in constructor named "confirmPassword"
        public bool isConfirmPasswordValid { get; private set; }
        public bool isPasswordLengthOk { get; private set; }
        public bool arePasswordsValid { get; private set; }
        /// <summary>
        /// This class not inherit StringValidation 
        /// As there are two string to validate
        /// But it still inherits ValidationBase.
        /// This will not check hashed password.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public NewPasswordValidation(string password, string confirmPassword
            , string passwordFieldName="Password", string confirmPasswordFieldName="Password confirmation") : base()
        {
            doPasswordsMatch = DoPasswordsMatch(password, confirmPassword);
            isPasswordValid = IsPasswordValid(password, passwordFieldName);
            isConfirmPasswordValid = IsPasswordValid(confirmPassword, confirmPasswordFieldName);
            isPasswordLengthOk = IsPasswordLengthOk(password);

            //setting arePasswordsValid
            if (doPasswordsMatch && isPasswordValid && isConfirmPasswordValid && isPasswordLengthOk)
            {
                arePasswordsValid = true;
            }
            else
            {
                arePasswordsValid = false;
            }

            //building error message
            if(!doPasswordsMatch)
            {
                ErrorMessage += passwordFieldName + " and " + confirmPasswordFieldName + " do not match.\n";
            }
            if(!isPasswordLengthOk)
            {
                ErrorMessage += passwordFieldName + " length must be greater than 7 and less than 101";
            }
            //no need to build error message for isPasswordValid && isConfirmPasswordValid
            //as the IsPasswordValid method already does that
        }

        bool DoPasswordsMatch(string password, string confirmPassword)
        {
            if(password != confirmPassword)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// This method is to be used to validate
        /// The string aspect of password and confirm passoword
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        bool IsPasswordValid(string password, string passwordFieldName)
        {
            var stringV = new StringValidation(password, passwordFieldName);

            if (stringV.isStringValid)
            {
                //allow reuse
                stringV.Complete();
                return true;
            }
            else
            {
                //allow reuse
                stringV.Complete();
                ErrorMessage += stringV.ErrorMessage;
                return false;
            }
        }

        bool IsPasswordLengthOk(string password)
        {
            if(password.Length > 7 && password.Length < 101)
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
