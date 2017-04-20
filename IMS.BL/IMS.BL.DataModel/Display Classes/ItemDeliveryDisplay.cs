using System;

namespace IMS.BL.DataModel
{
    public class CurrentItemDeliveryDisplay : DisplayBase
    {
        public int ItemID { get; private set; }
        public string Barcode { get; private set; }
        public string Description { get; private set; }
        public int QuantityInStock { get; private set; }
        public decimal RRP { get; private set; }
        public int QuantityInDelivery { get; private set; }
        public CurrentItemDeliveryDisplay(int deliveryID, DateTime expectedArrivalDate, int quantityInDelivery, 
            int itemID, string barcode, string description, int quantityInStock, decimal rrp)
            :base(deliveryID, expectedArrivalDate, quantityInDelivery)
        {
            //item related attributes
            ItemID = itemID;
            Barcode = barcode;
            Description = description;
            QuantityInStock = quantityInStock;
            RRP = rrp;

            //delivery related attribute defined in base class
        }

        /// <summary>
        /// Requires an item delivery and 
        /// Corrisponding CurrentDelivery and
        /// Item
        /// </summary>
        /// <param name="itemDelivery"></param>
        /// <param name="currentDelivery"></param>
        public static CurrentItemDeliveryDisplay GetCurrentItemDeliveryDisplay(ItemDelivery itemDelivery, CurrentDelivery currentDelivery, Item item)
        {
            return new CurrentItemDeliveryDisplay(itemDelivery.DeliveryID, 
                currentDelivery.ExpectedArrivalDate, itemDelivery.Quantity
                , item.ItemID, item.Barcode, item.Description, 
                item.QuantityStockLevel, item.RRP);
        }
    }
}
