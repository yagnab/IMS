using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.Validation
{
    public class StringValidation : ValidationBase
    {
        public bool isNullOrWhitespace { get; private set; }
        public bool hasWhiteSpace { get; private set; }
        public bool isEmpty { get; private set; }
        //if all above are false, this is true
        public bool isStringValid { get; private set; }
        /// <summary>
        /// Pass in string to validate here
        /// The constructor will call all required validation methods
        /// And initiliaise properties to tell you about result of
        /// Validation. Then call Complete() to reuse
        /// </summary>
        /// <param name="input"></param>
        /// <param name="fieldName"></param>
        public StringValidation(string input, string fieldName) : base()
        {
            isNullOrWhitespace = IsNullOrWhitespace(input);
            hasWhiteSpace = HasWhiteSpace(input);
            isEmpty = IsEmpty(input);

            //seeing if the string is valid by above 3 standards
            if (isNullOrWhitespace || hasWhiteSpace || isEmpty)
            {
                isStringValid = false;
            }
            else
            {
                isStringValid = true;
            }

            //creating error message
            if(isNullOrWhitespace)
            {
                ErrorMessage += fieldName + "can't be nothing or whitespace.\n";
            }
            if(hasWhiteSpace)
            {
                ErrorMessage += fieldName + " can't have whitespace.\n";
            }
            if(isEmpty)
            {
                ErrorMessage += fieldName + " can't be empty.\n";
            }
        }
        
        protected bool IsNullOrWhitespace(string input)
        {
            if(input == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected bool HasWhiteSpace(string input)
        {
            bool hasWhiteSpace = false;
            hasWhiteSpace = input.Any(c => char.IsWhiteSpace(c));
            if(hasWhiteSpace)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected bool IsEmpty(string input)
        {
            if(input == string.Empty)
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
