using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.BL.DataModel
{
    public class ItemDelivery
    {
        [Key, Column(Order = 0)]
        public int ItemID { get; set; }

        [Key, Column(Order = 1)]
        public int DeliveryID { get; set; }

        public virtual Item Item { get; set; }
        public virtual Delivery Delivery { get; set; }

        public int Quantity { get; set; }
    }
}
