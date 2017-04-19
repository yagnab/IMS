using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL.DataModel;

namespace IMS.BL.Validation
{
    public class NewItemValidation : ValidationBase
    {
        public bool isBarcodeValid { get; private set; }
        public bool isDescriptionValid { get; private set; }
        public bool isRRPValid { get; private set; }
        public bool isStockLevelValid { get; private set; }
        public bool isSaleRateValid { get; private set; }
        public bool isCatagoryValid { get; private set; }
        public bool isNewItemValid { get; private set; }
        /// <summary>
        /// Catagory validation is based on index
        /// Of selected item for catagory cmbbx.
        /// Rest are based on string values from controls.
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="description"></param>
        /// <param name="rrp"></param>
        /// <param name="stockLevel"></param>
        /// <param name="saleRate"></param>
        /// <param name="catagoryIndex"></param>
        public NewItemValidation(string barcode, string description, string rrp, string stockLevel, string saleRate, int catagoryIndex) : base()
        {
            var nbV = new NewBarcodeValidation(barcode);
            var dV = new DescriptionValidation(description);
            var rrpV = new RRPValidation(rrp);
            var stockLevelValidator = new StringToIntegerValidation(stockLevel);
            var saleRateValidatator = new StringToIntegerValidation(saleRate);

            //setting properties
            isBarcodeValid = nbV.isNewBarcodeValid;
            isDescriptionValid = dV.isDescriptionValid;
            isRRPValid = rrpV.isStringDecimalValid;
            isStockLevelValid = stockLevelValidator.isStringIntegerValid;
            isSaleRateValid = saleRateValidatator.isStringIntegerValid;
            isCatagoryValid = IsCatagoryValid(catagoryIndex);

            //building error message
            if(!isCatagoryValid)
            {
                ErrorMessage += "Pick a valid catagory.\n";
            }
            ErrorMessage += nbV.ErrorMessage + dV.ErrorMessage + rrpV.ErrorMessage 
                + stockLevelValidator.ErrorMessage + saleRateValidatator.ErrorMessage;
        }
        bool IsCatagoryValid(int catagoryIndex)
        {
            //number of types in Enum ItemCatagory
            var numOfCatagories = Enum.GetNames(typeof(ItemCatagory)).Length;

            //Only index 0,1,2... ,(numOfCatagories-1) allowed
            if (catagoryIndex > -1 && catagoryIndex < numOfCatagories)
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
