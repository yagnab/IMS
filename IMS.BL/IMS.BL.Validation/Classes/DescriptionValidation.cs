using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IMS.BL.Validation
{
    public class DescriptionValidation : StringValidation
    {
        public DescriptionValidation()
        {
            ErrorMessage = "";
        }

        public bool ValidateDescription(string description)
        {
            //Error message will show result of most recent validaiton
            ErrorMessage = "";

            ErrorMessage += ValidateString(description);
            
            //If description is empty or whitespace
            if(ErrorMessage != "")
            {
                return false;
            }
            else
            {
                ErrorMessage += IsDescriptionLengthOk(description);
                ErrorMessage += IllegalCharactersCheck(description);

                if (ErrorMessage == "")
                {
                    return true;
                }
                else
                {
                    //Errors found
                    return false;
                }
            }
        }
        protected string IsDescriptionLengthOk(string description)
        {
            if(description.Length > 300)
            {
                return "That Description is too long";
            }
            else if(description.Length < 5)
            {
                return "That description is too short";
            }

            //No validation error
            return "";
        }

        protected string IllegalCharactersCheck(string desciption)
        {
            //Only allows a-z, A-Z, 0-9 and apostrophe
            Regex rex = new Regex("^[a-zA-Z0-9']*$");

            if (! rex.IsMatch(desciption))
            {
                return "Only Alphanumeric characters apostrophe allowed";
            }
            
            //No validaiton error
            return "";
        }
    }
}
