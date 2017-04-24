using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.Validation
{
    public class IncreaseQuantityValidation : ValidationBase
    {
        //If the current request to increase quantity
        //of the item is valid or not
        public bool isRequestValid { get; private set; }
        public int intQuantity { get; private set; }
        public IncreaseQuantityValidation(string oldBarcode, string quantity)
        {
            var obV = new OldBarcodeValidation(oldBarcode);
            var nqV = new NewQuantityValidation(quantity);

            //validation was passed
            if(obV.isOldBarcodeValid && nqV.isNewQuantityValid)
            {
                isRequestValid = true;
                intQuantity = StringToIntegerValidation.IntegerFromString(quantity);
            }
            else
            {
                isRequestValid = false;
                intQuantity = -1;
                ErrorMessage += obV.ErrorMessage;
                ErrorMessage += nqV.ErrorMessage;
            }
        }
    }
}
