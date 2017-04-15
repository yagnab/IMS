using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.DataModel
{
    public abstract class Delivery
    {
        public int DeliveryID { get; set; }
        public Supplier Supplier { get; set; }
        public int SupplierID { get; set; }
        public bool IsArrived { get; set; }
        public ICollection<ItemDelivery> ItemDeliveries { get; set; }

        //return all itemId for this delivery
        //readonly
        public IEnumerable<int> ItemIDs
        {
            get
            {
                return this.ItemDeliveries.Select(ItemDel => ItemDel.ItemID);
            }
        }
    }
}
