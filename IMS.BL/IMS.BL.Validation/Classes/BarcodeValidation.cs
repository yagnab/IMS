using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL.DataModel;

namespace IMS.BL.Validation
{
    public class BarcodeValidation : StringValidation
    {
        public BarcodeValidation()
        {
            //ErrorMessage is empty
            ErrorMessage = "";
        }

        public Item GetItemFromBarcode(string barcode)
        {
            //Reset error message so its always acurate for most recently used barcode  
            ErrorMessage = "";

            string stringValidationError = StringValidation.ValidateString(barcode);

            if (stringValidationError == "")
            {
                Item tryItem = TryBarcode(barcode);
                if ( tryItem == null)
                {
                    //The barcode doesnt exist
                    ErrorMessage += "That is not a valid barcode";
                    return null;
                }
                else
                {
                    //Item does exist
                    return tryItem;
                }
            }
            else
            {
                //The string is empty, null or whitespace
                ErrorMessage += stringValidationError;
                return null;
            }
        }
        /// <summary>
        /// Tries to get an item
        /// With the provided barcode
        /// If its cant, returns null
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public static Item TryBarcode(string barcode)
        {
            using (var dbContext = new InventoryContext())
            {
                var newItems = from i in dbContext.Items
                               where i.Barcode == barcode
                               select i;

                //Barcode doesn't exist
                if (newItems == null)
                {
                    return null;
                }
                else
                {
                    return newItems.First();
                }
            }
        }

        public string ValidateNewBarcode(string barcode)
        {
            //Reset error message so its always acurate for most recently used barcode  
            ErrorMessage = "";

            ErrorMessage += StringValidation.ValidateString(barcode);
            ErrorMessage += IsBarcodeLenghtOk(barcode);
            ErrorMessage += IsBarcodeOnlyDigits(barcode);

            if(TryBarcode(barcode) != null)
            {
                ErrorMessage += "That barcode already exists";
            }

            return ErrorMessage;
        }

        protected string IsBarcodeLenghtOk(string barcode)
        {
            if(barcode.Length > 50 || barcode.Length < 10)
            {
                return "That barcode is in valid.\n";
            }
            return "";
        }
        /// <summary>
        /// Barcodes will only contain digits
        /// This method will ensure this is the case
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        protected string IsBarcodeOnlyDigits(string barcode)
        {
            foreach (char c in barcode)
            {
                if (c < '0' || c > '9')
                    return "A barcode can only contain digits.\n";
            }
            return "";
        }
    }
}
