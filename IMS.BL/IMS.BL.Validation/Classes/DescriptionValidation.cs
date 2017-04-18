using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IMS.BL.Validation
{
    public class DescriptionValidation : StringValidation
    {
        public bool isDescriptionLengthOk { get; private set; }
        public bool anyIllegalCharacters { get; private set; }
        public bool isDescriptionValid { get; private set; }
        public DescriptionValidation(string description, string descriptionFieldName = "Description") : base(description, descriptionFieldName)
        {
            isDescriptionLengthOk = IsDescriptionLengthOk(description);
            anyIllegalCharacters = AnyIllegalCharacters(description);

            //setting isDescriptionValid
            if(isDescriptionLengthOk && !anyIllegalCharacters && isStringValid)
            {
                isDescriptionValid = true;
            }
            else
            {
                isDescriptionValid = false;
            }

            //building error message
            if(!isDescriptionLengthOk)
            {
                ErrorMessage += descriptionFieldName + "'s length must be less than 300 and more than 5.\n";
            }
            if(anyIllegalCharacters)
            {
                ErrorMessage += descriptionFieldName + " must only contain alphanumeric characters and an apostrophe.\n";
            }
        }
        
        bool IsDescriptionLengthOk(string description)
        {
            if(description.Length > 300 || description.Length < 5)
            {
                //length is not ok
                return false;
            }
            else
            {
                return true;
            }
        }

        bool AnyIllegalCharacters(string desciption)
        {
            //Only allows a-z, A-Z, 0-9 and apostrophe
            Regex rex = new Regex("^[a-zA-Z0-9']*$");

            if (!rex.IsMatch(desciption))
            {
                //illegal characters do exist in description
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
