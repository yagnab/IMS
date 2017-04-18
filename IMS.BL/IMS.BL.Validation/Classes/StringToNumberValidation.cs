using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IMS.BL.Validation
{
    public class StringToNumberValidation : StringValidation
    {
        public bool areIllegalCharacters { get; private set; }
        public bool isStringNumberValid { get; private set; }

        /// <summary>
        /// This is the base class for StringToIntegerValidation
        /// And StringToDecimalValidation
        /// </summary>
        /// <param name="integer"></param>
        public StringToNumberValidation(string number, string numberFieldName = "Number") : base(number, numberFieldName) 
        {
            areIllegalCharacters = AreIllegalCharacters(number);

            //setting isStringNumberValid
            if(!areIllegalCharacters && isStringValid)
            {
                isStringNumberValid = true;
            }
            else
            {
                isStringNumberValid = false;
            }

            //building error message
            if(areIllegalCharacters)
            {
                ErrorMessage += numberFieldName + " must be digits only.\n";
            }
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
        /// <summary>
        /// Checks to see if string has
        /// Only digits
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        bool AreIllegalCharacters(string number)
        {
            var rex = new Regex("^[0-9]+$");

            //if illegal characters found
            if (! rex.IsMatch(number))
            {
                return false;
            }
            return true;
        }
    }
}
