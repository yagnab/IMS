using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL.DataModel;
using IMS.BL.Repositories;

namespace IMS.BL.Validation
{
    public class OldBarcodeValidation : StringValidation
    {
        public bool doesBarcodeExist { get; private set; }
        public bool isOldBarcodeValid { get; private set; }
        /// <summary>
        /// fieldName is "barcode" by default.
        /// Call Complete() when done with this.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="fieldName"></param>
        public OldBarcodeValidation(string barcode, string fieldName = "barcode") : base(barcode, fieldName)
        {
            doesBarcodeExist = DoesBarcodeExist(barcode);

            //setting isOldBarcodeValid
            if(doesBarcodeExist && isStringValid)
            {
                isOldBarcodeValid = true;
            }
            else
            {
                isOldBarcodeValid = false;
            }

            if(!doesBarcodeExist)
            {
                ErrorMessage += "That Barcode doesn't exist";
            }
        }
        bool DoesBarcodeExist(string barcode)
        {
            IEnumerable<Item> itemFound;

            //tries to get item with same barcode from database
            using (var iRepo = new ItemRepository(new InventoryContext()))
            {
                itemFound = iRepo.ItemByBarcode(barcode);
                iRepo.Complete();
            }

            //matche found
            if(itemFound.Count() > 0)
            {
                return true;
            }
            //on matches
            else
            {
                return false;
            }
        }
    }
}
