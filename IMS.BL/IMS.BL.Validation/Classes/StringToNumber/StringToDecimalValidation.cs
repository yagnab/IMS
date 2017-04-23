using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.Validation
{
    public class StringToDecimalValidation : StringToNumberValidation
    {
        public bool canConvertStringDecimal { get; private set; }
        public bool isStringDecimalValid { get; private set; }
        /// <summary>
        /// Called _decimal to avoid conflict w/
        /// Keyword "decimal".
        /// 
        /// </summary>
        /// <param name="_decimal"></param>
        /// <param name=""></param>
        public StringToDecimalValidation(string _decimal, string _decimalFieldName = "Decimal") : base(_decimal, _decimalFieldName)
        {
            canConvertStringDecimal = CanConvertToDecimal(_decimal);

            if (canConvertStringDecimal && isStringValid && isStringNumberValid)
            {
                isStringDecimalValid = true;
            }
            else
            {
                isStringDecimalValid = false;
            }

            //error message building
            if (!canConvertStringDecimal)
            {
                ErrorMessage += _decimalFieldName + " must be a decimal value.\n";
            }
        }
        /// <summary>
        /// Need to validate is conversion is possible
        /// First.
        /// </summary>
        /// <param name="_decimal"></param>
        public static decimal DecimalFromString(string _decimal)
        {
            decimal value = Decimal.Parse(_decimal);
            value = Math.Round(value, 2);
            return value;
        }
        bool CanConvertToDecimal(string _decimal)
        {
            try
            {
                decimal realDecimal = StringToDecimalValidation.DecimalFromString(_decimal);

                //worked
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}
