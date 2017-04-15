using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL
{
    public class ItemReservationDisplay : DisplayBase
    {
        public ItemReservationDisplay(int _ResevationID, DateTime _ExpectedPickUpDate, int _Quantity)
            :base(_ResevationID, _ExpectedPickUpDate, _Quantity)
        {
            
        }
    }
}
