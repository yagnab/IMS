using IMS.BL.Repositories;
using IMS.BL.DataModel;
using System.Linq;

namespace IMS.BL.Validation
{
    public class TillDataRowValidation : ValidationBase
    {
        public bool isTillDataRowValid { get; private set; }
        //Item that corrisponds to barcode, if its valid
        public Item item { get; private set; }
        //integer quantity from the string quantity
        public int quantity { get; private set; }
        public TillDataRowValidation(string newBarcode, string stringQuantity)
        {
            var nbV = new OldBarcodeValidation(newBarcode);

            //barcode is valid
            if(nbV.isOldBarcodeValid)
            {
                //getting corrisponding item
                using (var iRepo = new ItemRepository(new InventoryContext()))
                {
                    item = iRepo.ItemByBarcode(newBarcode).First();
                }
                var qV = new QuantityValidation(stringQuantity, item);

                //quantity is valid
                if(qV.isQuantityValid)
                {
                    isTillDataRowValid = true;
                    quantity = (int)qV.IntQuantity;
                }
                else
                {
                    isTillDataRowValid = false;
                    quantity = -1;
                    ErrorMessage += qV.ErrorMessage;
                }
            }
            else
            {
                isTillDataRowValid = false;
                item = null;
                quantity = -1;
                ErrorMessage += nbV.ErrorMessage;
            }
        }
    }
}
