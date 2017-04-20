using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL.DataModel;
using System.Collections;

namespace IMS.BL.Validation
{
    public class QuantityValidation : StringToIntegerValidation
    {
        public int? IntQuantity { get; private set; }
        public Item Item { get; private set; }
        public bool? isQuantityInRange { get; private set; }
        //Whether QuantityStockLevel in db >= intQuantity
        public bool? isQuantityInDBEnough { get; private set;}
        public bool isQuantityValid { get; private set; }
        public QuantityValidation(string quantity, Item item, string quantityFieldName = "Quantity") : base(quantity, quantityFieldName)
        {
            Item = item;
            if (isStringIntegerValid)
            {
                //can convert string to integer
                IntQuantity = IntegerFromString(quantity);
                isQuantityInRange = IsQuantityInRange();
                isQuantityInDBEnough = IsQuantityInDBEnough();

                //can cast to bool as above results in only boolean results
                if((bool)isQuantityInRange && (bool)isQuantityInDBEnough)
                {
                    isQuantityValid = true;
                }
                else
                {
                    isQuantityValid = false;
                }

                //building error message
                if(!(bool)isQuantityInRange)
                {
                    ErrorMessage += quantityFieldName + " must more than 0 and less than 999.\n";
                }
                if (!(bool)isQuantityInDBEnough)
                {
                    ErrorMessage += quantityFieldName + " is greater than the quantity in stock.\n";
                }
            }
            else
            {
                //No way to detemine these
                //as can't convert string quantity to an integer
                IntQuantity = null;
                isQuantityInRange = null;
                isQuantityInDBEnough = null;
            }
        }
        bool IsQuantityInDBEnough()
        {
            if(IntQuantity > Item.QuantityStockLevel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public override void Complete()
        {
            ErrorMessage = "";
            IntQuantity = null;
            Item = null;
        }
        bool IsQuantityInRange()
        {
            if (IntQuantity > 0 && IntQuantity < 999)
            {
                //Quantity is in range
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
