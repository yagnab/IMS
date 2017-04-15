using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IMS.BL.Validation
{
    public class IntegerValidation
    {
        public string ErrorMessage { get; set; }
        protected IntegerValidation()
        {
            //Initialises error message as nothing
            ErrorMessage = "";
        }
        /// <summary>
        /// Will try to convert string input to int?
        /// If it fails it will return null and ErrorMessage
        /// Will say why it failed
        /// </summary>
        /// <param name="integer"></param>
        /// <returns></returns>
        protected int? GetIntegerFromString(string integer)
        {
            bool nullOrWhiteSpace = StringValidation.IsStringNullOrWhiteSpace(integer);

            if (nullOrWhiteSpace)
            {
                ErrorMessage += "Please enter a value";
                return null;
            }

            return ConvertToInt(integer);

        }
        
        protected int? ConvertToInt(string integer)
        {
            try
            {
                return Convert.ToInt32(integer);
            }
            catch(Exception)
            {
                ErrorMessage += "Please enter an integer";
                //String not in right format
                return null;
            }
            

        }

        protected bool IsQuantityOnlyDigits(string quantity)
        {
            var rex = new Regex("^[0-9]+$");

            if (! rex.IsMatch(quantity))
            {
                return false;
            }
            return true;
        }
    }
}
