using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL
{
    public class ItemDisplay 
    {
        public string Description { get; set; }
        public string Barcode { get; set; }
        public decimal RRP { get; set; }
        public int QuantityStockLevel{ get; set; }
        public int QuantityWeaklySaleRate{ get; set; }
        public string Catagory { get; set; }
        public ItemDisplay(Item _Item) 
        {
            Description = _Item.Description;
            Barcode = _Item.Barcode;
            RRP = _Item.RRP;
            QuantityStockLevel = _Item.QuantityStockLevel;
            QuantityWeaklySaleRate = _Item.QuantityWeaklySaleRate;
            Catagory = GetCatagoryString(_Item.Catagory);
        }

        static string GetCatagoryString(ItemCatagory Catagory)
        {
            switch (Catagory)
            {
                case ItemCatagory.Alchol:
                    return "Alchol";
                case ItemCatagory.Chilled:
                    return "Chilled";
                case ItemCatagory.Confectionery:
                    return "Confectionery";
                case ItemCatagory.SoftDrink:
                    return "Soft Drink";
                default:
                    return null;

            }
        }
    }
}
