using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL
{
    public class ItemCatagoryDisplay
    {
        public Array Catagories { get; set; }
        public ItemCatagoryDisplay()
        {
            //Set catagories to value in ItemCatagory
            Catagories = Enum.GetValues(typeof(ItemCatagory));
        }
    }
}