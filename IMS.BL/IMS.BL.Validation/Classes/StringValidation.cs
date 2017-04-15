using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.Validation
{
    public class StringValidation
    {
        public string ErrorMessage { get; set; }
        protected static string ValidateString(string input)
        {
            string errorMessage = "";
            errorMessage += IsNullOrEmptyOrWhiteSpace(input);
            return errorMessage;
        }

        protected static string IsNullOrEmptyOrWhiteSpace(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "You must enter a value.\n";
            }
            else if(string.IsNullOrWhiteSpace(input))
            {
                return "You must enter valid characters.\n";
            }
            return null;
        }
        
        /// <summary>
        /// Public method that shows whether
        /// a string is WhiteSpace, Null or Empty
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsStringNullOrWhiteSpace(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return true;
            }
            else if (string.IsNullOrWhiteSpace(input))
            {
                return true;
            }
            return false;
        }
    }
}
