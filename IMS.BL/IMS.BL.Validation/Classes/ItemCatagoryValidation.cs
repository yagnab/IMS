using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.Validation
{
    public class ItemCatagoryValidation
    {
        public string ErrorMessage { get; set; }
        public ItemCatagory ItemCatagory { get; set; }

        public ItemCatagoryValidation()
        {
            ErrorMessage = "";
        }

        public bool ValidateItemCatagoryIndex(int index)
        {
            ErrorMessage = "";

            if (!IsIndexInRange(index))
            {
                return false;
            }

            //Validation worked
            ItemCatagory = (ItemCatagory)index;
            return true;
        }

        protected bool IsIndexInRange(int index)
        {
            ItemCatagoryDisplay idc = new ItemCatagoryDisplay();
            int numOfItemCatagories = idc.Catagories.Length;
            if (index < 0 || index >= numOfItemCatagories)
            {
                //failed validation
                ErrorMessage += "Pick an item catagory value.\n";
                return false;
            }
            return true;
        }
    }
}
