using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.DataModel
{
    public class ItemDeliveryDisplay : DisplayBase
    {
        
        public ItemDeliveryDisplay(int _DeliveryID, DateTime _ExpectedArrivalDate, int _Quantity)
            :base(_DeliveryID, _ExpectedArrivalDate, _Quantity)
        {
            
        }
    }
}
