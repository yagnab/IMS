using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.Validation
{
    public class RRPValidation
    {
        public string ErrorMessage { get; set; }
        public decimal RRP { get; set; }
        public RRPValidation()
        {
        }

        public bool ValidateRRPFromString(string rrp)
        {
            //Make sure error message displayed is of most recent validation
            ErrorMessage = "";
            RRP = 0;

            //Validate string isn't empty
            if (StringValidation.IsStringNullOrWhiteSpace(rrp))
            {
                //Sets error message to personalise for RRP
                ErrorMessage = "Enter a value for RRP";
                return false;
            }

            return StringToRRPValidation(rrp);
        }

        protected bool StringToRRPValidation(string rrp)
        {
            //Validate decimal value itself
            try
            {
                decimal input = Convert.ToDecimal(rrp);
                decimal roundedInput = Math.Round(input, 2);
                rrp = roundedInput.ToString();

                //It worked
                RRP = roundedInput;
                return true;
            }
            catch (Exception)
            {
                //Validation failed
                ErrorMessage = "Enter a valid price";
                return false;
            }
        }
    }
}
