using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.Validation
{
    public class NewBarcodeValidation : OldBarcodeValidation
    {
        public bool isBarcodeLengthOK { get; private set; }
        public bool isBarcodeOnlyDigits { get; private set; }
        //if all above are true, then this true
        public bool isNewBarcodeValid { get; private set; }
        /// <summary>
        /// fieldName is "new barcode by default".
        /// Use Complete() when done to reuse.
        /// </summary>
        /// <param name="newBarcode"></param>
        /// <param name="fieldName"></param>
        public NewBarcodeValidation(string newBarcode, string fieldName="new barcode") : base(newBarcode, fieldName)
        {
            isBarcodeLengthOK = IsBarcodeLengthOk(newBarcode);
            isBarcodeOnlyDigits = IsBarcodeOnlyDigits(newBarcode);

            //setting isNewBarcodeValid
            if(doesBarcodeExist || !isBarcodeOnlyDigits || !isBarcodeLengthOK || !isStringValid)
            {
                isNewBarcodeValid = false;
            }
            else
            {
                isNewBarcodeValid = true;
            }

            //forming error message
            if(doesBarcodeExist)
            {
                ErrorMessage += "That barcode already exists.\n";
            }
            if(!isBarcodeLengthOK)
            {
                ErrorMessage += "Barcode length must be greater than 10 or less than 50.\n";
            }
            if(!isBarcodeOnlyDigits)
            {
                ErrorMessage += "Barcodes must only be digits.\n";
            }
        }
        bool IsBarcodeLengthOk(string barcode)
        {
            if (barcode.Length > 50 || barcode.Length < 10)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Barcodes will only contain digits
        /// This method will ensure this is the case
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        bool IsBarcodeOnlyDigits(string barcode)
        {
            foreach (char c in barcode)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
    }
}
