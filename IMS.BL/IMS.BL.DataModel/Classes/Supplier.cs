using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMS.BL.DataModel
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        //need to create new migration for this change
        [Required]
        public string SupplierName { get; set; }
        public List<Delivery> Deliveries { get; set; }
        public List<int> DeliveryIDs { get; set; }

        public override string ToString()
        {
            return "Supplier";
        }
    }
}
