using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.Validation
{
    public class StringToIntegerValidation : StringToNumberValidation
    {
        public bool canConvertStringInteger { get; private set; }
        public bool isStringIntegerValid { get; private set; }
        public StringToIntegerValidation(string integer, string integerFieldName = "Integer ") : base(integer, integerFieldName)
        {
            canConvertStringInteger = CanConvertStringInteger(integer);

            if(canConvertStringInteger && isStringValid && isStringNumberValid)
            {
                isStringIntegerValid = true;
            }
            else
            {
                isStringIntegerValid = false;
            }

            //building error message
            if (!canConvertStringInteger)
            {
                //can't convert to integer as integer type
                //isn't large enough to house string with only digits
                if(isStringNumberValid)
                {
                    ErrorMessage += integerFieldName + " is too large.\n";
                }
                else
                {
                    ErrorMessage += integerFieldName + " must be an integer.\n";
                }
            }
        }
        /// <summary>
        /// Need to validate if conversion
        /// Is possible first.
        /// </summary>
        /// <param name="integer"></param>
        /// <returns></returns>
        public static int IntegerFromString(string integer)
        {
            return Convert.ToInt32(integer);
        }
        bool CanConvertStringInteger(string integer)
        {
            try
            {
                //integer conversion worked
                int integerValue = Convert.ToInt32(integer);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
