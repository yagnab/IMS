using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.BL.DataModel
{
    
    public class ItemTransaction
    {
        [Key, Column(Order = 0)]
        public int ItemID { get; set; }
        [Key, Column(Order = 1)]
        public int TransactionID { get; set; }  
        
        public virtual Item Item { get; set; }
        public virtual Transaction Transaction { get; set; }

        public int Quantity { get; set; }
    }
}
